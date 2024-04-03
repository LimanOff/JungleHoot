using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(HealthSystem))]
public class WeaponHandler : MonoBehaviour
{
    public event Action WeaponPickedUp;
    public event Action WeaponDropped;

    private Weapon _currentWeapon;
    private GameObject _currentWeaponGO;
    private GameObject _probablyWeaponGO;

    private HealthSystem _healthSystem;

    [Header("Debug")]
    [SerializeField] private GameObject _weaponHolder;
    [SerializeField] private GameObject _weaponFreeParent;

    private PlayerInputController _inputController;

    [Inject]
    private void Construct(PlayerInputController inputController)
    {
        _inputController = inputController;
    }

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();

        SubscribeToEvents();
    }
    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        _healthSystem.Die += OnDropKeyPressed;

        if (gameObject.name == "Player 1")
        {
            _inputController.GameInput.Player1.Drop.performed += OnDropKeyPressed;
            _inputController.GameInput.Player1.Shoot.performed += OnShootKeyPressed;
            _inputController.GameInput.Player1.Interact.performed += OnInteractKeyPressed;
        }
        else
        {
            _inputController.GameInput.Player2.Drop2.performed += OnDropKeyPressed;
            _inputController.GameInput.Player2.Shoot2.performed += OnShootKeyPressed;
            _inputController.GameInput.Player2.Interact2.performed += OnInteractKeyPressed;
        }
    }
    private void UnSubscribeFromEvents()
    {
        _healthSystem.Die -= OnDropKeyPressed;

        if (IsThereWeaponInHands())
        {
            _currentWeapon.NoMoreBullets -= OnNoMoreBullets;
        }


        if (gameObject.name == "Player 1")
        {
            _inputController.GameInput.Player1.Drop.performed -= OnDropKeyPressed;
            _inputController.GameInput.Player1.Shoot.performed -= OnShootKeyPressed;
            _inputController.GameInput.Player1.Interact.performed -= OnInteractKeyPressed;
        }
        else
        {
            _inputController.GameInput.Player2.Drop2.performed -= OnDropKeyPressed;
            _inputController.GameInput.Player2.Shoot2.performed -= OnShootKeyPressed;
            _inputController.GameInput.Player2.Interact2.performed -= OnInteractKeyPressed;
        }
    }


    private void PickupWeapon(GameObject weaponGameObject)
    {
        if (weaponGameObject != null)
        {
            SetWeaponGameObjectInWeaponHolder(ref weaponGameObject, ref _weaponHolder);

            if (_currentWeaponGO != null)
            {
                _currentWeapon.NoMoreBullets += OnNoMoreBullets;
            }


            WeaponPickedUp?.Invoke();
        }
    }
    private void DropWeapon()
    {
        _currentWeapon.NoMoreBullets -= OnNoMoreBullets;

        UnSetWeaponGameObjectFromWeaponHolder(ref _currentWeaponGO, ref _currentWeapon);

        WeaponDropped?.Invoke();
    }

    private void OnDropKeyPressed(InputAction.CallbackContext context)
    {
        if (IsThereWeaponInHands() && Time.timeScale == 1)
            DropWeapon();
    }
    private void OnDropKeyPressed()
    {
        if (IsThereWeaponInHands() && Time.timeScale == 1)
            DropWeapon();
    }

    private void OnShootKeyPressed(InputAction.CallbackContext context)
    {
        if (IsThereWeaponInHands() && Time.timeScale == 1)
        {
            _currentWeapon.Shoot();
        }
    }
    private void OnShootKeyPressed()
    {
        if (IsThereWeaponInHands() && Time.timeScale == 1)
        {
            _currentWeapon.Shoot();
        }
    }

    private void OnInteractKeyPressed(InputAction.CallbackContext context)
    {
        if (!IsThereWeaponInHands() && Time.timeScale == 1)
            PickupWeapon(_probablyWeaponGO);
    }
    private void OnInteractKeyPressed()
    {
        if (!IsThereWeaponInHands() && Time.timeScale == 1)
            PickupWeapon(_probablyWeaponGO);
    }

    private void OnNoMoreBullets()
    {
        _currentWeaponGO.tag = "Untagged";
        OnDropKeyPressed();
    }

    public bool IsThereWeaponInHands()
    {
        if (_currentWeapon == null)
            return false;

        return true;
    }
    private void SetWeaponGameObjectInWeaponHolder(ref GameObject weaponGameObject, ref GameObject parentGameObject)
    {
        _currentWeaponGO = weaponGameObject;

        weaponGameObject.GetComponent<CircleCollider2D>().enabled = false;
        weaponGameObject.GetComponent<PolygonCollider2D>().enabled = false;

        weaponGameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        weaponGameObject.transform.SetParent(parentGameObject.transform);

        ResetWeaponGameObjectTransforms(weaponGameObject);

        _currentWeapon = GetWeaponComponentFromGameObject(weaponGameObject);
        _currentWeapon.EnableBox2DCollider();

        void ResetWeaponGameObjectTransforms(GameObject weaponGO)
        {
            weaponGO.transform.localPosition = Vector3.zero;
            weaponGO.transform.rotation = Quaternion.identity;
            weaponGO.transform.localScale = Vector3.one;
        }
        Weapon GetWeaponComponentFromGameObject(GameObject weaponGO)
        {
            return weaponGO.GetComponent<Weapon>();
        }
    }
    private void UnSetWeaponGameObjectFromWeaponHolder(ref GameObject weaponGameObject, ref Weapon weapon)
    {
        weaponGameObject.transform.parent = null;

        weaponGameObject.GetComponent<CircleCollider2D>().enabled = true;
        weaponGameObject.GetComponent<PolygonCollider2D>().enabled = true;

        weaponGameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        _currentWeapon.DisableBox2DCollider();

        weapon = null;
        weaponGameObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            _probablyWeaponGO = collision.gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            _probablyWeaponGO = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _probablyWeaponGO = null;
    }
}

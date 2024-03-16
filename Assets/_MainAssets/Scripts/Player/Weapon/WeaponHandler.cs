using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
        _healthSystem.Die += OnDrop;

        if (gameObject.name == "Player 1")
        {
            PlayerInputController.GameInput.Player1.Drop.performed += OnDrop;
            PlayerInputController.GameInput.Player1.Shoot.performed += OnShoot;
            PlayerInputController.GameInput.Player1.Interact.performed += OnInteract;
        }
        else
        {
            PlayerInputController.GameInput.Player2.Drop2.performed += OnDrop;
            PlayerInputController.GameInput.Player2.Shoot2.performed += OnShoot;
            PlayerInputController.GameInput.Player2.Interact2.performed += OnInteract;
        }
    }
    private void UnSubscribeFromEvents()
    {
        _healthSystem.Die -= OnDrop;

        if (IsThereWeaponInHands())
        {
            _currentWeapon.NoMoreBullets -= () =>
            {
                _currentWeaponGO.tag = "Untagged";
                OnDrop();
            };
        }
        

        if (gameObject.name == "Player 1")
        {
            PlayerInputController.GameInput.Player1.Drop.performed -= OnDrop;
            PlayerInputController.GameInput.Player1.Shoot.performed -= OnShoot;
            PlayerInputController.GameInput.Player1.Interact.performed -= OnInteract;
        }
        else
        {
            PlayerInputController.GameInput.Player2.Drop2.performed -= OnDrop;
            PlayerInputController.GameInput.Player2.Shoot2.performed -= OnShoot;
            PlayerInputController.GameInput.Player2.Interact2.performed -= OnInteract;
        }
    }


    private void PickupWeapon(GameObject weaponGameObject)
    {
        if (weaponGameObject != null)
        {
            SetWeaponGameObjectInWeaponHolder(ref weaponGameObject,ref _weaponHolder);

            _currentWeapon.NoMoreBullets += () =>
            {
                _currentWeaponGO.tag = "Untagged";
                OnDrop();
            };

            WeaponPickedUp?.Invoke();
        }
    }
    private void DropWeapon()
    {
        _currentWeapon.NoMoreBullets -= () =>
        {
            _currentWeaponGO.tag = "Untagged";
            OnDrop();
        };

        UnSetWeaponGameObjectFromWeaponHolder(ref _currentWeaponGO, ref _currentWeapon);

        WeaponDropped?.Invoke();
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (IsThereWeaponInHands())
            DropWeapon();
    }
    public void OnDrop()
    {
        if (IsThereWeaponInHands())
            DropWeapon();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (IsThereWeaponInHands())
        {
            _currentWeapon.Shoot();
        }
    }
    public void OnShoot()
    {
        if (IsThereWeaponInHands())
        {
            _currentWeapon.Shoot();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!IsThereWeaponInHands())
            PickupWeapon(_probablyWeaponGO);
    }
    public void OnInteract()
    {
        if (!IsThereWeaponInHands())
            PickupWeapon(_probablyWeaponGO);
    }

    public bool IsThereWeaponInHands()
    {
        if (_currentWeapon == null)
            return false;

        return true;
    }
    private void SetWeaponGameObjectInWeaponHolder(ref GameObject weaponGameObject,ref GameObject parentGameObject)
    {
        _currentWeaponGO = weaponGameObject;

        Rigidbody2D weaponRB2D = weaponGameObject.GetComponent<Rigidbody2D>();
        PolygonCollider2D weaponCollider2D = weaponGameObject.GetComponent<PolygonCollider2D>();

        weaponRB2D.simulated = false;
        weaponCollider2D.enabled = false;

        weaponGameObject.transform.SetParent(parentGameObject.transform);

        ResetWeaponGameObjectTransforms(weaponGameObject);

        _currentWeapon = GetWeaponComponentFromGameObject(weaponGameObject);

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
    private void UnSetWeaponGameObjectFromWeaponHolder(ref GameObject weaponGameObject,ref Weapon weapon)
    {
        weaponGameObject.transform.parent = null;

        weaponGameObject.GetComponent<PolygonCollider2D>().enabled = true;
        weaponGameObject.GetComponent<Rigidbody2D>().simulated = true;

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

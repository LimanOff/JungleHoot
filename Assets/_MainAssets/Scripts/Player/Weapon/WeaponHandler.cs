using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    public event Action PickUpedWeapon;
    public event Action DropedWeapon;
    public event Action<Vector3> WeaponDestroyed;

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

        foreach (Transform child in transform)
        {
            if (child.CompareTag("WeaponHolder"))
            {
                _weaponHolder = child.gameObject;
            }
        }

        _healthSystem.Die += DropWeapon;

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
    private void OnDestroy()
    {
        _healthSystem.Die -= DropWeapon;

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

    private void PickupWeapon(GameObject weapon)
    {
        if (_currentWeapon == null && _probablyWeaponGO != null)
        {
            Rigidbody2D weaponRB2D = weapon.GetComponent<Rigidbody2D>();
            PolygonCollider2D weaponCollider2D = weapon.GetComponent<PolygonCollider2D>();

            weaponRB2D.simulated = false;
            weaponCollider2D.enabled = false;

            weapon.transform.SetParent(_weaponHolder.transform);

            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.rotation = Quaternion.identity;
            weapon.transform.localScale = Vector3.one;

            _currentWeapon = weapon.GetComponent<Weapon>();
            _currentWeaponGO = weapon;

            _currentWeapon.NoMoreBullets += DropWeapon;

            PickUpedWeapon?.Invoke();
        }
    }
    private void DropWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.NoMoreBullets -= DropWeapon;

            _currentWeaponGO.transform.parent = null;

            if (_currentWeapon.CurrentAmountOfBullets == 0)
            {
                DestroyWeapon(_currentWeaponGO);
            }
            else
            {
                _currentWeaponGO.GetComponent<Rigidbody2D>().simulated = true;
                _currentWeaponGO.GetComponent<PolygonCollider2D>().enabled = true;
            }

            _currentWeapon = null;
            _currentWeaponGO = null;
            DropedWeapon?.Invoke();
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (_currentWeapon != null)
            DropWeapon();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (_currentWeapon != null)
            _currentWeapon.Shoot();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        PickupWeapon(_probablyWeaponGO);
    }

    private void DestroyWeapon(GameObject weaponGO)
    {
        WeaponDestroyed?.Invoke(weaponGO.transform.position);
        Destroy(weaponGO);
    }
}

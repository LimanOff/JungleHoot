using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    public event Action PickUpedWeapon;
    public event Action DropedWeapon;

    private Weapon _currentWeapon;
    private GameObject _currentWeaponGO;
    private GameObject _probablyWeaponGO;

    [Header("Debug")]
    [SerializeField] private GameObject _weaponHolder;
    [SerializeField] private GameObject _weaponFreeParent;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("WeaponHolder"))
            {
                _weaponHolder = child.gameObject;
            }
        }

        if (gameObject.name == "Player 1")
        {
            PlayerInputController.GameInput.Player1.Drop.performed += OnDrop;
            PlayerInputController.GameInput.Player1.Shoot.performed += OnShoot;
            PlayerInputController.GameInput.Player1.Interact.performed += OnInteract;
        }
        else
        {
            PlayerInputController.GameInput.Player2.Drop.performed += OnDrop;
            PlayerInputController.GameInput.Player2.Shoot.performed += OnShoot;
            PlayerInputController.GameInput.Player2.Interact.performed += OnInteract;
        }
    }
    private void OnDestroy()
    {
        if (gameObject.name == "Player 1")
        {
            PlayerInputController.GameInput.Player1.Drop.performed -= OnDrop;
            PlayerInputController.GameInput.Player1.Shoot.performed -= OnShoot;
            PlayerInputController.GameInput.Player1.Interact.performed -= OnInteract;
        }
        else
        {
            PlayerInputController.GameInput.Player2.Drop.performed -= OnDrop;
            PlayerInputController.GameInput.Player2.Shoot.performed -= OnShoot;
            PlayerInputController.GameInput.Player2.Interact.performed -= OnInteract;
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

    private void PickupWeapon(GameObject weapon)
    {
        if (_currentWeapon == null && _probablyWeaponGO != null)
        {
            Rigidbody2D weaponRB2D = weapon.GetComponent<Rigidbody2D>();
            BoxCollider2D weaponBoxCollider2D = weapon.GetComponent<BoxCollider2D>();

            weaponRB2D.simulated = false;
            weaponBoxCollider2D.enabled = false;

            weapon.transform.SetParent(_weaponHolder.transform);

            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.rotation = Quaternion.identity;
            weapon.transform.localScale = Vector3.one;

            _currentWeapon = weapon.GetComponent<Weapon>();
            _currentWeaponGO = weapon;

            PickUpedWeapon?.Invoke();
        }
    }
    private void DropWeapon()
    {
        _currentWeaponGO.transform.parent = null;

        _currentWeapon = null;

        _currentWeaponGO.GetComponent<Rigidbody2D>().simulated = true;
        _currentWeaponGO.GetComponent<BoxCollider2D>().enabled = true;

        _currentWeaponGO = null;
        DropedWeapon?.Invoke();
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
}

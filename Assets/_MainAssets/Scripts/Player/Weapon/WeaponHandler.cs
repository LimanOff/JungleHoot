using System;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public event Action PickUpedWeapon;
    public event Action DropedWeapon;

    private Weapon _currentWeapon;
    private GameObject _currentWeaponGO;

    [Header("Debug")]
    [SerializeField]private GameObject _weaponHolder;
    [SerializeField] private GameObject _weaponFreeParent;

    [Header("Input")]
    [SerializeField] private KeyCode InteractKey;
    [SerializeField] private KeyCode DropWeaponKey;
    [SerializeField] private KeyCode ShootWeaponKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            if (Input.GetKey(InteractKey))
            {
                PickupWeapon(collision.gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (Input.GetKey(InteractKey))
            {
                PickupWeapon(collision.gameObject);
            }
        }
    }

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.CompareTag("WeaponHolder"))
            {
                _weaponHolder = child.gameObject;
            }
        }
    }

    private void PickupWeapon(GameObject weapon)
    {
        if (_currentWeapon == null)
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
        _currentWeaponGO.transform.SetParent(_weaponFreeParent.transform);
        
        _currentWeapon = null;

        _currentWeaponGO.GetComponent<Rigidbody2D>().simulated = true;
        _currentWeaponGO.GetComponent<BoxCollider2D>().enabled = true;

        _currentWeaponGO = null;
        DropedWeapon?.Invoke();
    }

    private void Update()
    {
        if (_currentWeapon != null)
        {
            if (_currentWeapon.WeaponTypeOfShooting == Weapon.TypeOfShooting.Single)
            {
                if (Input.GetKeyDown(ShootWeaponKey) && _currentWeapon != null)
                {
                    _currentWeapon.Shoot();
                }
            }
            else if (_currentWeapon.WeaponTypeOfShooting == Weapon.TypeOfShooting.Auto)
            {
                if (Input.GetKey(ShootWeaponKey) && _currentWeapon != null)
                {
                    _currentWeapon.Shoot();
                }
            }

            if (Input.GetKey(DropWeaponKey))
            {
                DropWeapon();
            }
        }        
    }
}

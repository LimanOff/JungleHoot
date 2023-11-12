using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Weapon _currentWeapon;
    private GameObject _currentWeaponGO;

    private GameObject _weaponHolder;
    
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
        Rigidbody2D rb2d = weapon.GetComponent<Rigidbody2D>();

        rb2d.isKinematic = true;

        GameObject weaponGO = Instantiate(weapon, _weaponHolder.transform.position, Quaternion.identity);
        weaponGO.transform.SetParent(_weaponHolder.transform);

        _currentWeapon = weaponGO.GetComponent<Weapon>();
        _currentWeaponGO = weaponGO;

        Destroy(weapon,0);
    }

    private void DropWeapon()
    {
        _currentWeaponGO.transform.SetParent(null);
        
        _currentWeapon = null;

        _currentWeaponGO.GetComponent<Rigidbody2D>().isKinematic = false;

        _currentWeaponGO = null;
    }

    private void Update()
    {
        if (Input.GetKey(DropWeaponKey) && _currentWeapon != null)
        {
            DropWeapon();
        }

        if (Input.GetKey(ShootWeaponKey) && _currentWeapon != null)
        {
            _currentWeapon.Shoot();
        }
    }
}

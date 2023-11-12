using Unity.VisualScripting;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Debug Panel")]
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private GameObject _currentWeaponGO;

    [SerializeField] private GameObject WeaponHolder;
    
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

    private void PickupWeapon(GameObject weapon)
    {
        Rigidbody2D rb2d = weapon.GetComponent<Rigidbody2D>();

        Destroy(rb2d);

        if (weapon.GetComponent<Rigidbody2D>().IsDestroyed())
        {
            GameObject weaponGO = Instantiate(weapon, WeaponHolder.transform.position, Quaternion.identity);
            weaponGO.transform.SetParent(WeaponHolder.transform);

            Destroy(weapon);

            _currentWeapon = weaponGO.GetComponent<Weapon>();
            _currentWeaponGO = weaponGO;
        }
    }

    private void DropWeapon()
    {
        _currentWeaponGO.transform.SetParent(null);
        
        _currentWeapon = null;

        _currentWeaponGO.AddComponent<Rigidbody2D>();

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

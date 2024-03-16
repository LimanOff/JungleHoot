using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D),
    typeof(CircleCollider2D))]
public class Weapon : MonoBehaviour
{
    public event Action NoMoreBullets;
    public event Action Shooted;

    public string Name;

    public int MaxAmountOfBullets;
    [field: SerializeField] public int CurrentAmountOfBullets { get; private set; }


    [Header("Debug")]
    [SerializeField] private GameObject _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;

    private void Awake()
    {
        CurrentAmountOfBullets = MaxAmountOfBullets;
    }

    public void Shoot()
    {
        if (CurrentAmountOfBullets > 0)
        {
            var player = _bulletSpawnPoint.transform.parent.parent.parent;
            Quaternion rotation = player.localScale == Vector3.one ? Quaternion.Euler(new Vector3(0, 0, -90)) : Quaternion.Euler(0, -180, -90);

            Instantiate(_bulletPrefab, _bulletSpawnPoint.transform.position, rotation);

            CurrentAmountOfBullets--;
            Shooted?.Invoke();
        }
        else
        {
            NoMoreBullets?.Invoke();
        }
    }
}

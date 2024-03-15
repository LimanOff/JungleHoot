using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CompositeCollider2D))]
public class Weapon : MonoBehaviour
{
    public event Action NoMoreBullets;

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
        if(CurrentAmountOfBullets > 0)
        {
            var player = _bulletSpawnPoint.transform.parent.parent.parent;
            Quaternion rotation = player.localScale == Vector3.one ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(0, -180, 0);

            Instantiate(_bulletPrefab, _bulletSpawnPoint.transform.position, rotation);

            CurrentAmountOfBullets--;
        }
        else
        {
            NoMoreBullets?.Invoke();
        }
    }
}

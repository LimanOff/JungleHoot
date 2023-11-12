using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string Name;
    public int Damage;

    [SerializeField] private GameObject _bulletPrefab;

    public virtual void Shoot()
    {
        Debug.Log("Ïèó-Ïèó");
    }
}

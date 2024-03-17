using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weaponsPrefabs;
    [SerializeField] private List<GameObject> _gunSpawnPoints;

    [SerializeField] private int _spawnTimeOut;

    private Timer _timer;

    private Coroutine _spawnWeaponsCoroutine;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;
    }

    private void Awake()
    {
        _spawnWeaponsCoroutine = StartCoroutine(SpawnWeapons());
        _timer.TimeUp += () => StopCoroutine(_spawnWeaponsCoroutine);
    }

    private void OnDestroy()
    {
        _timer.TimeUp -= () => StopCoroutine(_spawnWeaponsCoroutine);
    }

    private IEnumerator SpawnWeapons()
    {
        while (true)
        {
            SpawnWeapon(GetRandomSpawnPosition(), GetRandomWeaponGOInList());
            yield return new WaitForSeconds(_spawnTimeOut);
        }
    }

    private void SpawnWeapon(Vector3 spawnPoint, GameObject weapon)
    {
        Instantiate(weapon, spawnPoint, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition() => _gunSpawnPoints[GetRandomNumber(_gunSpawnPoints.Count)].transform.position;

    private GameObject GetRandomWeaponGOInList() => _weaponsPrefabs[GetRandomNumber(_weaponsPrefabs.Count)];

    private int GetRandomNumber(int maxExclusive) => Random.Range(0, maxExclusive);
}

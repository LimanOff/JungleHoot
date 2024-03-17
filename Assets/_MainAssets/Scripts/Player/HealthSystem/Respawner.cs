using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
public class Respawner : MonoBehaviour
{
    public event Action<Vector3> PlayerRespawned;

    [SerializeField] private List<Transform> _respawnPoints;
    private HealthSystem _playerHS;

    private int _lastPoint;

    private void Awake()
    {
        _playerHS = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        _playerHS.Die += RespawnPlayer;
    }
    private void OnDisable()
    {
        _playerHS.Die -= RespawnPlayer;
    }

    private void RespawnPlayer()
    {
        if (_respawnPoints.Count == 0)
        {
            Debug.LogError("(Respawner/RespawnPlayer) Нет доступных точек появления.");
            return;
        }

        int respawnCount = _respawnPoints.Count-1;
        int randomPoint;
        do
        {
            randomPoint = Random.Range(0, respawnCount);
        } while (randomPoint == _lastPoint);

        _lastPoint = randomPoint;

        SetPlayerPosition(_respawnPoints[randomPoint].position);        
    }

    private void SetPlayerPosition(Vector3 position)
    {
        transform.position = position;
        PlayerRespawned?.Invoke(position);
    }
}

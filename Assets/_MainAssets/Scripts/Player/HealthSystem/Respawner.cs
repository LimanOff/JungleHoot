using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _respawnPoints;
    private HealthSystem _playerHS;

    private int _randomPoint;
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
        _randomPoint = Random.Range(0, _respawnPoints.Count);
        if (_randomPoint == _lastPoint)
        {
            _randomPoint = Random.Range(0, _respawnPoints.Count);
        }
        _lastPoint = _randomPoint;

        transform.position = _respawnPoints[_randomPoint].position;        
    }
}

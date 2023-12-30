using System;
using UnityEngine;

public class PlayerDeathCounter : MonoBehaviour
{
    public event Action<int> DeathCounterUpdated;

    private HealthSystem _playerHS;

    private int _deathCount;
    public int DeathCount
    {
        get => _deathCount;
        private set
        {
            _deathCount = value > 0 ? value : 0;
            DeathCounterUpdated?.Invoke(_deathCount);
        }
    }

    private void Awake()
    {
        _playerHS = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        _playerHS.Die += delegate { DeathCount++; };
    }
    private void OnDisable()
    {
        _playerHS.Die -= delegate { DeathCount++; };
    }
}

using System;
using UnityEngine;

public class PlayerDeathCounter : MonoBehaviour
{
    public event Action<int> DeathCountChanged;

    private HealthSystem _playerHS;

    private int _deathCount;
    public int DeathCount
    {
        get => _deathCount;
        private set
        {
            _deathCount = value > 0 ? value : 0;
            DeathCountChanged?.Invoke(_deathCount);
        }
    }

    private void Awake()
    {
        _playerHS = GetComponent<HealthSystem>();

        _playerHS.Die += IncrementDeathCount;
    }

    private void OnDestroy()
    {
        _playerHS.Die -= IncrementDeathCount;
    }

    private void IncrementDeathCount()
    {
        DeathCount++;
    }
}

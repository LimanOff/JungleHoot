 using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HealthSystem : MonoBehaviour
{
    public event Action Die;
    public Action<float> ReceivedDamage;

    [field: SerializeField] public float MaxHealth { get; private set; }

    private float _currentHealth;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = (value >= 0 && value <= MaxHealth) ? value : 0;

            ReceivedDamage?.Invoke(_currentHealth);

            if (_currentHealth == 0)
            {
                Die?.Invoke();
                CurrentHealth = MaxHealth;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"Нанесён урон {damage}");
    }
}
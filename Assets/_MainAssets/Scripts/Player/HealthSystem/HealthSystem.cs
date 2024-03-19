using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HealthSystem : MonoBehaviour
{
    public event Action Die;
    public event Action Hited;
    public Action<float> ReceivedDamage;

    public event Action InvincibleModeActivated;
    public event Action InvincibleModeDeactivated;

    [field: SerializeField] public float MaxHealth { get; private set; }

    private float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = (value >= 0 && value <= MaxHealth) ? value : 0;

            if (_currentHealth == 0)
            {
                Die?.Invoke();
            }
        }
    }

    private bool _canTakeDamage;

    [Header("Parameters")]
    [Range(2,3)]
    [SerializeField] private int _invincibleModeDurationSeconds;

    private Coroutine _invincibleModeCoroutine;

    private void Awake()
    {
        ResetHealth();
        _canTakeDamage = true;

        Die += ResetHealth;
        Die += StartInvincibleModeCoroutine;
    }

    private void OnDestroy()
    {
        Die -= ResetHealth;
        Die -= StartInvincibleModeCoroutine;
    }

    public void TakeDamage(float damage)
    {
        if (_canTakeDamage)
        {
            CurrentHealth -= damage;
            ReceivedDamage?.Invoke(CurrentHealth);
            Hited?.Invoke();
        }
    }

    private void StartInvincibleModeCoroutine()
    {
        _invincibleModeCoroutine = StartCoroutine(InvincibleModeFor(_invincibleModeDurationSeconds));
    }

    private void ActivateInvincibleMode()
    {
        _canTakeDamage = false;
        InvincibleModeActivated?.Invoke();
    }
    private void DeactivateInvincibleMode()
    {
        _canTakeDamage = true;
        InvincibleModeDeactivated?.Invoke();
        StopCoroutine(_invincibleModeCoroutine);
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    private IEnumerator InvincibleModeFor(int durationInSeconds)
    {
        ActivateInvincibleMode();
        yield return new WaitForSeconds(durationInSeconds);
        DeactivateInvincibleMode();
    }
}
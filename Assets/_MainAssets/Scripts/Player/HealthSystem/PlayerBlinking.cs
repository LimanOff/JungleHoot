using ModestTree;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(HealthSystem))]
public class PlayerBlinking : MonoBehaviour
{
    public event Action PlayerStartedBlinking;
    public event Action PlayerStoppedBlinking;

    private HealthSystem _healthSystem;

    [SerializeField] private List<SpriteRenderer> _playerBodyPartsSpriteRenderers;

    private Coroutine _blinkingCoroutine;

    [field: SerializeField] public bool IsBlinking { get; private set; }

    private void Awake()
    {
        ValidateComponents();

        IsBlinking = false;

        _healthSystem = GetComponent<HealthSystem>();

        _healthSystem.InvincibleModeActivated += StartBlinking;
        _healthSystem.InvincibleModeDeactivated += StopBlinking;
    }

    private void OnDestroy()
    {
        _healthSystem.InvincibleModeActivated -= StartBlinking;
        _healthSystem.InvincibleModeDeactivated -= StopBlinking;
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_playerBodyPartsSpriteRenderers, "(PlayerBlinking/ValidateComponents) Не заданы части тела игрока");
    }

    private void StartBlinking()
    {
        IsBlinking = true;

        _blinkingCoroutine = StartCoroutine(Blinking());

        _playerBodyPartsSpriteRenderers.ForEach(spriteRenderer =>
        {
            Color color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
        });

        PlayerStartedBlinking?.Invoke();
    }

    private void StopBlinking()
    {
        IsBlinking = false;

        StopCoroutine(_blinkingCoroutine);

        _playerBodyPartsSpriteRenderers.ForEach(spriteRenderer =>
        {
            Color color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
        });

        PlayerStoppedBlinking?.Invoke();
    }

    private IEnumerator Blinking()
    {
        float blinkSpeed = 20f;
        float alpha = 1f;

        while (true)
        {
            alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f;

            _playerBodyPartsSpriteRenderers.ForEach(spriteRenderer =>
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            });

            yield return null;
        }
    }
}

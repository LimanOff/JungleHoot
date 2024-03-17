using ModestTree;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Weapon : MonoBehaviour
{
    public event Action NoMoreBullets;
    public event Action Shooted;

    public string Name;

    public int MaxAmountOfBullets;

    private int _currentAmountOfBullets;
    public int CurrentAmountOfBullets
    {
        get => _currentAmountOfBullets;
        private set
        {
            _currentAmountOfBullets = Mathf.Clamp(value, 0, MaxAmountOfBullets);

            if (_currentAmountOfBullets == 0)
                NoMoreBullets?.Invoke();
        }
    }


    [Header("Debug")]
    [SerializeField] private GameObject _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [Space]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Coroutine _fadeOutCoroutine;

    private void Awake()
    {
        ValidateComponents();
        CurrentAmountOfBullets = MaxAmountOfBullets;

        NoMoreBullets += FadeOutSprite;
    }

    private void OnDestroy()
    {
        NoMoreBullets -= FadeOutSprite;
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_bulletSpawnPoint,"(Weapon/ValidateComponents) Не задана точка появления пули.");
        Assert.IsNotNull(_bulletPrefab, "(Weapon/ValidateComponents) Не задан префаб пули.");

        Assert.IsNotNull(_spriteRenderer, "(Weapon/ValidateComponents) Не задан компонент SpriteRenderer.");
    }

    public void Shoot()
    {
        if (CurrentAmountOfBullets > 0)
        {
            var player = _bulletSpawnPoint.transform.parent.parent.parent;
            Quaternion rotation = player.localScale == Vector3.one ? Quaternion.Euler(new Vector3(0, 0, -90)) : Quaternion.Euler(0, -180, -90);

            Instantiate(_bulletPrefab, _bulletSpawnPoint.transform.position, rotation);

            CurrentAmountOfBullets--;
            Shooted?.Invoke();
        }
        else
        {
            NoMoreBullets?.Invoke();
        }
    }

    private IEnumerator FadeOut(float fadeTime)
    {
        Color color = _spriteRenderer.color;
        float alpha = color.a;
        float fadeSpeed = 1f / fadeTime;

        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            color = new Color(color.r, color.g, color.b, alpha);
            _spriteRenderer.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void FadeOutSprite()
    {
        if (_fadeOutCoroutine != null)
            StopCoroutine(_fadeOutCoroutine);

        _fadeOutCoroutine = StartCoroutine(FadeOut(2f));
    }
}

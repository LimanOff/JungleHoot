using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D),
    typeof(CircleCollider2D))]
public class Weapon : MonoBehaviour
{
    public event Action NoMoreBullets;
    public event Action Shooted;

    public string Name;

    public int MaxAmountOfBullets;
    [field: SerializeField] public int CurrentAmountOfBullets { get; private set; }


    [Header("Debug")]
    [SerializeField] private GameObject _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [Space]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Coroutine _fadeOutCoroutine;

    private void Awake()
    {
        CurrentAmountOfBullets = MaxAmountOfBullets;

        NoMoreBullets += () => _fadeOutCoroutine = StartCoroutine(FadeOut(2f));
    }

    private void OnDestroy()
    {
        if (_fadeOutCoroutine != null)
            StopCoroutine(_fadeOutCoroutine);

        NoMoreBullets -= () => _fadeOutCoroutine = StartCoroutine(FadeOut(2f));
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
}

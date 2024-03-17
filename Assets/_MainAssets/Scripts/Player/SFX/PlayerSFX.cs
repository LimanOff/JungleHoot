using ModestTree;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
public class PlayerSFX : MonoBehaviour
{
    [Header("Sound source")]
    [SerializeField] private AudioSource _hitAudioSource;
    [SerializeField] private AudioSource _walkAudioSource;

    [Header("Sounds")]
    [SerializeField] private List<AudioClip> _footSounds;
    [SerializeField] private AudioClip _hurtSound;

    [SerializeField] private HealthSystem _healthSystem;

    private void Awake()
    {
        ValidateComponents();
        _healthSystem.Hited += PlayHurtSound;
    }

    private void OnDestroy()
    {
        _healthSystem.Hited -= PlayHurtSound;
    }

    private void PlayHurtSound()
    {
        PlaySound(_hurtSound, _hitAudioSource);
    }

    private void PlaySound(AudioClip audio, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audio);
    }

    public void PlayRandomFootSound()
    {
        if (_footSounds != null && _footSounds.Count > 0)
        {
            int randomIndex = Random.Range(0, _footSounds.Count-1);
            PlaySound(_footSounds[randomIndex], _walkAudioSource);
        }
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_hitAudioSource, "(PlayerSFX/ValidateComponents) Не задан источник звука для звука получения урона.");
        Assert.IsNotNull(_walkAudioSource, "(PlayerSFX/ValidateComponents) Не задан источник звука для звука ходьбы.");

        Assert.IsNotNull(_hurtSound, "(PlayerSFX/ValidateComponents) Не задан звук получения урона.");
        Assert.IsNotNull(_footSounds, "(PlayerSFX/ValidateComponents) Не заданы звуки ходьбы.");

        Assert.IsNotNull(_healthSystem,"(PlayerSFX/ValidateComponents) Не задан компонент HealthSystem");
    }
}

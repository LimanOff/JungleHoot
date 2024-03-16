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
        _healthSystem.Hited += () => PlaySound(_hurtSound, _hitAudioSource);
    }

    private void OnDestroy()
    {
        _healthSystem.Hited -= () => PlaySound(_hurtSound, _hitAudioSource);
    }

    private void PlaySound(AudioClip audio, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audio);
    }

    public void PlayRandomFootSound()
    {
        if (_footSounds != null)
        {
            int randomIndex = Random.Range(0, _footSounds.Count - 1);
            PlaySound(_footSounds[randomIndex], _walkAudioSource);
        }
    }
}

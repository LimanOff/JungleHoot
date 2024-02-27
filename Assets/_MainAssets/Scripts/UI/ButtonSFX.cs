using UnityEngine;

[DisallowMultipleComponent]
public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlaySound(AudioClip audio)
    {
        _audioSource.clip = audio;
        _audioSource.Play();
    }
}

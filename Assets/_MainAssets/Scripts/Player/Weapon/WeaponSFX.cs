using UnityEngine;

public class WeaponSFX : MonoBehaviour
{
    private Weapon _weapon;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _noAmmoSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _weapon = GetComponent<Weapon>();

        _weapon.Shooted += () => PlaySound(_shootSound);
        _weapon.NoMoreBullets += () => PlaySound(_noAmmoSound);
    }

    private void OnDestroy()
    {
        _weapon.Shooted -= () => PlaySound(_shootSound);
        _weapon.NoMoreBullets -= () => PlaySound(_noAmmoSound);
    }

    private void PlaySound(AudioClip audio)
    {
        _audioSource.PlayOneShot(audio);
    }
}

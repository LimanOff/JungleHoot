using UnityEngine;

[RequireComponent(typeof(WeaponHandler))]
public class WeaponHandlerSFX : MonoBehaviour
{
    [Header("Sound source")]
    [SerializeField] private AudioSource _audioSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip _pickUpWeaponSound;
    [SerializeField] private AudioClip _dropWeaponSound;

    private WeaponHandler _weaponHandler;

    private void Awake()
    {
        _weaponHandler = GetComponent<WeaponHandler>();

        _weaponHandler.WeaponPickedUp += () => PlaySound(_pickUpWeaponSound);
        _weaponHandler.WeaponDropped += () => PlaySound(_dropWeaponSound); 
    }

    private void OnDestroy()
    {
        _weaponHandler.WeaponPickedUp -= () => PlaySound(_pickUpWeaponSound);
        _weaponHandler.WeaponDropped -= () => PlaySound(_dropWeaponSound);
    }

    private void PlaySound(AudioClip audio)
    {
        _audioSource.PlayOneShot(audio);
    }
}

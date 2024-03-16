using ModestTree;
using UnityEngine;

[RequireComponent(typeof(Mover)),RequireComponent(typeof(HealthSystem)),
 RequireComponent(typeof(Jumper)), RequireComponent(typeof(WeaponHandler))]
public class AnimationChanger : MonoBehaviour
{
    [SerializeField] private string postfixNameForAnimationWithHoldingWeapon;

    [SerializeField] private Animator _animator;

    private Mover _mover;
    private Jumper _jumper;
    private WeaponHandler _weaponHandler;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        InitializeComponents();

        _mover.PlayerIsMoving += (movementValue) =>
        {
            _animator.SetInteger("Movement",(int)movementValue);
        };

        _weaponHandler.WeaponPickedUp += () => _animator.SetBool("HasWeapon",true);
        _weaponHandler.WeaponDropped += () => _animator.SetBool("HasWeapon", false);

        _jumper.PlayerInAir += () => _animator.SetBool("IsInAir", true);
        _jumper.PlayerOnGround += () => _animator.SetBool("IsInAir", false);

        _healthSystem.Hited += () => _animator.SetTrigger("Hit");
    }

    private void OnDestroy()
    {
        _mover.PlayerIsMoving -= (movementValue) =>
        {
            _animator.SetInteger("Movement", (int)movementValue);
        };
    }

    private void InitializeComponents()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _weaponHandler = GetComponent<WeaponHandler>();

        Assert.IsNotEmpty(postfixNameForAnimationWithHoldingWeapon, "(Класс AnimationChanger). Постфикс имя для анимаций с оружием на задано.");
    }
}

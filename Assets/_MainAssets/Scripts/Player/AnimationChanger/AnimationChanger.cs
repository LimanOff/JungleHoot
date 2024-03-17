using ModestTree;
using UnityEngine;

[RequireComponent(typeof(Mover)), RequireComponent(typeof(HealthSystem)),
RequireComponent(typeof(Jumper)), RequireComponent(typeof(WeaponHandler))]
public class AnimationChanger : MonoBehaviour
{
    [SerializeField] private string postfixNameForAnimationWithHoldingWeapon;
    [SerializeField] private Animator _animator;

    private Mover _mover;
    private Jumper _jumper;
    private WeaponHandler _weaponHandler;
    private HealthSystem _healthSystem;

    private const string MovementAnimation = "Movement";
    private const string HasWeaponParameter = "HasWeapon";
    private const string IsInAirParameter = "IsInAir";
    private const string HitTrigger = "Hit";

    private void Awake()
    {
        InitializeComponents();
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void InitializeComponents()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _weaponHandler = GetComponent<WeaponHandler>();

        Assert.IsNotNull(_animator, "(AnimationChanger) Не задан компонент Animator.");
        Assert.IsNotEmpty(postfixNameForAnimationWithHoldingWeapon, "(Класс AnimationChanger). Постфикс имя для анимаций с оружием на задано.");
    }

    private void HandleMovementAnimation(float movementValue)
    {
        _animator.SetInteger(MovementAnimation, (int)movementValue);
    }

    private void SetAnimatorBoolParameter(string parameterName, bool value)
    {
        _animator.SetBool(parameterName, value);
    }

    private void SubscribeEvents()
    {
        _mover.PlayerIsMoving += HandleMovementAnimation;
        _weaponHandler.WeaponPickedUp += OnWeaponPickedUp;
        _weaponHandler.WeaponDropped += OnWeaponDropped;
        _jumper.PlayerInAir += OnPlayerInAir;
        _jumper.PlayerOnGround += OnPlayerOnGround;
        _healthSystem.Hited += OnPlayerHit;
    }

    private void UnSubscribeEvents()
    {
        _mover.PlayerIsMoving -= HandleMovementAnimation;
        _weaponHandler.WeaponPickedUp -= OnWeaponPickedUp;
        _weaponHandler.WeaponDropped -= OnWeaponDropped;
        _jumper.PlayerInAir -= OnPlayerInAir;
        _jumper.PlayerOnGround -= OnPlayerOnGround;
        _healthSystem.Hited -= OnPlayerHit;
    }

    private void OnWeaponPickedUp()
    {
        SetAnimatorBoolParameter(HasWeaponParameter, true);
    }

    private void OnWeaponDropped()
    {
        SetAnimatorBoolParameter(HasWeaponParameter, false);
    }

    private void OnPlayerInAir()
    {
        SetAnimatorBoolParameter(IsInAirParameter, true);
    }

    private void OnPlayerOnGround()
    {
        SetAnimatorBoolParameter(IsInAirParameter, false);
    }

    private void OnPlayerHit()
    {
        _animator.SetTrigger(HitTrigger);
    }
}
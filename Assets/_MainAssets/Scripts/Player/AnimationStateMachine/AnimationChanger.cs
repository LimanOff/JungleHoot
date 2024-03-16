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

    private bool _hasWeapon;
    private bool _isInAir;

    private void Awake()
    {
        InitializeComponents();

        _weaponHandler.WeaponPickedUp += () => _hasWeapon = true;
        _weaponHandler.WeaponDropped += () => _hasWeapon = false;

        _mover.PlayerIsMoving += () => ChangeState("Run");

        _mover.PlayerIsStay += () => ChangeState("Stay");

        _jumper.PlayerInAir += () =>
        {
            ChangeState("Jump");
            _isInAir = true;
        };
        _jumper.PlayerOnGround += () =>
        {
            _isInAir = false;
        };

        _healthSystem.Hited += () => ChangeState("Hit");
    }

    private void OnDestroy()
    {
        _weaponHandler.WeaponPickedUp -= () => _hasWeapon = true;
        _weaponHandler.WeaponDropped -= () => _hasWeapon = false;

        _mover.PlayerIsMoving -= () => ChangeState("Run");

        _mover.PlayerIsStay -= () => ChangeState("Stay");

        _jumper.PlayerInAir -= () =>
        {
            ChangeState("Jump");
            _isInAir = true;
        };
        _jumper.PlayerOnGround -= () =>
        {
            _isInAir = false;
        };

        _healthSystem.Hited -= () => ChangeState("Hit");
    }

    private void InitializeComponents()
    {
        _hasWeapon = false;
        _isInAir = false;

        _healthSystem = GetComponent<HealthSystem>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _weaponHandler = GetComponent<WeaponHandler>();

        Assert.IsNotEmpty(postfixNameForAnimationWithHoldingWeapon, "(Класс AnimationChanger). Постфикс имя для анимаций с оружием на задано.");
    }

    private void ChangeState(string stateName)
    {
        string animationToPlay;

        if (_isInAir)
        {
            animationToPlay = _hasWeapon ? $"Jump{postfixNameForAnimationWithHoldingWeapon}" : "Jump";
        }
        else
        {
            animationToPlay = _hasWeapon ? $"{stateName}{postfixNameForAnimationWithHoldingWeapon}" : stateName;
        }

        _animator.Play(animationToPlay);
    }
}

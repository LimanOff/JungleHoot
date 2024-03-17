using ModestTree;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    public event Action<float> PlayerMovementChanged;

    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Transform _playerBody;

    [Header("Characteristics")]
    [Range(12f, 20f)] public int Speed;

    private float _horizontalMovement;
    private Vector2 _movementDirection;

    private PlayerInputController _inputController;

    [Inject]
    private void Construct(PlayerInputController inputController)
    {
        _inputController = inputController;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        ValidateComponents();
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_playerBody,"(Mover/ValidateComponents) Тело игрока не было задано.");
    }

    private void Update()
    {
        ReadMovement();
    }
    private void FixedUpdate()
    {
        _movementDirection = new Vector2(_horizontalMovement * Speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = _movementDirection;
    }

    private void Flip(float movementDirection)
    {
        if (movementDirection > 0)
        {
            _playerBody.localScale = new Vector3(1, 1, 1);
        }
        else if (movementDirection < 0)
        {
            _playerBody.localScale = new Vector3(1 * -1, 1, 1 * -1);
        }
    }

    private void ReadMovement()
    {
        if (gameObject.name == "Player 1")
        {
            _horizontalMovement = _inputController.GameInput.Player1.Move.ReadValue<float>();
        }
        else
        {
            _horizontalMovement = _inputController.GameInput.Player2.Move2.ReadValue<float>();
        }
        Flip(_horizontalMovement);

        PlayerMovementChanged?.Invoke(_horizontalMovement);
    }
}

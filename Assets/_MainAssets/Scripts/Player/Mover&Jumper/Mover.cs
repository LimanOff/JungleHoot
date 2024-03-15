using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    public event Action PlayerIsMoving;
    public event Action PlayerIsStay;

    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Transform _playerBody;

    [Header("Characteristics")]
    [Range(12f, 20f)] public int Speed;

    private float _horizontalMovement;
    private Vector2 _movementDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
            _horizontalMovement = PlayerInputController.GameInput.Player1.Move.ReadValue<float>();
        }
        else
        {
            _horizontalMovement = PlayerInputController.GameInput.Player2.Move2.ReadValue<float>();
        }
        Flip(_horizontalMovement);

        if (_horizontalMovement == 0)
            PlayerIsStay?.Invoke();
        else
            PlayerIsMoving?.Invoke();
    }
}

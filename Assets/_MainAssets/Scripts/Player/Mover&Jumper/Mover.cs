using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Transform _playerBody;

    [Header("Characteristics")]
    [Range(8f, 12f)] public int Speed;

    private float _horizontalMovement;
    private Vector2 _movementDirection;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.action.ReadValue<Vector2>());
        _horizontalMovement = 0;
        
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
            _horizontalMovement = _playerInput.Player1.Move.ReadValue<float>();
        }
        else
        {
            _horizontalMovement = _playerInput.Player2.Move.ReadValue<float>();
        }
        Flip(_horizontalMovement);
    }
}

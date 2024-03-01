using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    public delegate void PlayerJumpedHandler();
    public event PlayerJumpedHandler PlayerJumped;

    [SerializeField] private LayerMask JumpableObjects;
    private Rigidbody2D _rigidbody2D;
    public float JumpHeight;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (gameObject.name == "Player 1")
        {
            _playerInput.Player1.Jump.performed += OnJump;
        }
        else
        {
            _playerInput.Player2.Jump.performed += OnJump;
        }
    }

    private void OnDestroy()
    {
        if (gameObject.name == "Player 1")
        {
            _playerInput.Player1.Jump.performed -= OnJump;
        }
        else
        {
            _playerInput.Player2.Jump.performed -= OnJump;
        }
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Jump()
    {
        float jumpForce = (Mathf.Sqrt(JumpHeight * (Physics2D.gravity.y * _rigidbody2D.gravityScale) * -2)) * _rigidbody2D.mass;
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        PlayerJumped?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (CalcCanJump())
            Jump();
    }

    private bool CalcCanJump()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, .7f, JumpableObjects))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

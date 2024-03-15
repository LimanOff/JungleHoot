using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    public event Action PlayerStartJump;
    public event Action PlayerInAir;
    public event Action PlayerOnGround;
    public event Action PlayerJumped;

    [SerializeField] private LayerMask JumpableObjects;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    public float JumpHeight;


    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (gameObject.name == "Player 1")
        {
            PlayerInputController.GameInput.Player1.Jump.performed += OnJump;
        }
        else
        {
            PlayerInputController.GameInput.Player2.Jump2.performed += OnJump;
        }
    }

    private void OnDestroy()
    {
        if (gameObject.name == "Player 1")
        {
            PlayerInputController.GameInput.Player1.Jump.performed -= OnJump;
        }
        else
        {
            PlayerInputController.GameInput.Player2.Jump2.performed -= OnJump;
        }
    }

    private void Jump()
    {
        PlayerStartJump?.Invoke();

        float jumpForce = (Mathf.Sqrt(JumpHeight * (Physics2D.gravity.y * _rigidbody2D.gravityScale) * -2)) * _rigidbody2D.mass;
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        PlayerJumped?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsOnSurface())
            Jump();
    }

    private bool IsOnSurface()
    {
        float colliderBottom = _boxCollider2D.bounds.min.y;
        float offset = .1f;
        Vector2 rayOrigin = new Vector2(transform.position.x, colliderBottom - offset);

        if (Physics2D.Raycast(rayOrigin, Vector2.down, .7f, JumpableObjects))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (!IsOnSurface())
        {
            PlayerInAir?.Invoke();
        }
        else
        {
            PlayerOnGround?.Invoke();
        }
    }
}

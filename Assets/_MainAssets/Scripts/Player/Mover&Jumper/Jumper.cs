using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    public KeyCode JumpKey;

    public delegate void PlayerJumpedHandler();
    public event PlayerJumpedHandler PlayerJumped;

    [SerializeField] private LayerMask JumpableObjects;
    private Rigidbody2D _rigidbody2D;
    public float JumpHeight;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(JumpKey) && CalcCanJump())
        {
            Jump();
        }
    }

    private void Jump()
    {
        float jumpForce = (Mathf.Sqrt(JumpHeight * (Physics2D.gravity.y * _rigidbody2D.gravityScale) * -2)) * _rigidbody2D.mass;
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        PlayerJumped?.Invoke();
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

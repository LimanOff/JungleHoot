using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    public KeyCode JumpKey;

    [Range(8f,9f)]
    public float JumpForce;

    private Rigidbody2D _rigidbody;

    [Header("Debug")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private LayerMask jumpableObjectsMask;
    [SerializeField] private Transform _rayOrigin;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = IsOnTheGroundOrNot();
    }

    private void Update()
    {
        if(Input.GetKeyDown(JumpKey) && _isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * JumpForce,ForceMode2D.Impulse);
    }

    private bool IsOnTheGroundOrNot()
    {
        float distanceToCheck = .3f;

        if(Physics2D.Raycast(_rayOrigin.position, Vector2.down, distanceToCheck, jumpableObjectsMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

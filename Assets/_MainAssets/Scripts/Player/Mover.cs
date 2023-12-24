using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("Input")]
    public KeyCode RightKey;
    public KeyCode LeftKey;

    [Header("Characteristics")]
    [Range(8f,12f)] public int Speed;

    private float _horizontalMovement;
    private Vector2 _movementDirection;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKey(RightKey))
        {
            _horizontalMovement = 1;
            Flip(_horizontalMovement);
        }

        if (Input.GetKey(LeftKey))
        {
            _horizontalMovement = -1;
            Flip(_horizontalMovement);
        }

        if (Input.GetKeyUp(RightKey) || Input.GetKeyUp(LeftKey))
        {
            _horizontalMovement = 0;
        }
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(movementDirection < 0)
        {
            transform.localScale = new Vector3(1 * -1, 1, 1 * -1);
        }
    }
}

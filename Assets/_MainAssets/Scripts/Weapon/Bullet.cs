using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    private Rigidbody2D _rigidBody2d;

    private void Awake()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, 0.1f);

        if (hit2D.collider != null)
        {
            BlowUp();
        }

        transform.Translate(Vector3.right * BulletSpeed * Time.deltaTime);
    }

    public virtual void BlowUp()
    {
        Debug.Log("���");
        Destroy(gameObject);
    }
}

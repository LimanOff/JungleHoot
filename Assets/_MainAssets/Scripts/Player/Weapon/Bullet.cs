using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Range(60f, 65f)]
    public float BulletSpeed;
    public int Damage;

    private RaycastHit2D _hit2D;

    private void Update()
    {
        MoveBullet();
        HandleCollision();
    }

    public void BlowUp()
    {
        Destroy(gameObject);
    }

    private void HandleCollision()
    {
        if (_hit2D = Physics2D.Raycast(transform.position, transform.up, 0.1f))
        {
            var hittenGO = _hit2D.collider.gameObject;

            if (hittenGO.tag == "Player")
            {
                hittenGO.GetComponent<HealthSystem>().TakeDamage(Damage);
                BlowUp();
            }
            if (hittenGO.tag == "Obstacle")
            {
                BlowUp();
            }
        }
    }

    private void MoveBullet()
    {
        transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
    }
}

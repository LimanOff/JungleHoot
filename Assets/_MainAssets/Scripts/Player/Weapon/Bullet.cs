using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Range(20f, 25f)]
    public float BulletSpeed;
    public int Damage;

    private void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up, 0.1f);

        if (hit2D.collider != null)
        {
            var hittenGO = hit2D.collider.gameObject;

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

        transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
    }

    public void BlowUp()
    {
        Destroy(gameObject);
    }
}

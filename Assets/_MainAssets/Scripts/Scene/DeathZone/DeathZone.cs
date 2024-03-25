using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthSystem playerHS = collision.gameObject.GetComponent<HealthSystem>();
            playerHS.TakeDamage(playerHS.MaxHealth);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthSystem playerHS = collision.gameObject.GetComponent<HealthSystem>();
            playerHS.DeactivateInvincibleMode();
            playerHS.TakeDamage(playerHS.MaxHealth);
        }
    }
}

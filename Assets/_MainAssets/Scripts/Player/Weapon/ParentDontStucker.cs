using UnityEngine;

public class ParentDontStucker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform parentTransform = gameObject.transform;

            while (parentTransform.parent != null)
            {
                if (parentTransform.parent.name == "Players")
                    break;

                parentTransform = parentTransform.parent;
            }

            string parentGameObjectName = parentTransform.name;

            if (collision.gameObject.name == parentGameObjectName)
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        }
    }
}

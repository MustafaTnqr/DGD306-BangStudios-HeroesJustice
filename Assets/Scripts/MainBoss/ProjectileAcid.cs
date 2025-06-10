using UnityEngine;

public class ProjectileAcid : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection;
    private bool hasHit = false;
    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return; 

        if (other.CompareTag("Player"))
        {
            hasHit = true;

            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}

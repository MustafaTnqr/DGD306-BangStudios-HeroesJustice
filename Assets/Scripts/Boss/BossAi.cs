using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public int damageAmount = 1;
    public float damageCooldown = 1f; // saniye cinsinden

    private bool movingToB = true;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    private float lastDamageTime = -999f; // son hasar zamaný

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Transform target = movingToB ? pointB : pointA;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if ((movingToB && transform.position.x >= pointB.position.x - 0.1f) ||
            (!movingToB && transform.position.x <= pointA.position.x + 0.1f))
        {
            movingToB = !movingToB;
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                Health hp = other.GetComponent<Health>();
                if (hp != null)
                {
                    hp.TakeDamage(damageAmount);
                    lastDamageTime = Time.time; // hasar zamaný güncelle
                }
            }
        }
    }
}

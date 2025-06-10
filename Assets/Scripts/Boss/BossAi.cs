using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public int damageAmount = 1;
    public float damageCooldown = 1f; 

    private bool movingToB = true;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    private float lastDamageTime = -999f; 

    private AudioSource audioSource;

    [Header("Yürüme Sesi")]
    public AudioClip walkClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

        
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
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
                    lastDamageTime = Time.time; 
                }
            }
        }
    }
}

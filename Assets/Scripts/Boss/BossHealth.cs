using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    public float destroyDelay = 1.5f;

    [Header("Anahtar Ayarlarý")]
    public GameObject keyPrefab; // Anahtar prefabýný buraya sürükle
    public Transform dropPoint;  // Anahtarýn düþeceði yer (Yoksa boss'un pozisyonu kullanýlýr)

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Boss damage aldý! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isDead");
            Invoke(nameof(DisableAnimator), 0.1f);
        }

        // Anahtar düþürme
        Vector3 spawnPos = dropPoint != null ? dropPoint.position : transform.position;
        Instantiate(keyPrefab, spawnPos, Quaternion.identity);

        Destroy(gameObject, destroyDelay);
    }

    void DisableAnimator()
    {
        if (animator != null)
            animator.enabled = false;
    }
}

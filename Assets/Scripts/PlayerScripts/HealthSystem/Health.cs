using UnityEngine;

public class Health : MonoBehaviour  //Youtube videosundan bakılarak yapıldı
{
    public float startingHealth = 3f;
    public float currentHealth { get; private set; }
    public bool isDead { get; private set; }
    private Animator animator;
    private bool invulnerable = false;

    private SpriteRenderer spriteRenderer; 

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();

        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable || isDead)
            return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!isDead)
                Die();
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);

        
        if (TryGetComponent<PlayerMovement>(out var movement))
            movement.enabled = false;

        if (TryGetComponent<SwordPlayerController>(out var swordMovement))
            swordMovement.enabled = false;

        if (TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = Vector2.zero;

        
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null && gm.deathScreenUI != null)
        {
            gm.deathScreenUI.SetActive(true);
        }
    }

    private System.Collections.IEnumerator Invulnerability()
    {
        invulnerable = true;

        // 🔥 Yanıp sönme efekti
        float flashDelay = 0.1f;
        int flashCount = 5;

        for (int i = 0; i < flashCount; i++)
        {
            if (spriteRenderer != null)
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f); 

            yield return new WaitForSeconds(flashDelay);

            if (spriteRenderer != null)
                spriteRenderer.color = Color.white; 

            yield return new WaitForSeconds(flashDelay);
        }

        invulnerable = false;
    }
}

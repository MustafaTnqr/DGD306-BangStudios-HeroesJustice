using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float startingHealth = 3f;
    public float currentHealth { get; private set; }

    [Header("Death Animation")]
    [Tooltip("Ölüm animasyonunuzun toplam süresi (saniye)")]
    public float deathAnimationDuration = 1.2f;

    public bool isDead { get; private set; }

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool invulnerable = false;

    void Awake()
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

        // 1) Ölüm animasyonunu tetikle
        animator.SetBool("isDead", true);

        // 2) Hareket/girdi devre dışı
        if (TryGetComponent<PlayerMovement>(out var move)) move.enabled = false;
        if (TryGetComponent<SwordPlayerController>(out var sword)) sword.enabled = false;
        if (TryGetComponent<Rigidbody2D>(out var rb)) rb.velocity = Vector2.zero;

        // 3) Animasyon süresince bekle, sonra death screen’i aç
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // Ölüm animasyonu oynarken zamanı normal bırak (Time.timeScale = 1)
        yield return new WaitForSecondsRealtime(deathAnimationDuration);

        // Şimdi kullanıcıya death screen göster ve zamanı durdur
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null && gm.deathScreenUI != null)
        {
            gm.deathScreenUI.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        float flashDelay = 0.1f, elapsed = 0f;
        int flashCount = 5;

        for (int i = 0; i < flashCount; i++)
        {
            if (spriteRenderer != null)
                spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDelay);
            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDelay);
        }
        invulnerable = false;
    }
}

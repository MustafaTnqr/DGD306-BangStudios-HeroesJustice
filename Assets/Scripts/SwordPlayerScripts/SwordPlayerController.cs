using UnityEngine;
using UnityEngine.UI;

public class SwordPlayerController : MonoBehaviour
{
    public bool canMove = true;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded;

    [Header("Shuriken UI")]
    public Image shurikenCooldownImage;

    [Header("Shuriken Sesi")]
    public AudioClip shurikenThrowSound;


    [Header("Shuriken Cooldown")]
    public float shurikenCooldown = 3f;
    private float shurikenTimer = -999f;

    [Header("Shuriken")]
    public GameObject shurikenPrefab;
    public Transform shurikenSpawnPoint;
    public float shurikenForce = 20f;
    public float shurikenSpeed = 20f;

    [Header("Yürüyüş Sesi")]
    public AudioClip defaultWalkClip;
    public AudioClip woodWalkClip;
    private string currentSurface = "Default";
    private float walkSoundCooldown = 0.3f;
    private float walkSoundTimer = 0f;

    [Header("Saldırı Sesi")]
    public AudioClip swordSwingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("isWalking", false);
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f);

        // Yürüyüş sesi
        walkSoundTimer -= Time.deltaTime;
        if (Mathf.Abs(moveInput) > 0.1f && isGrounded && walkSoundTimer <= 0f)
        {
            if (AudioManager.Instance != null)
            {
                AudioClip clipToPlay = (currentSurface == "Wood") ? woodWalkClip : defaultWalkClip;
                AudioManager.Instance.PlaySFX(clipToPlay, 0.1f);
            }

            walkSoundTimer = walkSoundCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");

            if (AudioManager.Instance != null && AudioManager.Instance.sfxSource != null && swordSwingSound != null)
            {
                AudioManager.Instance.PlaySFX(swordSwingSound, 0.6f);
            }
        }

        shurikenTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) && shurikenTimer <= 0f)
        {
            ThrowShuriken();
            shurikenTimer = shurikenCooldown;
        }

        if (shurikenCooldownImage != null)
        {
            float cooldownProgress = 1f - (shurikenTimer / shurikenCooldown);
            cooldownProgress = Mathf.Clamp01(cooldownProgress);
            shurikenCooldownImage.fillAmount = cooldownProgress;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodFloor"))
        {
            currentSurface = "Wood";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WoodFloor"))
        {
            currentSurface = "Default";
        }
    }

    void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Boss"))
            {
                BossHealth boss = enemy.GetComponent<BossHealth>();
                if (boss != null)
                    boss.TakeDamage(1);
            }
            else
            {
                if (AudioManager.Instance != null)
                {
                    if (enemy.GetComponent<ZombieIdentifier>() != null)
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.zombieDeath);
                    else if (enemy.GetComponent<SkeletonIdentifier>() != null)
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.skeletonDeath);
                    else if (enemy.GetComponent<BatIdentifier>() != null)
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.batDeath);
                }

                Destroy(enemy.gameObject);
            }
        }
    }

    void ThrowShuriken()
    {
        if (shurikenPrefab != null && shurikenSpawnPoint != null)
        {
            GameObject shuriken = Instantiate(shurikenPrefab, shurikenSpawnPoint.position, Quaternion.identity);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector2 direction = (mousePos - shurikenSpawnPoint.position).normalized;

            Rigidbody2D rb = shuriken.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * shurikenSpeed;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                shuriken.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            shuriken.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

            if (AudioManager.Instance != null && shurikenThrowSound != null)
            {
                AudioManager.Instance.PlaySFX(shurikenThrowSound, 0.4f);
            }
        }
    }
}

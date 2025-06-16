using UnityEngine;
using UnityEngine.UI;

public class SwordPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public bool canMove = true;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Melee Attack")]
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public AudioClip swordSwingSound;

    [Header("Shuriken")]
    public GameObject shurikenPrefab;
    public Transform shurikenSpawnPoint;
    public float shurikenForce = 20f;       // artık kullanılmıyor
    public float shurikenSpeed = 20f;
    public float shurikenCooldown = 3f;
    public Image shurikenCooldownImage;
    public AudioClip shurikenThrowSound;

    [Header("Footstep SFX")]
    public AudioClip defaultWalkClip;
    public AudioClip woodWalkClip;
    private string currentSurface = "Default";
    private float walkSoundCooldown = 0.3f;
    private float walkSoundTimer = 0f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded;
    private float shurikenTimer = 0f;
    private int facingDirection = 1; // 1=sağ, -1=sol

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        shurikenTimer = shurikenCooldown; // ilk anda atmaya hazır
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
        else rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // --- Movement (D-Pad via Legacy Input Manager) ---
        float moveInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical"); // ihtiyaca göre kullanabilirsin

        // Sprite yönü
        if (moveInput > 0) transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveInput < 0) transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f);

        // Footstep SFX
        walkSoundTimer -= Time.deltaTime;
        if (Mathf.Abs(moveInput) > 0.1f && isGrounded && walkSoundTimer <= 0f)
        {
            AudioClip clip = (currentSurface == "Wood") ? woodWalkClip : defaultWalkClip;
            AudioManager.Instance?.PlaySFX(clip, 0.1f);
            walkSoundTimer = walkSoundCooldown;
        }

        // Jump (Jump input map, örn: Fire2 / joystick button 1)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        // Update facingDirection (melee default direction)
        facingDirection = transform.localScale.x > 0 ? 1 : -1;

        // --- Melee Attack (A = Fire1) ---
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
            AudioManager.Instance?.PlaySFX(swordSwingSound, 0.6f);
        }

        // --- Shuriken Throw (X = Fire3) with cooldown ---
        shurikenTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire3") && shurikenTimer <= 0f)
        {
            ThrowShuriken();
            shurikenTimer = shurikenCooldown;
            AudioManager.Instance?.PlaySFX(shurikenThrowSound, 0.4f);
        }

        // Cooldown UI
        if (shurikenCooldownImage)
        {
            float progress = 1f - Mathf.Clamp01(shurikenTimer / shurikenCooldown);
            shurikenCooldownImage.fillAmount = progress;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodFloor")) currentSurface = "Wood";
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WoodFloor")) currentSurface = "Default";
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
        // 1) D-pad’e bakarak yön belirle
        float aimX = Input.GetAxisRaw("Horizontal");
        float aimY = Input.GetAxisRaw("Vertical");
        Vector2 dir;
        if (Mathf.Abs(aimX) < 0.1f && Mathf.Abs(aimY) < 0.1f)
            dir = new Vector2(facingDirection, 0);
        else
            dir = new Vector2(aimX, aimY).normalized;

        // 2) Shuriken’i spawn et ve SetDirection ile fırlat
        var shuriken = Instantiate(shurikenPrefab, shurikenSpawnPoint.position, Quaternion.identity);
        var proj = shuriken.GetComponent<ShurikenProjectile>();
        if (proj != null)
            proj.SetDirection(dir);

        // 3) Opsiyonel: shuriken sprite yönü
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shuriken.transform.rotation = Quaternion.Euler(0, 0, angle);
        shuriken.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }
}


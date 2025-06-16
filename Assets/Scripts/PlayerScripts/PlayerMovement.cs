using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public AudioClip defaultWalkClip;
    public AudioClip woodWalkClip;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalScale;
    private string currentSurface = "Default";

    public bool canMove = true;
    private bool isGrounded = true;

    private float walkSoundCooldown = 0.3f;
    private float walkSoundTimer = 0f;

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
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("isWalking", false);
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // --- D-Pad üzerinden dijital hareket ---
        float moveInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical"); // dilerseniz aşağı yukarı hareket için de kullanırsınız

        // Yön ve animasyon
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f);

        // --- Jump: "Jump" Input Map (örneğin joystick button 1 / Fire2) ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodFloor"))
            currentSurface = "Wood";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WoodFloor"))
            currentSurface = "Default";
    }
}

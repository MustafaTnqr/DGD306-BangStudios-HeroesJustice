using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded;

    [Header("Yürüyüş Sesi")]
    public AudioSource walkAudioSource;
    public AudioClip defaultWalkClip;
    public AudioClip woodWalkClip;

    private string currentSurface = "Default";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        // 🎧 Zemin türüne göre yürüyüş sesi ve ses yüksekliği
        if (Mathf.Abs(moveInput) > 0.1f && isGrounded)
        {
            if (!walkAudioSource.isPlaying)
            {
                if (currentSurface == "Wood")
                {
                    walkAudioSource.clip = woodWalkClip;
                    walkAudioSource.volume = 0.1f;
                }
                else
                {
                    walkAudioSource.clip = defaultWalkClip;
                    walkAudioSource.volume = 0.03f;
                }

                walkAudioSource.loop = true;
                walkAudioSource.Play();
            }
        }
        else
        {
            if (walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop();
            }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FootstepWood"))
        {
            
            currentSurface = "Wood";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FootstepWood"))
        {
            
            currentSurface = "Default";
        }
    }
}

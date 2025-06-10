using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public AudioClip defaultWalkClip;
    public AudioClip woodWalkClip;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource walkAudioSource;
    private bool isGrounded = true;
    private Vector3 originalScale;
    private string currentSurface = "Default";

    public bool canMove = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        walkAudioSource = GetComponent<AudioSource>();
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

            if (walkAudioSource.isPlaying)
                walkAudioSource.Stop();

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

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

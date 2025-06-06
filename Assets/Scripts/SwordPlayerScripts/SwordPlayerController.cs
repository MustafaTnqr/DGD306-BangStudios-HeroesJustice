﻿using UnityEngine;

public class SwordPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded;

    [Header("Yürüyüş Sesi")]
    public AudioSource walkAudioSource;
    public AudioClip defaultWalkClip;
    public AudioClip woodWalkClip;

    private string currentSurface = "Default";

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
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.01f);

        // 🎧 Zemin türüne göre ses ve volume kontrolü
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
                    walkAudioSource.volume = 0.01f;
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
                AudioManager.Instance.sfxSource.PlayOneShot(swordSwingSound, 0.6f);
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

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
}

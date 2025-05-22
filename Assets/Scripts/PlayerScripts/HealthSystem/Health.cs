using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    public void TakeDamage(float _damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(DamageFlash());
        }
        else
        {
            Die();
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);

        if (currentHealth > 0 && isDead)
        {
            isDead = false;
            animator.SetBool("isDead", false);
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);

        
        if (TryGetComponent<PlayerMovement>(out var movement))
            movement.enabled = false;

        
        if (TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = Vector2.zero;
    }

    private IEnumerator DamageFlash()
    {
        int flashCount = 3;
        float flashDuration = 0.1f;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }

        spriteRenderer.color = Color.white;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
}

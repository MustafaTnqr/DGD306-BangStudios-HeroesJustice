using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            isDead = false;
    }

    private void Die()
    {
        isDead = true;
        
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

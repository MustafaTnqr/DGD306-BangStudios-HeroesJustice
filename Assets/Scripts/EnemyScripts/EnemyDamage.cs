using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageCooldown = 1.0f; 
    private bool canDealDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDealDamage)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            canDealDamage = false;
            Invoke(nameof(ResetDamage), damageCooldown);
        }
    }

    private void ResetDamage()
    {
        canDealDamage = true;
    }
}

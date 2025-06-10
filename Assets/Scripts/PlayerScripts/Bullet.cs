using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public float lifetime = 3f;

    void Start()
    {
        float angle = Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;  //Mermi yönünü ayarlamak için AI dan destek alýndý
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            
            BossHealth bh = collision.GetComponent<BossHealth>();
            if (bh != null)
            {
                bh.TakeDamage(1);
            }

            
            MainBossHealth mbh = collision.GetComponent<MainBossHealth>();
            if (mbh != null)
            {
                mbh.TakeDamage(1);
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            
            if (collision.GetComponent<ZombieIdentifier>() != null)
            {
                AudioManager.Instance?.PlaySFX(AudioManager.Instance.zombieDeath);
            }
            
            else if (collision.GetComponent<SkeletonIdentifier>() != null)
            {
                AudioManager.Instance?.PlaySFX(AudioManager.Instance.skeletonDeath);
            }
            
            else if (collision.GetComponent<BatIdentifier>() != null)
            {
                AudioManager.Instance?.PlaySFX(AudioManager.Instance.batDeath);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

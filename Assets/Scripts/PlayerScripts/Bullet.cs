using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        float angle = Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
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

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            // Zombi sesi
            if (collision.GetComponent<ZombieIdentifier>() != null)
            {
                AudioManager.Instance?.PlaySFX(AudioManager.Instance.zombieDeath);
            }
            // Ýskelet sesi
            else if (collision.GetComponent<SkeletonIdentifier>() != null)
            {
                AudioManager.Instance?.PlaySFX(AudioManager.Instance.skeletonDeath);
            }
            // Yarasa sesi
            else if (collision.GetComponent<BatIdentifier>() != null)
            {
                AudioManager.Instance?.PlaySFX(AudioManager.Instance.batDeath);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

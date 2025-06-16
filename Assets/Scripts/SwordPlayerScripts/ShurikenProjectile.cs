using UnityEngine;

public class ShurikenProjectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 3;
    public float lifeTime = 3f;

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // World uzayýnda doðrusal hareket et
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Kendi etrafýnda dönme (spin) efekti
        transform.Rotate(0f, 0f, -720f * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            
            if (collision.CompareTag("Enemy"))
            {
                if (AudioManager.Instance != null)
                {
                    if (collision.GetComponent<ZombieIdentifier>() != null)
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.zombieDeath);
                    else if (collision.GetComponent<SkeletonIdentifier>() != null)
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.skeletonDeath);
                    else if (collision.GetComponent<BatIdentifier>() != null)
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.batDeath);
                }

                Destroy(collision.gameObject);
            }

            
            if (collision.CompareTag("Boss"))
            {
                BossHealth boss = collision.GetComponent<BossHealth>();
                if (boss != null)
                    boss.TakeDamage(damage);

                MainBossHealth mainBoss = collision.GetComponent<MainBossHealth>();
                if (mainBoss != null)
                    mainBoss.TakeDamage(damage);
            }

            
            Destroy(gameObject);
        }
    }

}

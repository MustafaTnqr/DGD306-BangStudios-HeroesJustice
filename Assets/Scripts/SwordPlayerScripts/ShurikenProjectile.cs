using UnityEngine;

public class ShurikenProjectile : MonoBehaviour
{
    public float speed = 10f;
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
        transform.Translate(direction * speed * Time.deltaTime);
        
 
        transform.Rotate(0f, 0f, -720f * Time.deltaTime); // Saat yönünde hýzlý döner
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            if (collision.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("Boss"))
            {
                BossHealth boss = collision.GetComponent<BossHealth>();
                if (boss != null)
                    boss.TakeDamage(damage);
            }

            if (collision.CompareTag("Boss"))
            {
                MainBossHealth mainBoss = collision.GetComponent<MainBossHealth>();
                if (mainBoss != null)
                    mainBoss.TakeDamage(damage);
            }

            else
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject); 
        }
    }
}

using UnityEngine;

public class BossController : MonoBehaviour
{
    public Animator animator;
    public Transform targetPosition;
    public float descendSpeed = 2f;
    private bool isDescending = false;
    private bool isActive = false;

    [Header("Health Bar")]
    public GameObject bossHealthBarUI;

    [Header("Acid Attack Settings")]
    public GameObject acidProjectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDescending)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, descendSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
            {
                isDescending = false;
                animator.SetTrigger("StartFight");
                isActive = true;


                MainBossHealth health = GetComponent<MainBossHealth>();
                if (health != null)
                {
                    health.ShowHealthBar();
                }
            }
        }

        if (isActive && Time.time > nextFireTime)
        {
            FireAcid();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void ActivateBoss()
    {
        if (!isActive)
        {
            isDescending = true;
        }
    }

    void FireAcid()
    {
        if (acidProjectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(acidProjectilePrefab, firePoint.position, Quaternion.identity);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 direction = (player.transform.position - firePoint.position).normalized;
                projectile.GetComponent<ProjectileAcid>().Initialize(direction);
            }
        }
    }
}

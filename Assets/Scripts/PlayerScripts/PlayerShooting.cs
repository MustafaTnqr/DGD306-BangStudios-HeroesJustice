using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public float fireRate = 0.2f;
    private float nextFireTime;

    private Vector3 originalScale;
    private PlayerMovement playerMovement;
    private int facingDirection = 1; // 1 = sağ, -1 = sol

    void Start()
    {
        originalScale = transform.localScale;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement != null && !playerMovement.canMove)
            return;

        // Karakter yönünü güncelle (yalnızca sprite yönü için)
        facingDirection = (transform.localScale.x > 0) ? 1 : -1;

        // Ateş tuşuna basıldıysa
        if (Time.time >= nextFireTime && Input.GetButton("Fire1"))
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet()
    {
        // 1) D-Pad’den anlık okuma:
        float aimX = Input.GetAxisRaw("Horizontal");  // D-Pad x
        float aimY = Input.GetAxisRaw("Vertical");    // D-Pad y

        Vector2 shootDir;

        // 2) Eğer hiçbir yöne basılmıyorsa, karakterin baktığı yönde at
        if (Mathf.Abs(aimX) < 0.1f && Mathf.Abs(aimY) < 0.1f)
        {
            shootDir = new Vector2(facingDirection, 0);
        }
        else
        {
            // D-pad’e basılmışsa o yöne atış yap
            shootDir = new Vector2(aimX, aimY).normalized;
        }

        // 3) Mermiyi spawn et ve hızını ayarla
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir * bulletSpeed;

        // (Opsiyonel) Merminin rotasyonunu da shootDir’e göre ayarlamak istersen:
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 4) Ses efekti
        if (AudioManager.Instance != null && AudioManager.Instance.pistolShot != null)
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pistolShot);
    }

}

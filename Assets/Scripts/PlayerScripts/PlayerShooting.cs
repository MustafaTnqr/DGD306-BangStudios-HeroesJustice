using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public float fireRate = 0.2f;
    private float nextFireTime;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Time.time >= nextFireTime && Input.GetMouseButton(0))
        {
            Vector2 direction = GetMouseDirection();
            RotateToMouse(direction);
            if (direction.magnitude > 0.1f)
            {
                FireBullet(direction.normalized);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - firePoint.position;
        return direction.normalized;
    }

    void RotateToMouse(Vector2 direction)
    {
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    }

    void FireBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        //  Tabanca sesi çal 
        if (AudioManager.Instance != null && AudioManager.Instance.pistolShot != null)
        {
            AudioSource.PlayClipAtPoint(AudioManager.Instance.pistolShot, transform.position, 0.1f); // %10 ses
        }
    }
}

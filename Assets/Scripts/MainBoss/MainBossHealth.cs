using UnityEngine;
using UnityEngine.UI;

public class MainBossHealth : MonoBehaviour //Can sistemini ui ile birleþtirmek için ai tarafýndan destek alýndý
{
    public int maxHealth = 20;
    private int currentHealth;

    [Header("UI")]
    public GameObject healthBarUI;
    public Image healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarUI != null)
            healthBarUI.SetActive(false);

        UpdateHealthBar();
    }

    public void ShowHealthBar()
    {
        if (healthBarUI != null)
            healthBarUI.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        if (healthBarUI != null)
            healthBarUI.SetActive(false);

        Debug.Log("BOSS DEAD - Opening Mission Complete UI");

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null && gm.missionCompleteUI != null)
        {
            gm.missionCompleteUI.SetActive(true);
        }

        // Destroy iþlemini biraz geciktiriyoruz ki UI rahatça görünsün
        Invoke(nameof(DestroyBoss), 0.1f);
    }

    void DestroyBoss()
    {
        Destroy(gameObject);
    }
}

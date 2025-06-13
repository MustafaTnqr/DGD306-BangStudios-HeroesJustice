using UnityEngine;
using UnityEngine.UI;

public class MainBossHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private bool isDead = false;

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
        if (isDead) return;

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
        if (isDead) return;
        isDead = true;

        if (healthBarUI != null)
            healthBarUI.SetActive(false);

        
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.Invoke("ShowMissionCompleteUI", 2f); 

        Destroy(gameObject); 
    }



    void ShowMissionCompleteUI()
    {

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null && gm.missionCompleteUI != null)
        {
            gm.missionCompleteUI.SetActive(true);
        }
    }

    void DestroyBoss()
    {
        Destroy(gameObject);
    }
}

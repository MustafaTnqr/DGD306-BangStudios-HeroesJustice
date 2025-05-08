using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalheathBar;
    [SerializeField] private Image currenthealthBar;
    
    void Start()
    {
        totalheathBar.fillAmount = playerHealth.currentHealth / 10;
    }

    
    void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}

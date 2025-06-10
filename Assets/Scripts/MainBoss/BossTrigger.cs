using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private bool triggered = false;
    public GameObject doorToActivate;
    public AudioClip bossMusic;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            BossController boss = FindObjectOfType<BossController>();
            if (boss != null)
                boss.ActivateBoss();

            if (doorToActivate != null)
                doorToActivate.SetActive(true);

            if (bossMusic != null)
                AudioManager.Instance.PlayMusic(bossMusic);
        }
    }
}

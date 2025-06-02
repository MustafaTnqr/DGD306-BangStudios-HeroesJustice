using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleDoor : MonoBehaviour
{
    public string nextSceneName = "Level2";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerInventory.instance.hasKey)
            {
                // Anahtar var → sahne değiştir
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("Kapı kilitli, anahtar lazım!");
                // Buraya UI ile "Anahtar lazım!" yazısı da çıkarabilirsin.
            }
        }
    }
}
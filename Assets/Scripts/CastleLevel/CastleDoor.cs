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
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                KeyWarningUI.instance.ShowMessage("You Need To Find A Key");  //UI Gosterme için ai yardımı alındı
            }

        }
    }
}
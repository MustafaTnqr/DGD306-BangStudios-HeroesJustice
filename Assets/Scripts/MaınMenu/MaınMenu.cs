using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour  //Menude seslerin düzgün çalmasý için delay eklendi bunun için gptden yardým alýndý
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayGame()
    {
        StartCoroutine(PlayClickAndLoad("CharacterSelection"));
    }

    public void OpenOptions()
    {
        StartCoroutine(PlayClickOnly(() => {
            Debug.Log("Options menüsü henüz yapýlmadý.");
        }));
    }

    public void OpenCredits()
    {
        StartCoroutine(PlayClickAndLoad("Credits"));
    }

    public void QuitGame()
    {
        StartCoroutine(PlayClickAndQuit());
    }

    

    IEnumerator PlayClickAndLoad(string sceneName)
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator PlayClickAndQuit()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    IEnumerator PlayClickOnly(System.Action actionAfter)
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(1f);
        actionAfter?.Invoke();
    }
}

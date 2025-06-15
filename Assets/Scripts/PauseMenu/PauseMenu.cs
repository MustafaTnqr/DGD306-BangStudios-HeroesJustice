using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    public GameObject optionsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            Destroy(player);

        SceneManager.LoadScene("MainMenu");
    }

    public void CharachterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
    public void OpenOptions()
    {
        pauseUI.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pauseUI.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

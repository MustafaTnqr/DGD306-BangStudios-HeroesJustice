using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    public GameObject optionsPanel;

    [Header("UI Navigation")]
    public Button resumeButton;    // Inspector�a drag-drop ile atay�n
    public Button optionsButton;   // (Opsiyonel) se�enekler butonu
    public Button quitButton;      // (Opsiyonel) ��k�� butonu

    void Update()
    {
        // Start/Escape ile a�-kapa
        bool pausePressed = (Gamepad.current?.startButton.wasPressedThisFrame ?? false)
                         || (Keyboard.current?.escapeKey.wasPressedThisFrame ?? false);

        if (pausePressed)
        {
            if (isPaused) Resume();
            else Pause();
            return;
        }

        if (!isPaused)
            return;

        // B (East) ile geri
        if (Gamepad.current.buttonEast.wasPressedThisFrame
         || (Keyboard.current.backspaceKey.wasPressedThisFrame))
        {
            if (optionsPanel.activeInHierarchy)
                CloseOptions();
            else
                Resume();
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        optionsPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        // **�lk se�ili butonu ayarla** (D-Pad navigasyonu i�in)
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }

   
        public void LoadMainMenu()
    {
        // 1) Kesinlikle zaman� normale d�nd�r
        Time.timeScale = 1f;
        isPaused = false;

        
        var player = GameObject.FindWithTag("Player");
         if (player != null) Destroy(player);

        
        SceneManager.LoadScene("MainMenu");
    }


    public void OpenOptions()
    {
        pauseUI.SetActive(false);
        optionsPanel.SetActive(true);

        // �imdi options panelde gezinmek istersen ilk butonu se�
        EventSystem.current.SetSelectedGameObject(optionsButton.gameObject);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pauseUI.SetActive(true);

        // Pause ana men�s�ne geri d�nerken resume butonunu se�
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

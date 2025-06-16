// YouDiedManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class YouDiedManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject diedUI;           // �You Died� panel gameObject
    public Button tryAgainButton;       // �TRY AGAIN� butonu
    public Button mainMenuButton;       // �MAIN MENU� butonu

    void OnEnable()
    {
        // Paneli a� ve durdur gerekiyorsa oyunu durdur
        if (diedUI != null)
            diedUI.SetActive(true);
        Time.timeScale = 0f;

        // Gamepad navigasyonu i�in �ncelikle TRY AGAIN butonunu se�
        if (tryAgainButton != null)
            EventSystem.current.SetSelectedGameObject(tryAgainButton.gameObject);
    }

    void Update()
    {
        // Panel g�r�n�rken input�u dinle
        if (diedUI == null || !diedUI.activeInHierarchy)
            return;

        // A (buttonSouth) ile se�ili butona t�kla
        if (Gamepad.current?.buttonSouth.wasPressedThisFrame == true ||
            Keyboard.current?.enterKey.wasPressedThisFrame == true)
        {
            var sel = EventSystem.current.currentSelectedGameObject;
            if (sel == tryAgainButton.gameObject)
                OnTryAgain();
            else if (sel == mainMenuButton.gameObject)
                OnMainMenu();
        }

        // B (buttonEast) ile Main Menu�ye d�n
        if (Gamepad.current?.buttonEast.wasPressedThisFrame == true ||
            Keyboard.current?.escapeKey.wasPressedThisFrame == true)
        {
            OnMainMenu();
        }
    }

    public void OnTryAgain()
    {
        // 1) Oyunu devam ettir
        Time.timeScale = 1f;

        // 2) Mevcut Player objesini bulup yok et
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            Destroy(player);

        // 3) Se�im ekran�n� y�kle (tekil modda, eski sahneleri temizleyerek)
        SceneManager.LoadScene("CharacterSelection", LoadSceneMode.Single);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

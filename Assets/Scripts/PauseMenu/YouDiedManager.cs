// YouDiedManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class YouDiedManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject diedUI;           // “You Died” panel gameObject
    public Button tryAgainButton;       // “TRY AGAIN” butonu
    public Button mainMenuButton;       // “MAIN MENU” butonu

    void OnEnable()
    {
        // Paneli aç ve durdur gerekiyorsa oyunu durdur
        if (diedUI != null)
            diedUI.SetActive(true);
        Time.timeScale = 0f;

        // Gamepad navigasyonu için öncelikle TRY AGAIN butonunu seç
        if (tryAgainButton != null)
            EventSystem.current.SetSelectedGameObject(tryAgainButton.gameObject);
    }

    void Update()
    {
        // Panel görünürken input’u dinle
        if (diedUI == null || !diedUI.activeInHierarchy)
            return;

        // A (buttonSouth) ile seçili butona týkla
        if (Gamepad.current?.buttonSouth.wasPressedThisFrame == true ||
            Keyboard.current?.enterKey.wasPressedThisFrame == true)
        {
            var sel = EventSystem.current.currentSelectedGameObject;
            if (sel == tryAgainButton.gameObject)
                OnTryAgain();
            else if (sel == mainMenuButton.gameObject)
                OnMainMenu();
        }

        // B (buttonEast) ile Main Menu’ye dön
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

        // 3) Seçim ekranýný yükle (tekil modda, eski sahneleri temizleyerek)
        SceneManager.LoadScene("CharacterSelection", LoadSceneMode.Single);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

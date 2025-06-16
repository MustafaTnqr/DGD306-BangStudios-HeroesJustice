// MissionCompleteManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MissionCompleteManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject missionCompleteUI;   // Canvas veya panel
    public Button mainMenuButton;          // “MAIN MENU” butonunuz

    void Start()
    {
        // Ýlk baþta gizliyse açýlýyor mu diye kontrol edin
        if (missionCompleteUI != null && !missionCompleteUI.activeInHierarchy)
            missionCompleteUI.SetActive(true);

        // Zamaný durdurmak istersen
        Time.timeScale = 0f;

        // Gamepad navigasyonu için butonu seçili yap
        if (mainMenuButton != null)
            EventSystem.current.SetSelectedGameObject(mainMenuButton.gameObject);
    }

    void Update()
    {
        // Sadece ekranda görünüyorken dinle
        if (missionCompleteUI == null || !missionCompleteUI.activeInHierarchy)
            return;

        // A tuþuna basýnca Main Menu’ye dön
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            OnMainMenu();
        }

        // Klavyeyle de Enter ile çýkýþ istersen
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            OnMainMenu();
        }
    }

    public void OnMainMenu()
    {
        // Zamaný geri al
        Time.timeScale = 1f;

        // Sahne geçiþi
        SceneManager.LoadScene("MainMenu");
    }
}

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
    public Button mainMenuButton;          // �MAIN MENU� butonunuz

    void Start()
    {
        // �lk ba�ta gizliyse a��l�yor mu diye kontrol edin
        if (missionCompleteUI != null && !missionCompleteUI.activeInHierarchy)
            missionCompleteUI.SetActive(true);

        // Zaman� durdurmak istersen
        Time.timeScale = 0f;

        // Gamepad navigasyonu i�in butonu se�ili yap
        if (mainMenuButton != null)
            EventSystem.current.SetSelectedGameObject(mainMenuButton.gameObject);
    }

    void Update()
    {
        // Sadece ekranda g�r�n�yorken dinle
        if (missionCompleteUI == null || !missionCompleteUI.activeInHierarchy)
            return;

        // A tu�una bas�nca Main Menu�ye d�n
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            OnMainMenu();
        }

        // Klavyeyle de Enter ile ��k�� istersen
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            OnMainMenu();
        }
    }

    public void OnMainMenu()
    {
        // Zaman� geri al
        Time.timeScale = 1f;

        // Sahne ge�i�i
        SceneManager.LoadScene("MainMenu");
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Character Switcher")]
    public CharacterSwitcher characterSwitcher;

    private float inputCooldown = 0.3f;
    private float inputTimer = 0f;

    void Update()
    {
        if (Gamepad.current == null) return;

        inputTimer -= Time.unscaledDeltaTime;

        // Karakter değişimi için ←/→ D-Pad veya joystick
        float moveInput = Gamepad.current.leftStick.x.ReadValue();
        float dpadInput = Gamepad.current.dpad.x.ReadValue();

        if (inputTimer <= 0f)
        {
            if (moveInput > 0.5f || dpadInput > 0.5f)
            {
                characterSwitcher.ShowNext();
                inputTimer = inputCooldown;
            }
            else if (moveInput < -0.5f || dpadInput < -0.5f)
            {
                characterSwitcher.ShowPrevious();
                inputTimer = inputCooldown;
            }
        }

        // A tuşu → seç ve başlat
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterSwitcher.GetSelectedIndex());
            PlayerPrefs.Save();
            SceneManager.LoadScene("Level1");
        }

        // B tuşu → menüye dön
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

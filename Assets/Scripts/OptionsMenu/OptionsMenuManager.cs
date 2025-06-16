using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class OptionsMenuManager : MonoBehaviour
{
    public ArcadeVolumeBar musicBar;
    public SFXVolumeBar sfxBar;

    private int selectedIndex = 0; // 0 = music, 1 = sfx
    private float switchCooldown = 0.3f;
    private float switchTimer = 0f;

    void Start()
    {
        SelectBar(0); // müzikle baþla
    }

    void Update()
    {
        if (Gamepad.current == null) return;
        switchTimer -= Time.unscaledDeltaTime;

        float vertical = Gamepad.current.dpad.y.ReadValue();
        float stickY = Gamepad.current.leftStick.y.ReadValue();

        if (switchTimer <= 0f)
        {
            if (vertical > 0.5f || stickY > 0.5f)
            {
                selectedIndex = 0;
                SelectBar(0);
                switchTimer = switchCooldown;
            }
            else if (vertical < -0.5f || stickY < -0.5f)
            {
                selectedIndex = 1;
                SelectBar(1);
                switchTimer = switchCooldown;
            }
        }

        // B tuþu ile menüye geri dön
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void SelectBar(int index)
    {
        musicBar.isSelected = (index == 0);
        sfxBar.isSelected = (index == 1);

        // Scale'larý sýfýrla
        musicBar.transform.localScale = musicBar.isSelected ? Vector3.one * 1.1f : Vector3.one;
        sfxBar.transform.localScale = sfxBar.isSelected ? Vector3.one * 1.1f : Vector3.one;
    }
}

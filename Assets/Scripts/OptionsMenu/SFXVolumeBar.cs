using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SFXVolumeBar : MonoBehaviour
{
    public Image[] segments;
    private float volume;
    public bool isSelected = false;
    private float inputCooldown = 0.25f;
    private float inputTimer = 0f;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SFXVolumeLevel", 1f);
        ApplyVolume();
    }

    void Update()
    {
        if (!isSelected) return;

        inputTimer -= Time.unscaledDeltaTime;

        float right = Gamepad.current?.dpad.right.ReadValue() ?? 0f;
        float left = Gamepad.current?.dpad.left.ReadValue() ?? 0f;
        float joy = Gamepad.current?.leftStick.x.ReadValue() ?? 0f;

        if (inputTimer <= 0f)
        {
            if (right > 0.5f || joy > 0.5f)
            {
                volume = Mathf.Clamp01(volume + 0.1f);
                ApplyVolume();
                inputTimer = inputCooldown;
            }

            if (left > 0.5f || joy < -0.5f)
            {
                volume = Mathf.Clamp01(volume - 0.1f);
                ApplyVolume();
                inputTimer = inputCooldown;
            }
        }

        transform.localScale = Vector3.Lerp(
    transform.localScale,
    isSelected ? Vector3.one * 1.1f : Vector3.one,
    Time.unscaledDeltaTime * 8f
);
    }

    void ApplyVolume()
    {
        AudioManager.Instance?.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolumeLevel", volume);
        UpdateBar();
    }

    void OnEnable()
    {
        volume = PlayerPrefs.GetFloat("SFXVolumeLevel", 1f);
        ApplyVolume();
    }

    void UpdateBar()
    {
        int activeCount = Mathf.RoundToInt(volume * segments.Length);
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].color = (i < activeCount) ? Color.cyan : Color.gray;
        }
    }
}

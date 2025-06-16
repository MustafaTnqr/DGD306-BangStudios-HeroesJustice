using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ArcadeVolumeBar : MonoBehaviour
{
    public Image[] segments;
    [Range(0f, 1f)] public float volume = 1f;
    public bool isSelected = true; // baþta seçili olabilir
    private float inputCooldown = 0.25f;
    private float inputTimer = 0f;
    void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVolumeLevel", 1f);
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
        AudioManager.Instance?.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolumeLevel", volume);
        UpdateBar();
    }

    void OnEnable()
    {
        volume = PlayerPrefs.GetFloat("MusicVolumeLevel", 1f);
        ApplyVolume();
    }

    void UpdateBar()
    {
        int activeCount = Mathf.RoundToInt(volume * segments.Length);
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].color = (i < activeCount) ? Color.green : Color.gray;
        }
    }
}

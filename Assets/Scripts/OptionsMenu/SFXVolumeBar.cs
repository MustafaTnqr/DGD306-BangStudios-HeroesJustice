using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeBar : MonoBehaviour
{
    public Image[] segments;
    private float volume;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SFXVolumeLevel", 1f);
        ApplyVolume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            volume = Mathf.Clamp01(volume + 0.1f);
            ApplyVolume();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            volume = Mathf.Clamp01(volume - 0.1f);
            ApplyVolume();
        }
    }

    void ApplyVolume()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(volume);

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

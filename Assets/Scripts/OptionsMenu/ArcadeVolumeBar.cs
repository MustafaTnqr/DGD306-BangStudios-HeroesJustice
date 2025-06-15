using UnityEngine;
using UnityEngine.UI;

public class ArcadeVolumeBar : MonoBehaviour
{
    public Image[] segments;
    [Range(0f, 1f)] public float volume = 1f;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVolumeLevel", 1f);
        ApplyVolume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            volume = Mathf.Clamp01(volume + 0.1f);
            ApplyVolume();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            volume = Mathf.Clamp01(volume - 0.1f);
            ApplyVolume();
        }
    }

    void ApplyVolume()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(volume);

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

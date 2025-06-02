using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Kanallarý")]
    public AudioSource sfxSource;       // Efekt sesleri için
    public AudioSource musicSource;     // Arka plan müziði için

    [Header("Efekt Sesleri")]
    public AudioClip zombieDeath;
    public AudioClip skeletonDeath;
    public AudioClip batDeath;
    public AudioClip pistolShot;
    public AudioClip swordSwingSound;

    [Header("Yürüyüþ Sesleri (Opsiyonel)")]
    public AudioClip defaultWalk;
    public AudioClip woodWalk;

    [Header("Arka Plan Müzik")]
    public AudioClip backgroundMusic;

    private void Awake()
    {
        // Singleton örneði oluþtur
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþse bile kalýcý
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    /// <summary>
    /// Efekt sesi çalar (tek seferlik)
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip, volume);
    }

    /// <summary>
    /// Arka plan müziðini baþlatýr
    /// </summary>
    public void PlayMusic(AudioClip music)
    {
        if (music != null && musicSource != null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    /// <summary>
    /// Mevcut müziði durdurur
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Kanallar�")]
    public AudioSource sfxSource;       // Efekt sesleri i�in
    public AudioSource musicSource;     // Arka plan m�zi�i i�in

    [Header("Efekt Sesleri")]
    public AudioClip zombieDeath;
    public AudioClip skeletonDeath;
    public AudioClip batDeath;
    public AudioClip pistolShot;
    public AudioClip swordSwingSound;

    [Header("Y�r�y�� Sesleri (Opsiyonel)")]
    public AudioClip defaultWalk;
    public AudioClip woodWalk;

    [Header("Arka Plan M�zik")]
    public AudioClip backgroundMusic;

    private void Awake()
    {
        // Singleton �rne�i olu�tur
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�se bile kal�c�
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
    /// Efekt sesi �alar (tek seferlik)
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip, volume);
    }

    /// <summary>
    /// Arka plan m�zi�ini ba�lat�r
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
    /// Mevcut m�zi�i durdurur
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}

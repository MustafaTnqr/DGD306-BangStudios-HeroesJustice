using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Kanallar�")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Efekt Sesleri")]
    public AudioClip zombieDeath;
    public AudioClip skeletonDeath;
    public AudioClip batDeath;
    public AudioClip pistolShot;
    public AudioClip swordSwingSound;

    [Header("Arka Plan M�zik")]
    public AudioClip backgroundMusic;

    private void Awake()
    {
        // Singleton kal�b�: sahnede tek bir AudioManager olmas�n� garanti eder
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�se bile yok olmas�n
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
    /// Tek seferlik efekt sesi �alar
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
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
    /// M�zi�i durdurur
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}

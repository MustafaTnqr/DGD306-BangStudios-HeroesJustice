using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Kanallarý")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Efekt Sesleri")]
    public AudioClip zombieDeath;
    public AudioClip skeletonDeath;
    public AudioClip batDeath;
    public AudioClip pistolShot;
    public AudioClip swordSwingSound;

    [Header("Arka Plan Müzik")]
    public AudioClip backgroundMusic;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
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

   
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }

   
    public void PlayMusic(AudioClip music)
    {
        if (music != null && musicSource != null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

   
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}

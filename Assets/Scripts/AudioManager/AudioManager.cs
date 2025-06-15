using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Kanalları")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Efekt Sesleri")]
    public AudioClip zombieDeath;
    public AudioClip skeletonDeath;
    public AudioClip batDeath;
    public AudioClip pistolShot;
    public AudioClip swordSwingSound;

    [Header("Yürüyüş Sesleri")]
    public AudioClip defaultWalk;
    public AudioClip woodWalk;

    [Header("Arka Plan Müzik")]
    public AudioClip backgroundMusic;
    public AudioClip bossMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        
        float savedMusic = PlayerPrefs.GetFloat("MusicVolumeLevel", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolumeLevel", 1f);

        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);

        PlayMusic(backgroundMusic);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, sfxSource.volume);
        }
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
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

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = volume;

        PlayerPrefs.SetFloat("MusicVolumeLevel", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = volume;

        PlayerPrefs.SetFloat("SFXVolumeLevel", volume);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CharacterSelection")
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour //AUDIO MANAGER AI DAN DESTEK ALINARAK YAPILDI
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

    [Header("Yürüyüþ Sesleri (Opsiyonel)")]
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

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip, volume);
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "Level2")
        {
            StopMusic();
        }

        
        if (scene.name == "CharacterSelection")
        {
            Destroy(gameObject);
        }
    }
}

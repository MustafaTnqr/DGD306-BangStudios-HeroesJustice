using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public GameObject missionCompleteUI;
    public GameObject deathScreenUI;
    public Transform spawnPoint;
    public AudioSource musicSource; 



    private GameObject spawned;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        
        if (GameObject.FindWithTag("Player") == null)
        {
            spawned = Instantiate(characterPrefabs[selectedCharacter], spawnPoint.position, Quaternion.identity);
            DontDestroyOnLoad(spawned);
        }
        else
        {
            spawned = GameObject.FindWithTag("Player"); 
        }

        
        RoomCamera camScript = Camera.main.GetComponent<RoomCamera>();
        if (camScript != null)
        {
            camScript.player = spawned.transform;
        }

        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ShowMissionCompleteUI()
    {
        if (missionCompleteUI != null)
            missionCompleteUI.SetActive(true);
        if (musicSource != null)
            musicSource.Stop();
        Time.timeScale = 0f;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawn = GameObject.Find("SpawnPoint");

        
        if (scene.name == "CharacterSelection" && spawned != null)
        {
            Destroy(spawned);
            spawned = null;
            return; 
        }

        
        if (spawn != null && spawned != null)
        {
            spawned.transform.position = spawn.transform.position;
        }

        if (scene.name == "CharacterSelection")
        {
            if (spawned != null)
            {
                Destroy(spawned);
                spawned = null;
            }

            
            AudioManager audio = FindObjectOfType<AudioManager>();
            if (audio != null)
                Destroy(audio.gameObject);

            return;
        }

    }

}

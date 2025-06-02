using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;

    private GameObject spawned;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        // Karakter zaten varsa tekrar yaratma
        if (GameObject.FindWithTag("Player") == null)
        {
            spawned = Instantiate(characterPrefabs[selectedCharacter], spawnPoint.position, Quaternion.identity);
            DontDestroyOnLoad(spawned);
        }
        else
        {
            spawned = GameObject.FindWithTag("Player"); // Zaten varsa referans� al
        }

        // Kamera ba�lant�s�
        RoomCamera camScript = Camera.main.GetComponent<RoomCamera>();
        if (camScript != null)
        {
            camScript.player = spawned.transform;
        }

        // Sahne de�i�ince spawn noktas�na ta��
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawn = GameObject.Find("SpawnPoint"); // Sahnedeki SpawnPoint objesini bul
        if (spawn != null && spawned != null)
        {
            spawned.transform.position = spawn.transform.position;
        }
    }
}

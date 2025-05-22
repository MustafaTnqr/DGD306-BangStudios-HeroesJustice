using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; 
    public Transform spawnPoint;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        GameObject spawned = Instantiate(characterPrefabs[selectedCharacter], spawnPoint.position, Quaternion.identity);

        
        RoomCamera camScript = Camera.main.GetComponent<RoomCamera>();
        if (camScript != null)
        {
            camScript.player = spawned.transform;       
        }
        else
        {
            
        }
    }

}

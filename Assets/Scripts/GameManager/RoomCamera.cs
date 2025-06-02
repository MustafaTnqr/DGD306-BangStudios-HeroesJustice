using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomCamera : MonoBehaviour
{
    public Transform player;
    public float teleportOffset = 0.5f;
    private float screenHalfWidthWorld;
    private float screenHalfHeightWorld;
    private int currentScreenX = 0;
    private int currentScreenY = 0;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        SetupCamera();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupCamera(); 
    }

    void SetupCamera()
    {
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                player = obj.transform;
        }

        screenHalfHeightWorld = Camera.main.orthographicSize;
        screenHalfWidthWorld = screenHalfHeightWorld * Camera.main.aspect;
        currentScreenX = 0;
        currentScreenY = 0;
        UpdateCameraPosition();
    }

    void Update()
    {
        if (player == null) return;

        float playerX = player.position.x;
        float playerY = player.position.y;

        float currentScreenCenterX = currentScreenX * screenHalfWidthWorld * 2f;
        float currentScreenCenterY = currentScreenY * screenHalfHeightWorld * 2f;

        if (playerX > currentScreenCenterX + screenHalfWidthWorld)
        {
            currentScreenX++;
            UpdateCameraPosition();
            player.position = new Vector3(currentScreenCenterX + screenHalfWidthWorld + teleportOffset, playerY, player.position.z);
        }
        else if (playerX < currentScreenCenterX - screenHalfWidthWorld)
        {
            currentScreenX--;
            UpdateCameraPosition();
            player.position = new Vector3(currentScreenCenterX - screenHalfWidthWorld - teleportOffset, playerY, player.position.z);
        }

        if (playerY > currentScreenCenterY + screenHalfHeightWorld)
        {
            currentScreenY++;
            UpdateCameraPosition();
            player.position = new Vector3(playerX, currentScreenCenterY + screenHalfHeightWorld + teleportOffset, player.position.z);
        }
        else if (playerY < currentScreenCenterY - screenHalfHeightWorld)
        {
            currentScreenY--;
            UpdateCameraPosition();
            player.position = new Vector3(playerX, currentScreenCenterY - screenHalfHeightWorld - teleportOffset, player.position.z);
        }
    }

    void UpdateCameraPosition()
    {
        float newX = currentScreenX * screenHalfWidthWorld * 2f;
        float newY = currentScreenY * screenHalfHeightWorld * 2f;
        transform.position = new Vector3(newX, newY, -10f);
    }
}

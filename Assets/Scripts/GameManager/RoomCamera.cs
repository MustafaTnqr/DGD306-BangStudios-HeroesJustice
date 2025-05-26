using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public Transform player;
    public float teleportOffset = 0.5f;
    private float screenHalfWidthWorld;
    private float screenHalfHeightWorld;
    private int currentScreenX = 0;
    private int currentScreenY = 0;

    void Start()
    {
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                player = obj.transform;
        }

        screenHalfHeightWorld = Camera.main.orthographicSize;
        screenHalfWidthWorld = screenHalfHeightWorld * Camera.main.aspect;
        UpdateCameraPosition();
    }

    void Update()
    {
        float playerX = player.position.x;
        float playerY = player.position.y;

        float currentScreenCenterX = currentScreenX * screenHalfWidthWorld * 2f;
        float currentScreenCenterY = currentScreenY * screenHalfHeightWorld * 2f;

        // Saða geçiþ
        if (playerX > currentScreenCenterX + screenHalfWidthWorld)
        {
            currentScreenX++;
            UpdateCameraPosition();
            player.position = new Vector3(currentScreenCenterX + screenHalfWidthWorld + teleportOffset, playerY, player.position.z);
        }
        // Sola geçiþ
        else if (playerX < currentScreenCenterX - screenHalfWidthWorld)
        {
            currentScreenX--;
            UpdateCameraPosition();
            player.position = new Vector3(currentScreenCenterX - screenHalfWidthWorld - teleportOffset, playerY, player.position.z);
        }

        // Yukarý geçiþ
        if (playerY > currentScreenCenterY + screenHalfHeightWorld)
        {
            currentScreenY++;
            UpdateCameraPosition();
            player.position = new Vector3(playerX, currentScreenCenterY + screenHalfHeightWorld + teleportOffset, player.position.z);
        }
        // Aþaðý geçiþ
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

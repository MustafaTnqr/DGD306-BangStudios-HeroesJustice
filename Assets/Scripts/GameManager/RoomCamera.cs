using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public Transform player;
    public float teleportOffset = 0.5f;
    private float screenHalfWidthWorld;
    private int currentScreenX = 0;

    void Start()
    {
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                player = obj.transform;
        }

        float screenHalfHeight = Camera.main.orthographicSize;
        screenHalfWidthWorld = screenHalfHeight * Camera.main.aspect;
        UpdateCameraPosition();
    }

    void Update()
    {
        float playerX = player.position.x;

        float currentScreenCenter = currentScreenX * screenHalfWidthWorld * 2f;

        if (playerX > currentScreenCenter + screenHalfWidthWorld)
        {
            currentScreenX++;
            UpdateCameraPosition();
            player.position = new Vector3(currentScreenCenter + screenHalfWidthWorld + teleportOffset, player.position.y, player.position.z);
        }

        else if (playerX < currentScreenCenter - screenHalfWidthWorld)
        {
            currentScreenX--;
            UpdateCameraPosition();
            player.position = new Vector3(currentScreenCenter - screenHalfWidthWorld - teleportOffset, player.position.y, player.position.z);
        }
    }

    void UpdateCameraPosition()
    {
        float newX = currentScreenX * screenHalfWidthWorld * 2f;
        transform.position = new Vector3(newX, transform.position.y, -10f);
    }
}

using UnityEngine;
using System.Collections;

public class CinematicCamera : MonoBehaviour  //Sinematik için ai kullanýldý
{
    public Transform[] cameraPoints;
    public float moveSpeed = 1.5f; 
    public float waitTime = 2f;

    private int currentPoint = 0;

    private MonoBehaviour playerController; 
    private System.Type controllerType;

    void Start()
    {
        
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            
            var move = player.GetComponent<PlayerMovement>();
            var sword = player.GetComponent<SwordPlayerController>();

            if (move != null)
            {
                playerController = move;
                controllerType = typeof(PlayerMovement);
                move.canMove = false;
            }
            else if (sword != null)
            {
                playerController = sword;
                controllerType = typeof(SwordPlayerController);
                sword.canMove = false;
            }
        }

        StartCoroutine(MoveThroughPoints());
    }

    IEnumerator MoveThroughPoints()
    {
        while (currentPoint < cameraPoints.Length)
        {
            Vector3 target = cameraPoints[currentPoint].position;
            target.z = -10f; 

            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                Vector3 newPos = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                transform.position = newPos;
                yield return null;
            }

            currentPoint++;
            yield return new WaitForSeconds(waitTime);
        }

        EndCinematic();
    }

    void EndCinematic()
    {
        StartCoroutine(DelayThenActivateCamera());
    }

    IEnumerator DelayThenActivateCamera()
    {
        yield return new WaitForSeconds(1f); 

        
        RoomCamera cam = GetComponent<RoomCamera>();
        if (cam != null)
        {
            cam.enabled = true;
            cam.SnapToPlayer();
        }

        
        if (playerController != null)
        {
            if (controllerType == typeof(PlayerMovement))
                ((PlayerMovement)playerController).canMove = true;
            else if (controllerType == typeof(SwordPlayerController))
                ((SwordPlayerController)playerController).canMove = true;
        }

        Destroy(this); 
    }
}

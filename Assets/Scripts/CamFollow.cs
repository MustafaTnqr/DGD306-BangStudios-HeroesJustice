using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset; 
    public float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset; 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;  
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }   
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

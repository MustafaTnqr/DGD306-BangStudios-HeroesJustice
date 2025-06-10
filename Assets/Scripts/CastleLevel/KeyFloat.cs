using UnityEngine;

public class KeyFloat : MonoBehaviour
{
    public float floatSpeed = 1f;   
    public float floatAmount = 0.5f; 

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}

using UnityEngine;

public class KeyFloat : MonoBehaviour
{
    public float floatSpeed = 1f;   // Ne kadar hýzlý yukarý-aþaðý hareket etsin
    public float floatAmount = 0.5f; // Yukarý aþaðý ne kadar mesafe yapsýn

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

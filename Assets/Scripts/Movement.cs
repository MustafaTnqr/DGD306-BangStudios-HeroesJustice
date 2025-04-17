using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody2D rb; 
    private Vector2 moveDirection;
    private Vector3 originalScale; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        originalScale = transform.localScale; 
    }

    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal"); 
        float moveInputY = Input.GetAxisRaw("Vertical"); 
        moveDirection = new Vector2(moveInputX, moveInputY).normalized;  
        if (moveInputX > 0) 
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (moveInputX < 0) 
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
}

using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) target = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        float direction = target.position.x - transform.position.x;

        rb.velocity = new Vector2(Mathf.Sign(direction) * moveSpeed, rb.velocity.y);

        if (direction > 0 && !isFacingRight)
            Flip();
        else if (direction < 0 && isFacingRight)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;

    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;
    private Transform currentTarget;

    [Header("Detection")]
    public float detectionRange = 5f;
    private bool chasingPlayer = false;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;

    void Awake()
    {
        isFacingRight = transform.localScale.x > 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }

        currentTarget = pointA; // Baþlangýç devriye hedefi
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        chasingPlayer = (distanceToPlayer <= detectionRange);

        if (chasingPlayer)
        {
            // Oyuncuya saldýrý
            float dir = target.position.x - transform.position.x;
            rb.velocity = new Vector2(Mathf.Sign(dir) * moveSpeed, rb.velocity.y);

            if (dir > 0 && !isFacingRight)
                Flip();
            else if (dir < 0 && isFacingRight)
                Flip();
        }
        else
        {
            // Devriye modu
            float dir = currentTarget.position.x - transform.position.x;
            rb.velocity = new Vector2(Mathf.Sign(dir) * moveSpeed, rb.velocity.y);

            if (Mathf.Abs(dir) < 0.2f)
            {
                // Hedefe ulaþtýysa diðer patrol noktasýna geç
                currentTarget = (currentTarget == pointA) ? pointB : pointA;
            }

            if (dir > 0 && !isFacingRight)
                Flip();
            else if (dir < 0 && isFacingRight)
                Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (pointA != null && pointB != null)
            Gizmos.DrawLine(pointA.position, pointB.position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    

    

}

using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 4f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                float verticalInput = Input.GetAxisRaw("Vertical");

                if (Mathf.Abs(verticalInput) > 0.1f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
                    rb.gravityScale = 0f;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1f;
            }
        }
    }
}

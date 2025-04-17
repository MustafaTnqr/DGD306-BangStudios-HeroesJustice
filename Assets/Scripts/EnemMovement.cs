using UnityEngine;

public class EnemMovement : MonoBehaviour
{
    private Transform target; 
    public float speed = 2f; 

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsTarget(); 
    }

    void MoveTowardsTarget()
    {
        if (target == null) return; 

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}

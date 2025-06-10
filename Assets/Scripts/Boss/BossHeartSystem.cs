using UnityEngine;

public class BossHeartSpawner : MonoBehaviour
{
    public float triggerDistance = 3f;
    public GameObject heartPrefab;
    public Transform[] spawnPoints;
    public float spawnDelay = 20f;

    private Transform player;
    private bool timerStarted = false;
    private float timer = 0f;
    private bool heartsSpawned = false;

    void Update()
    {
        
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;

            return; 
        }

        if (heartsSpawned)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!timerStarted && distance <= triggerDistance)
        {
            timerStarted = true;
            timer = spawnDelay;
        }

        if (timerStarted)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                SpawnHearts();
                heartsSpawned = true;
            }
        }
    }

    void SpawnHearts()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(heartPrefab, point.position, Quaternion.identity);
        }
    }
}

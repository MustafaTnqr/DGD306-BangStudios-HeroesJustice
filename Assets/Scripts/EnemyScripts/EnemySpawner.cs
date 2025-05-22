using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform player;
    public float spawnRadius = 5f;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

       
        float randomOffsetX = Random.Range(-spawnRadius, spawnRadius);
        Vector3 spawnPosition = new Vector3(player.position.x + randomOffsetX, player.position.y, 0f);

        Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
    }
}

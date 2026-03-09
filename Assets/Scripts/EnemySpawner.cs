using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform player;

    [SerializeField] float spawnDistance = 10f;
    [SerializeField] float minY = -2f;
    [SerializeField] float maxY = 1f;

    [SerializeField] float minSpawnTime = 1.5f;
    [SerializeField] float maxSpawnTime = 3f;

    [SerializeField] float startDelay = 10f; // delay before spawning starts

    private float nextSpawnTime = 0f;

    void Start()
    {
        nextSpawnTime = Time.time + startDelay; // wait before first spawn
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnEnemy()
    {
        float spawnX = player.position.x + spawnDistance;
        float spawnY = Random.Range(minY, maxY);

        Instantiate(enemyPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
    }
}
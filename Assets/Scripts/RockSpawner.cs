using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject rockPrefab;
    [SerializeField] Transform player;
    [SerializeField] float spawnDistance = 10f;
    [SerializeField] float groundY = -2f;
    [SerializeField] float minSpawnTime = 3f;
    [SerializeField] float maxSpawnTime = 6f;
    [SerializeField] float startDelay = 10f;

    private float nextSpawnTime = 0f;

    void Start()
    {
        nextSpawnTime = Time.time + startDelay;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRock();
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnRock()
    {
        float spawnX = player.position.x + spawnDistance;
        Instantiate(rockPrefab, new Vector3(spawnX, groundY, 0f), Quaternion.identity);
    }
}
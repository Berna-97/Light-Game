using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 15f;

    private float spawnTimer = 0f;

    private void Start()
    {
        
        if (enemyPrefab == null)
        {
            Debug.LogError("No Enemy Prefab");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemies();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemies()
    {
        int enemyCount = Random.Range(1, 5); 

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPos, Quaternion.Euler(-15, 90, 0));
        }

        Debug.Log($"Spawned {enemyCount} enemies");
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomZ = Random.Range(-20f, 20f);
        return new Vector3(-20f, 1f, randomZ);
    }
}
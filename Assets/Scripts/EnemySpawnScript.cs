using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 15f;

    private float spawnTimer = 0f;

    private void Start()
    {
        // Verifica se o prefab foi atribuído
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab não foi atribuído!");
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
        int enemyCount = Random.Range(1, 5); // Números aleatórios entre 1 e 4

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPos, Quaternion.Euler(0, 0, -75));
        }

        Debug.Log($"Spawned {enemyCount} inimigos");
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomZ = Random.Range(-5f, 5f);
        return new Vector3(-20f, 1f, randomZ);
    }
}
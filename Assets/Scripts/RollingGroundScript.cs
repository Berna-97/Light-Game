using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingGroundScript : MonoBehaviour
{
    public GroundLevelData levelData;
    public GameObject ground;
    public GameObject gate1;
    public GameObject gate2;
    public GameObject youWin;

    [Header("Progress UI")]
    public Slider progressSlider;
    public float fillSpeed = 2f; //for the progress bar

    [Header("Enemy Spawn Settings")]
    public float spawnDelay = 0.1f; // Time between each enemy spawn

    private int groundCount;
    private float targetSliderValue;
    private bool isUpdatingSlider;

    private void Start()
    {
        groundCount = 0;
        targetSliderValue = 0;
        isUpdatingSlider = false;
        InitializeProgressSlider();
    }

    private void Update()
    {
        if (isUpdatingSlider && progressSlider != null)
        {
            progressSlider.value = Mathf.Lerp(progressSlider.value, targetSliderValue, Time.deltaTime * fillSpeed);
            if (Mathf.Abs(progressSlider.value - targetSliderValue) < 0.01f)
            {
                progressSlider.value = targetSliderValue;
                isUpdatingSlider = false;
            }
        }
    }

    private void InitializeProgressSlider()
    {
        if (progressSlider != null)
        {
            progressSlider.minValue = 0;
            progressSlider.maxValue = levelData.totalGroundCount;
            progressSlider.value = 0;
        }
        else
        {
            Debug.LogWarning("where progress bar bro?");
        }
    }

    private void UpdateProgressSlider()
    {
        if (progressSlider != null)
        {
            targetSliderValue = groundCount;
            isUpdatingSlider = true;
            float percentage = (float)groundCount / levelData.totalGroundCount * 100f;
            //Debug.Log($"Progresso: {groundCount}/{levelData.totalGroundCount} ({percentage:F1}%)");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trigger")) return;

        bool spawned = false;

        // Check for gate spawns from level data
        for (int i = 0; i < levelData.totalGroundCount; i++)
        {
            levelData.GetData(i, out int before, out int buttons, out int hp);
            if (groundCount + 1 == before)
            {
                SpawnGate(buttons, hp);
                spawned = true;
                break;
            }
        }

        // Check for enemy wave spawns
        SpawnEnemyWaves();

        if (!spawned && groundCount < levelData.totalGroundCount)
            Instantiate(ground);

        if (groundCount == levelData.totalGroundCount)
            youWin.SetActive(true);

        groundCount++;
        UpdateProgressSlider();
    }

    private void SpawnEnemyWaves()
    {
        if (levelData.enemyWaves == null) return;

        foreach (EnemyWave wave in levelData.enemyWaves)
        {
            if (wave.spawnAtGroundCount == groundCount + 1)
            {
                StartCoroutine(SpawnWaveSequentially(wave));
            }
        }
    }

    private IEnumerator SpawnWaveSequentially(EnemyWave wave)
    {
        float totalWidth = (wave.enemyCount - 1) * wave.spawnSpacing;
        float startZ = -totalWidth / 2f;

        startZ = Mathf.Max(-35f + totalWidth / 2f, Mathf.Min(35f - totalWidth / 2f, startZ));

        Debug.Log($"Starting enemy wave spawn at ground {groundCount + 1}: {wave.enemyCount} enemies");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            float zPosition = startZ + (i * wave.spawnSpacing);
            zPosition = Mathf.Clamp(zPosition, -35f, 35f);

            Vector3 spawnPosition = new Vector3(-30f, 1f, zPosition);
            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.Euler(-15, 90, 0));

            EnemyMoveScript enemyScript = enemy.GetComponent<EnemyMoveScript>();
            if (enemyScript == null)
            {
                enemyScript = enemy.GetComponentInChildren<EnemyMoveScript>();
            }

            if (enemyScript != null)
            {
                int randomHealth = Random.Range(wave.minEnemyHealth, wave.maxEnemyHealth + 1);
                enemyScript.maxHealth = randomHealth;
                enemyScript.SetHealthToMax();
            }

            // Wait before spawning the next enemy
            if (i < wave.enemyCount - 1)
            {
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        Debug.Log($"Completed enemy wave spawn at ground {groundCount + 1}");
    }

    private void SpawnGate(int buttonsNum, int buttonsHp)
    {
        switch (buttonsNum)
        {
            case 0:
                Instantiate(ground);
                break;
            case 1:
                GameObject gateOne = Instantiate(gate1);
                Transform blueSquare = gateOne.transform.Find("Gate/Blue Square");
                EnemyMoveScript script = blueSquare.GetComponent<EnemyMoveScript>();
                script.maxHealth = buttonsHp;
                script.SetHealthToMax();
                break;
            case 2:
                GameObject gateTwo = Instantiate(gate2);
                Transform blueSquare2 = gateTwo.transform.Find("Gate/Blue Square");
                EnemyMoveScript script2 = blueSquare2.GetComponent<EnemyMoveScript>();
                script2.maxHealth = buttonsHp;
                script2.SetHealthToMax();
                Transform blueSquare3 = gateTwo.transform.Find("Gate/Blue Square2");
                EnemyMoveScript script3 = blueSquare3.GetComponent<EnemyMoveScript>();
                script3.maxHealth = buttonsHp;
                script3.SetHealthToMax();
                break;
            default:
                Instantiate(ground);
                break;
        }
    }
}
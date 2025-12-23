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
            Debug.Log($"Progresso: {groundCount}/{levelData.totalGroundCount} ({percentage:F1}%)");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trigger")) return;

        bool spawned = false;

        for (int i = 0; i < levelData.totalGroundCount; i++)
        {
            levelData.GetData(i, out int before, out int buttons, out int hp);

            if (groundCount + 1 == before)
            {
                Debug.Log("yea");
                SpawnGate(buttons, hp);
                spawned = true;
                break;
            }
        }

        if (!spawned && groundCount < levelData.totalGroundCount)
            Instantiate(ground);

        if (groundCount == levelData.totalGroundCount)
            youWin.SetActive(true);

        groundCount++;
        UpdateProgressSlider();
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
                Debug.Log(blueSquare2);
                EnemyMoveScript script2 = blueSquare2.GetComponent<EnemyMoveScript>();
                Debug.Log(script2);
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


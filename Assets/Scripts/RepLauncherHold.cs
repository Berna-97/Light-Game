using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RepLauncherHold : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject spawnPosition;
    public float speed = 10f;
    private float initialSpacing = 0f;
    public float spacing = 0.2f;
    public float timer = 0;
    private bool hasHeld = false;
    public float spawnRate = 0.3f;
    public float timeBetweenShots = 0.2f;
    private TargetClickScript targetSystem;
    private List<Repetition> activeRockets = new List<Repetition>();
    

    // Energy system
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    public float energyRechargeRate = 10f;
    public float energyCostPerRocket = 20f;
    private int rocketsQueued = 0;
    public TextMeshProUGUI energyCounter;

    void Start()
    {
        targetSystem = FindFirstObjectByType<TargetClickScript>();
        if (targetSystem == null)
            Debug.LogWarning("TargetClickScript não encontrado!");

        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += energyRechargeRate * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
        }

        UpdateEnergyDisplay();

        if (Input.GetMouseButton(0))
        {
            if (rocketPrefab == null || spawnPosition == null) return;

            if (currentEnergy < energyCostPerRocket) return;

            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                hasHeld = true;
                GameObject rocketObj = Instantiate(rocketPrefab, spawnPosition.transform.position + new Vector3(initialSpacing, 0, 0), Quaternion.Euler(0, -90, 0));
                initialSpacing -= spacing;
                timer = 0;

                currentEnergy -= energyCostPerRocket;
                rocketsQueued++;
            }

        }

        if (Input.GetMouseButtonUp(0) && hasHeld)
        {
            hasHeld = false;
            timer = 0;
            initialSpacing = 0f;
            StartCoroutine(FireRockets());
        }

        //foreach (var rocket in activeRockets)
        //{
        //    if (rocket != null)
        //        rocket.SetTarget(targetSystem.Target);
        //}
        activeRockets.RemoveAll(r => r == null);
    }

    IEnumerator FireRockets()
    {
        GameObject[] rep = GameObject.FindGameObjectsWithTag("Repetition");
        foreach (GameObject go in rep)
        {
            Repetition rocket = go.AddComponent<Repetition>();
            rocket.SetTarget(targetSystem.Target);

            activeRockets.Add(rocket);
            yield return new WaitForSeconds(timeBetweenShots);
        }

        rocketsQueued = 0;
        yield return null;
    }

    private void UpdateEnergyDisplay()
    {
        if (energyCounter != null)
        {
            energyCounter.text = $"Energy: {currentEnergy:F0}/{maxEnergy:F0}";
        }
    }

}
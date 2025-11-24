using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    void Start()
    {
        targetSystem = FindFirstObjectByType<TargetClickScript>();

        if (targetSystem == null)
            Debug.LogWarning("TargetClickScript não encontrado!");
    }

    void Update()
    {
        // Dispara rocket ao clicar com o botão esquerdo
        if (Input.GetMouseButton(0))
        {
            if (rocketPrefab == null || spawnPosition == null) return;

            timer += Time.deltaTime;

            if (timer > spawnRate) {

                hasHeld = true;
                GameObject rocketObj = Instantiate(rocketPrefab, spawnPosition.transform.position + new Vector3(initialSpacing, 0, 0), Quaternion.Euler(0, -90, 0));
                initialSpacing -= spacing;
                timer = 0;
            }

            //Repetition rocket = rocketObj.AddComponent<Repetition>();

            //rocket.SetTarget(targetSystem.Target); // passa o alvo atual

            //activeRockets.Add(rocket);
        }
        if (Input.GetMouseButtonUp(0) && hasHeld) 
        {

            hasHeld = false;
            timer = 0;
            initialSpacing = 0f;

            StartCoroutine(FireRockets());
        }

        // Atualiza o alvo de todos os rockets ativos (caso mude)
        foreach (var rocket in activeRockets)
        {
            if (rocket != null)
                rocket.SetTarget(targetSystem.Target);
        }

        // Remove rockets destruídos da lista
        activeRockets.RemoveAll(r => r == null);
    }

    IEnumerator FireRockets()
    {
        GameObject[] rep = GameObject.FindGameObjectsWithTag("Repetition");
        foreach (GameObject go in rep)
        {
            Repetition rocket = go.AddComponent<Repetition>();

            rocket.SetTarget(targetSystem.Target); // passa o alvo atual

            
            activeRockets.Add(rocket);
            yield return new WaitForSeconds(timeBetweenShots);
        }
        yield return null;
    }

}

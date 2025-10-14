using System.Collections.Generic;
using UnityEngine;

public class RepLauncherClick : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject spawnPosition;
    public float speed = 10f;

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
        if (Input.GetMouseButtonDown(0))
        {
            if (rocketPrefab == null || spawnPosition == null) return;

            GameObject rocketObj = Instantiate(rocketPrefab, spawnPosition.transform.position, Quaternion.identity);
            Repetition rocket = rocketObj.AddComponent<Repetition>();

            //rocket.speed = speed;
            rocket.SetTarget(targetSystem.Target); // passa o alvo atual

            activeRockets.Add(rocket);
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
}

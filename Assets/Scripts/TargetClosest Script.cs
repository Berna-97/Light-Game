using UnityEngine;

// Este script de targeting funciona automaticamente: o alvo muda para o mais proximo
// de maneira totalmente autónoma.
public class TargetClosestScript : MonoBehaviour
{
    private GameObject[] enemies;
    private GameObject targeted;
    private GameObject player;
    public float rotationSpeed = 50f;

    void Awake()
    {
        // Encontra todos os inimigos com a tag "Enemy" e o player
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");


        if (enemies.Length == 0)
        {
            Debug.LogWarning("E os inimigos onde andam?");
            return;
        }
        if (player == null)
        {
            Debug.LogWarning("Falta o player pah");
            return;
        }


        // Define o inimigo mais próximo
        targeted = FindClosestEnemy();
    }

    void Update()
    {
        // Segue o inimigo mais próximo
        if (targeted != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targeted.transform.position,
                rotationSpeed * Time.deltaTime // velocidade
            );
            targeted = FindClosestEnemy();
        }
        else
        {
            // Atualiza o alvo se o inimigo for destruído
            targeted = FindClosestEnemy();
        }
    }

    private GameObject FindClosestEnemy()
    {
        
        GameObject closest = null;
        float closestDistance = 9999f;

        //Por cada inimigo presente vamos buscar o mais próximo do player
        foreach (GameObject go in enemies)
        {
            float distance = Vector3.Distance(go.transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = go;
            }
        }
        Debug.Log(closest);
        return closest;
    }
}

using UnityEngine;

public class Repetition : MonoBehaviour
{
    public GameObject target;
    public float speed = 10f;
    public float explodeDistance = 2.5f;
    public float damage = 1f;
    

    private EnemyMoveScript enemyScript;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;


        if (target != null)
        {
            enemyScript = target.GetComponent<EnemyMoveScript>();
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); return;
        }

        
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(target.transform.position.x, 0, target.transform.position.z),
            speed * Time.deltaTime
        );

        // Faz o rocket olhar para o alvo
        transform.LookAt(target.transform);

        // Destroi ao chegar perto
        if (Vector3.Distance(transform.position, target.transform.position) < explodeDistance)
        {
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}



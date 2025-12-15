using UnityEngine;

public class EnemyProjectileLogic : MonoBehaviour
{
    public GameObject target;
    public float speed = 10f;
    public float explodeDistance = 2.5f;
    public float damage = 1f;
    

    private Opacity player;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;


        if (target != null)
        {
            player = target.GetComponent<Opacity>();
        }

    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); return;
        }

        // Faz o rocket olhar para o alvo
        transform.LookAt(target.transform);

    }
}



using UnityEngine;

public class RepetitionScript : MonoBehaviour
{
    public GameObject target;
    public float speed = 10f;
    public float explodeDistance = 0.5f;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
            return;

        // Move em direção ao alvo
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
        );

        // Faz o rocket olhar para o alvo
        transform.LookAt(target.transform);

        // Destroi ao chegar perto
        if (Vector3.Distance(transform.position, target.transform.position) < explodeDistance)
        {
            Destroy(gameObject);
        }
    }
}



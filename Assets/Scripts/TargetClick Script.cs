using UnityEngine;


public class TargetClickScript : MonoBehaviour
{
    private GameObject[] enemies;
    private int currentIndex = -1;
    private GameObject player;

    public GameObject Target { get; private set; } 
    public float targetSpeed = 50f;

    private float clickStartTime;
    private bool isHold;
    public float holdThreshold = 0.15f; // 150ms

    void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");

        if (enemies.Length > 0)
        {
            currentIndex = 0;
            Target = enemies[currentIndex];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickStartTime = Time.time;
            isHold = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time - clickStartTime > holdThreshold)
                isHold = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!isHold)
                Target = SwitchTarget();
        }



        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                Target.transform.position,
                targetSpeed * Time.deltaTime
            );
        }
        else
        {
            Target = FindClosestEnemy();
        }
    }

    private void RefreshEnemies() => enemies = GameObject.FindGameObjectsWithTag("Enemy");

    private GameObject SwitchTarget()
    {
        RefreshEnemies();

        if (enemies == null || enemies.Length == 0)
        {
            Debug.LogWarning("Nenhum inimigo encontrado!");
            Target = null;
            currentIndex = -1;
            return null;
        }

        currentIndex++;
        if (currentIndex >= enemies.Length)
            currentIndex = 0;

        Target = enemies[currentIndex];
        return Target;
    }

    private GameObject FindClosestEnemy()
    {

        RefreshEnemies();
        GameObject closest = null;
        float closestDistance = 9999f;

        if( player!= null ) { 
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
        return closest;
        }
        else { return null; }
    }
}

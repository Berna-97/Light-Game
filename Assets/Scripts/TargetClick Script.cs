using System.Linq;
using UnityEngine;


public class TargetClickScript : MonoBehaviour
{
    private GameObject[] enemies;
    private int currentIndex = -1;
    private GameObject player;

    public GameObject Target { get; private set; } 
    public float targetSpeed = 250f;

    private float clickStartTime;
    private bool isHold;
    public float holdThreshold = 0.15f; // 150ms
    private GameObject startPoint;
    void Awake()
    {
        startPoint = GameObject.Find("TargetStartPoint");
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
            MoveTargetToEnemy();
        }
        else
        {
            Target = FindClosestEnemy();
            if (Target == null)
            {
                MoveTargetToDefault();
            }
       
        }
    }

    private void RefreshEnemies() => enemies = GameObject.FindGameObjectsWithTag("Enemy");

    private GameObject SwitchTarget()
    {
        RefreshEnemies();

        if (enemies == null || enemies.Length == 0)
        {

            //Target = null;
            currentIndex = -1;
            //return null;

            return startPoint;
        }

        var closest3 = FindClosestThreeEnemies();

        if (closest3.Length == 0 && enemies.Length == 0)
        {
            currentIndex = -1;
            return startPoint;
        }

        if (closest3.Length != 0)
        {
            currentIndex = (currentIndex + 1) % closest3.Length;
            Target = closest3[currentIndex];
        }


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

    private GameObject[] FindClosestThreeEnemies()
    {
        RefreshEnemies();

        if (player == null || enemies == null || enemies.Length == 0)
            return new GameObject[0];

        return enemies
            .Where(e => !e.GetComponent<EnemyMoveScript>().isGate)
            .OrderBy(e => Vector3.Distance(e.transform.position, player.transform.position))
            .Take(3)
            .ToArray();
    }

    private void MoveTargetToEnemy()
    {
            transform.position = Vector3.MoveTowards(
            transform.position,
            Target.transform.position,
            targetSpeed * Time.deltaTime
        );
    }

    private void MoveTargetToDefault()
    {
        transform.position = Vector3.MoveTowards(
        transform.position,
        startPoint.transform.position,
        targetSpeed / 3 * Time.deltaTime
    );
    }
}

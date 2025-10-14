using UnityEngine;

public class TargetClickScript : MonoBehaviour
{
    private GameObject[] enemies;
    private int currentIndex = -1;
    private GameObject player;

    public GameObject Target { get; private set; } // 👈 alvo público e acessível
    public float targetSpeed = 50f;

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
        if (Input.GetMouseButtonDown(1))
        {
            Target = SwitchTarget();
        }

        // opcional: mover este objeto para o alvo
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                Target.transform.position,
                targetSpeed * Time.deltaTime
            );
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
        Debug.Log($"Novo alvo: {Target.name}");
        return Target;
    }
}

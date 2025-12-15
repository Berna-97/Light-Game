using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public GameObject spawner;
    public GameObject player;
    public GameObject target;
    public float acceleration;
    public float velocity;
    private float timer;

    void Start()
    {
        timer = 0;
        //ime.timeScale = 0f;
        spawner.SetActive(false);
        acceleration = -0.2f;
        velocity = 0f;
        HealthSystem healthSystem = player.GetComponent<HealthSystem>();
        healthSystem.health = 9999;
        target.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < 5)
        {

            timer += Time.deltaTime;
            velocity += acceleration * Time.deltaTime;
            player.transform.position = player.transform.position + new Vector3(velocity, 0, 0);

        }
        else
        {
            Destroy(player);
            SceneManager.LoadScene("Main Menu Scene");
        }


    }
}

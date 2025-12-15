using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pause;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause.activeSelf)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause.activeSelf)
        {
            UnPause();
        }

    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    private GameOverScreen deathScreen;

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu Scene");
    }
}

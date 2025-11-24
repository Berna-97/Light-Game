using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject optionCanvas;

    private void Start()
    {
        optionCanvas.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene("FoxView Scene");
    }

    public void Options()
    {
        canvas.SetActive(false);
        optionCanvas.SetActive(true);
    }
    public void ReturnFromOptions()
    {
        canvas.SetActive(true);
        optionCanvas.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

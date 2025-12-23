using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject optionCanvas;
    [SerializeField] private GameObject howToPlayCanvas;
    [SerializeField] private GameObject cam;
    private CameraSnapToFace snap;

    private void Start()
    {
        optionCanvas.SetActive(false);
        snap = cam.GetComponent<CameraSnapToFace>();
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level Infinite");
    }


    public void Options()
    {
        canvas.SetActive(false);
        optionCanvas.SetActive(true);
    }
    public void HowToPlay()
    {
        canvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }
    public void ReturnFromOptions()
    {
        canvas.SetActive(true);
        optionCanvas.SetActive(false);
    }
    public void ReturnFromHowToPlay()
    {
        canvas.SetActive(true);
        howToPlayCanvas.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void RotateRight()
    {
        snap.RotateRight();
    }

    public void RotateLeft()
    {
        snap.RotateLeft();
    }
}

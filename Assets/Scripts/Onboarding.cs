using UnityEngine;
using UnityEngine.Video;

public class Onboarding : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    GameObject canvas;
    GameObject onboardingCanvas;
    VideoPlayer player;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        onboardingCanvas = GameObject.Find("OnboardCanvas");
        canvas.SetActive(false);
        player = GameObject.Find("OnboardPlayer").GetComponent<VideoPlayer>();
        player.loopPointReached += OnVideoEnd;


    }

    void OnVideoEnd(VideoPlayer vp)
    {
        canvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onboardingCanvas.SetActive(false);
            canvas.SetActive(true);
        }
    }
}

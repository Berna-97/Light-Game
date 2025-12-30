using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GateScript : MonoBehaviour
{
    public bool isLeftGate;
    private bool isGateOpen = false;
    public int openess = 20;
    private int currentOpeness;
    private bool isSfxPlaying = false;
    private AudioSource rockSfx;

    // Update is called once per frame

    private void Start()
    {
        rockSfx = GameObject.Find("DoorOpenSFX").GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (!isGateOpen && currentOpeness < openess)
        {
            if (!isSfxPlaying)
            {
                isSfxPlaying=true;
                rockSfx.Play();
            }


            if (isLeftGate)
            {
                transform.Rotate(0, -5f, 0);
            }
            else
            {
                transform.Rotate(0, 5f, 0);
            }

            currentOpeness++;
        }
        //else if(isSfxPlaying) {
        //    rockSfx.Stop();
        //    isSfxPlaying = false;
        //}

    }

}

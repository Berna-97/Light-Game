using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GateScript : MonoBehaviour
{
    public bool isLeftGate;
    private bool isGateOpen = false;
    public int openess = 100;
    private int currentOpeness;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isGateOpen && currentOpeness < openess)
        {
            if (isLeftGate)
            {
                transform.Rotate(0, -1f, 0);
            }
            else
            {
                transform.Rotate(0, 1f, 0);
            }

            currentOpeness++;
        }
    }

}

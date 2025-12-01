using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class MultiCameraSwitcher : MonoBehaviour
{
    [Header("Assign your two cameras")]
    public CinemachineCamera cameraTarget;
    public CinemachineCamera cameraMoving;

    [Header("Set the priorities")]
    public int activePriority = 5;
    public int inactivePriority = 1;
    public bool target = false;

    void Start()
    {
        SwitchToCameraMoving();
    }

    public void Update()
    {
        // Example logic to switch cameras based on some condition
        if (target == true)
        {
            SwitchToCameraTarget();
        }
        else 
        {
            SwitchToCameraMoving();
        }
    }

    public void SwitchToCameraTarget()
    {
        cameraTarget.Priority = activePriority;
        cameraMoving.Priority = inactivePriority;
    }

    public void SwitchToCameraMoving()
    {
        cameraMoving.Priority = activePriority;
        cameraTarget.Priority = inactivePriority;
    }
}


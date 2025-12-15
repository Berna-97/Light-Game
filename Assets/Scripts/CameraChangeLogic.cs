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

    private GameObject[] enemies;
    private GameObject[] player;

    void Start()
    {
        SwitchToCameraMoving();
    }

    public void Update()
    {
        // Example logic to switch cameras based on some condition
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectsWithTag("Player");
       
        if (enemies.Length == 0 && player != null)
        {
            SwitchToCameraMoving();
        }
        else 
        {
            SwitchToCameraTarget();
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


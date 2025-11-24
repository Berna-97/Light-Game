using UnityEngine;
using System.Collections.Generic;

public class MultiCameraSwitcher : MonoBehaviour
{
    public List<Camera> cameras;

    public int activePriority = 5;
    public int inactivePriority = 0;

    public int activeCameraIndex = 0;

    public void SwitchToCamera(int index)
    {
        if (cameras == null || cameras.Count == 0)
            return;

        if (index < 0 || index >= cameras.Count)
            index = 0;

        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].enabled = (i == index);
        }
    }

    void Start()
    {
        // ensure the configured active camera is enabled at startup
        SwitchToCamera(activeCameraIndex);
    }

}

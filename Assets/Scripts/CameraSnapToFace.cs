using Unity.Cinemachine;
using UnityEngine;

public class CameraSnapToFace : MonoBehaviour
{
    public CinemachineOrbitalFollow follow;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4; // TOPO
    public GameObject title;

    private GameObject currentUI;

    public float autoRotateSpeed = 30f;
    public float snapSpeed = 2f;

    private readonly float[] faces = { 0f, 120f, 240f };

    private int currentLevel = 1;   // 1–4
    private int currentFace = 0;    // 0–2 (apenas laterais)

    private bool snappingX;
    private bool snappingY;

    void Start()
    {
        follow = GetComponent<CinemachineOrbitalFollow>();
        currentFace = GetClosestFaceIndex(follow.HorizontalAxis.Value);
        currentUI = title;
    }

    void Update()
    {
        // AUTO ROTATE
        if (!snappingY)
        {
            // Nos níveis laterais: normal
            if (currentLevel != 4)
            {
                follow.HorizontalAxis.Value += autoRotateSpeed * Time.deltaTime;
            }
            else
            {
                Debug.Log(follow.HorizontalAxis.Value);
                // No topo: roda mais devagar (opcional)
                follow.HorizontalAxis.Value -= 15 * Time.deltaTime;
            }

            follow.HorizontalAxis.Value %= 360f;
        }


        // Snap horizontal (faces laterais)
        if (snappingX)
        {
            float target = faces[currentFace];

            follow.HorizontalAxis.Value = Mathf.LerpAngle(
                follow.HorizontalAxis.Value,
                target,
                Time.deltaTime * snapSpeed
            );

            if (Mathf.Abs(Mathf.DeltaAngle(follow.HorizontalAxis.Value, target)) < 0.1f)
            {
                follow.HorizontalAxis.Value = target;
                snappingX = false;
            }
        }

        // Snap vertical (topo)
        if (snappingY)
        {
            float targetY = currentLevel == 4 ? 60f : 0f;

            follow.VerticalAxis.Value = Mathf.Lerp(
                follow.VerticalAxis.Value,
                targetY,
                Time.deltaTime * snapSpeed
            );

            if (currentLevel == 4)
            {
                if (Mathf.Abs(follow.VerticalAxis.Value - targetY) < 20f)
                {
                    follow.VerticalAxis.Value = targetY;
                    snappingY = false;
                }
            }
            else
            {
                if (Mathf.Abs(follow.VerticalAxis.Value - targetY) < 0.1f)
                {
                    follow.VerticalAxis.Value = targetY;
                    snappingY = false;
                }
            }
 
        }
    }

    // ---------- INPUT ----------

    public void RotateRight()
    {
        autoRotateSpeed = 0f;

        if (currentLevel == 3)
        {
            GoToLevel(4);
            return;
        }

        if (currentLevel == 4)
        {
            GoToLevel(1);
            return;
        }

        currentLevel++;
        currentFace = (currentFace - 1 + faces.Length) % faces.Length;
        snappingX = true;
        snappingY = true;

        ShowLevelScreen(currentLevel);
    }

    public void RotateLeft()
    {
        autoRotateSpeed = 0f;

        if (currentLevel == 1)
        {
            GoToLevel(4);
            return;
        }

        if (currentLevel == 4)
        {
            GoToLevel(3);
            return;
        }

        currentLevel--;
        currentFace = (currentFace + 1) % faces.Length;
        snappingX = true;
        snappingY = true;

        ShowLevelScreen(currentLevel);
    }

    // ---------- NAVEGAÇÃO ----------

    private void GoToLevel(int level)
    {
        currentLevel = level;

        if (level == 4)
        {
            snappingX = false;
            snappingY = true;
        }
        else
        {
            currentFace = level - 1;
            snappingX = true;
            snappingY = true;
        }

        ShowLevelScreen(level);
    }


    public void GoToLevelOne()
    {
        GoToLevel(1);
        title.SetActive(false);
    }

    private int GetClosestFaceIndex(float currentAngle)
    {
        int closest = 0;
        float min = Mathf.Abs(Mathf.DeltaAngle(currentAngle, faces[0]));

        for (int i = 1; i < faces.Length; i++)
        {
            float d = Mathf.Abs(Mathf.DeltaAngle(currentAngle, faces[i]));
            if (d < min)
            {
                min = d;
                closest = i;
            }
        }
        return closest;
    }

    // ---------- UI ----------

    private void ShowLevelScreen(int level)
    {
        if (currentUI != null)
            currentUI.SetActive(false);

        switch (level)
        {
            case 1:
                level1.SetActive(true);
                currentUI = level1;
                break;
            case 2:
                level2.SetActive(true);
                currentUI = level2;
                break;
            case 3:
                level3.SetActive(true);
                currentUI = level3;
                break;
            case 4:
                level4.SetActive(true);
                currentUI = level4;
                break;
        }
    }
}

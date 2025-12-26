using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RepLauncherHold : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject spawnPosition;
    public float speed = 10f;
    private float initialSpacing = 0f;
    public float spacing = 0.2f;
    public float timer = 0;
    private bool hasHeld = false;
    public float spawnRate = 0.3f;
    public float timeBetweenShots = 0.2f;
    public int maxShots = 7;
    private TargetClickScript targetSystem;
    private List<GameObject> activeRockets = new List<GameObject>();
    private bool isFiring = false;
    public AudioSource fireSfx;

    void Start()
    {
        targetSystem = FindFirstObjectByType<TargetClickScript>();
        if (targetSystem == null)
            Debug.LogWarning("TargetClickScript não encontrado!");
    }

    void Update()
    {
        if (isFiring)
            return;

        if (Input.GetMouseButton(0))
        {
            if (rocketPrefab == null || spawnPosition == null) return;

            // Check if player has reached max shots
            if (activeRockets.Count >= maxShots) return;

            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                hasHeld = true;
                GameObject rocketObj = Instantiate(
                    rocketPrefab,
                    spawnPosition.transform.position + new Vector3(initialSpacing, 0, 0),
                    Quaternion.Euler(0, -90, 0)
                );
                activeRockets.Add(rocketObj);
                initialSpacing -= spacing;
                //timer = activeRockets.Count * 0.03f;
                timer = 0;
            }
        }

        if (Input.GetMouseButtonUp(0) && hasHeld)
        {
            hasHeld = false;
            timer = 0;
            initialSpacing = 0f;
            StartCoroutine(FireRockets());
        }

        activeRockets.RemoveAll(r => r == null);
    }

    IEnumerator FireRockets()
    {
        isFiring = true;
        GameObject currentTarget = targetSystem.Target;
        List<GameObject> rocketsToFire = new List<GameObject>(activeRockets);

        foreach (var rocketObj in rocketsToFire)
        {
            if (rocketObj != null)
            {
                fireSfx.Play();
                Repetition rocket = rocketObj.AddComponent<Repetition>();
                rocket.SetTarget(currentTarget);
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }

        activeRockets.Clear();
        isFiring = false;
    }
}


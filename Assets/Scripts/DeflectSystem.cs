using UnityEngine;

public class DeflectSystem : MonoBehaviour
{
    public GameObject deflectZone;
    public float deflectActiveTime = 0.35f;
    public float doubleClickTime = 0.3f;

    private float lastClickTime;

    void Update()
    {
        if (Input.GetMouseButton(0)) // change key if needed
        {
            lastClickTime = Time.time;
        } 

        if (Time.time - lastClickTime <= doubleClickTime)
            {
              ActivateDeflect();
            }
    }

    void ActivateDeflect()
    {
        StopAllCoroutines();
        StartCoroutine(DeflectCoroutine());
    }

    System.Collections.IEnumerator DeflectCoroutine()
    {
        deflectZone.SetActive(true);
        Debug.LogWarning("Deflect Activated");
        yield return new WaitForSeconds(deflectActiveTime);
        deflectZone.SetActive(false);
        Debug.LogWarning("Deflect Deactivated");
    }
}

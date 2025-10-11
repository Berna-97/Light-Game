using Unity.VisualScripting;
using UnityEngine;

public class GroundMoverScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.1f, 0, 0);

        Destroy(gameObject, 2.0f);

    }
}

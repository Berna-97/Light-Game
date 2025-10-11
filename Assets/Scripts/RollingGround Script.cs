using UnityEngine;

public class RollingGroundScript : MonoBehaviour
{

    public GameObject PathSection;

    private void Start()
    {
        Debug.Log("ge");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hai");
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(PathSection);
        }
    }
}

using UnityEngine;

// Este script cria um chão novo quando o player passa por um trigger presente no chão,
// o que cria um loop infinito
public class RollingGroundScript : MonoBehaviour
{

    public GameObject PathSection;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(PathSection);
        }
    }
}

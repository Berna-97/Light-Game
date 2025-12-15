using UnityEngine;

public class DeflectZone : MonoBehaviour
{



    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Projectile Detected");
        Projectile proj = other.GetComponent<Projectile>();
        Debug.LogWarning(proj.gameObject.name);
        if (proj != null && !proj.deflected)
        {
            
            Vector3 dir = other.transform.position - transform.position;
            proj.Deflect(dir);
        }
    }
}

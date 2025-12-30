using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;


public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    private bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (hasCollided) return;
            HealthSystem healthSystem = other.gameObject.GetComponentInParent<HealthSystem>();
            Debug.Log("yr");

            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("No HealthSystem found on Player or its parents!");
            }

            hasCollided = true;
        }
    }
}

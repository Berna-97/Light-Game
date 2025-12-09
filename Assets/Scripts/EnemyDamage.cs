using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;


public class EnemyDamage : MonoBehaviour
{
    private HealthSystem healthSystem;
    public int damage = 1;
    private bool hasCollided = false;

    public void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (hasCollided) return;
            HealthSystem healthSystem = other.gameObject.GetComponentInParent<HealthSystem>();


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

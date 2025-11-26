using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyDamage : MonoBehaviour
{
    public HealthSystem healthSystem;
    public int damage = 1;

    void Start()
    {
        
    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = other.gameObject.GetComponentInParent<HealthSystem>();


            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("No HealthSystem found on Player or its parents!");
            }
        }
    }
}

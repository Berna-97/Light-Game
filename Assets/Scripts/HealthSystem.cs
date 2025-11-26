using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int maxHealth = 5;
    public int minHealth = 0;
    public int damage = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= minHealth)
        {
            Destroy(this.gameObject);
        }
    }
}

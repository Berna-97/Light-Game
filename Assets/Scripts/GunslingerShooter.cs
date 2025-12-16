using JetBrains.Annotations;
using UnityEngine;

public class Gunslinger : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform player;

    public int health;
    public int maxHealth = 10;
    public int minHealth = 9;


    // Automatic shooting interval in seconds
    public float shootInterval = 2f;
    private float shootTimer = 1f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }

        // automatic shooting timer
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 1f;
        }
    }

    public void Shoot()
    {
        if (firePoint == null || player == null || projectilePrefab == null)
        {
            Debug.LogWarning("Gunslinger: missing references for shooting");
            return;
        }

        firePoint.LookAt(player);
        GameObject spawned = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile proj = spawned.GetComponent<Projectile>();

        if (proj != null)
        {
            proj.SetOwner(transform);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= minHealth)
        {
            Debug.Log("Gunslinger defeated");
            Destroy(this.gameObject);
        }
    }

}

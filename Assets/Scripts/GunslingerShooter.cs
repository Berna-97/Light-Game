using JetBrains.Annotations;
using UnityEngine;

public class Gunslinger : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform player;

    float shootDistance = 10f;



    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceToPlayer <= shootDistance)
        {
            Shoot();
        }


    }


    public void Shoot()
    {
        firePoint.LookAt(player);
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    public void Die()
    {
        Debug.LogWarning("Gunslinger defeated!");
        Destroy(gameObject);
    }
}

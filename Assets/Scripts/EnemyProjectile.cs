using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public bool deflected = false;

    private Rigidbody rb;
    private object projectile;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }
        else
        {
            Debug.LogWarning("Projectile missing Rigidbody: " + gameObject.name);
        }
    }

    void Update()
    {
        if (rb != null)
        {
            Debug.DrawRay(transform.position, rb.linearVelocity, Color.red);
        }

        
        projectile = GetComponent<Projectile>();
        if (projectile != null) {
            // Additional projectile behavior can be added here
            transform.forward = rb.linearVelocity.normalized;
        }
    }

    public void Deflect(Vector3 newDirection)
    {
        deflected = true;
        if (rb != null)
        {
        
            rb.linearVelocity = newDirection.normalized * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If projectile was deflected and hits enemy
        if (deflected && collision.gameObject.CompareTag("EnemyR"))
        {
            if (collision.gameObject.TryGetComponent<Gunslinger>(out var enemy))
            {
                enemy.Die();
            }
            Destroy(this.gameObject);
        }

        // If not deflected and hits player
        if (!deflected && collision.gameObject.CompareTag("Player"))
        {
            // Player damage here
            
            Destroy(this.gameObject);
        }
    }
}

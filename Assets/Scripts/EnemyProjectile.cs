using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public bool deflected = false;

    private Rigidbody rb;
    private object projectile;
    private Transform owner;

    public int damage = 1;

    private HealthSystem player;

    public void SetOwner(Transform newOwner)
    {
        owner = newOwner;
    }

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

        IgnoreSiblingProjectiles();
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

    void IgnoreSiblingProjectiles()
{
    Collider myCol = GetComponent<Collider>();
    Projectile[] allProjectiles = FindObjectsOfType<Projectile>();

    foreach (Projectile p in allProjectiles)
    {
        if (p != this && p.owner == owner)
        {
            Physics.IgnoreCollision(myCol, p.GetComponent<Collider>());
        }
    }
}

    public void Deflect()
    {
        
        if (owner == null) return;

        deflected = true;
        Debug.LogWarning("Is Deflected");

        if (rb != null)
        {
            Debug.LogWarning("Changing direction to owner");
            // Direction back to owner
            Vector3 dirToOwner = owner.position - transform.position;
            rb.linearVelocity = dirToOwner.normalized * speed;
        }
    }

    void OnTriggerEnter(UnityEngine.Collider other)
    {
        HealthSystem healthSystem = other.gameObject.GetComponentInParent<HealthSystem>();

        // If projectile was deflected and hits enemy
        if (deflected && other.gameObject.CompareTag("EnemyR"))
        {
            Debug.LogWarning("Projectile collided with: enemy");
            if (other.gameObject.TryGetComponent<Gunslinger>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }

        // If not deflected and hits player
        if (!deflected && other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("Projectile collided with: player");
            // Player damage here
            if (healthSystem != null)
            {
                Debug.LogWarning("Player takes damage");
                healthSystem.TakeDamage(damage);
                Destroy(this.gameObject);
            } 
            else {
                Debug.LogError("No HealthSystem found on Player or its parents!");
            }
         }
    
    }
}

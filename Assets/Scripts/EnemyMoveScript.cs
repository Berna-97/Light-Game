using UnityEngine;
using TMPro;
using NUnit.Framework; // Add this for TextMeshPro support

public class EnemyMoveScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 40.0f;
    public float maxHealth = 4f;
    public TextMeshProUGUI healthText; // Reference to UI text element

    private Transform target;
    private float currentHealth;


    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        target = playerObj != null ? playerObj.transform : null;
        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }

    void Update()
    {
        //3d
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        //2d
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (target != null) { 
        var step = speed * Time.deltaTime;
        Vector3 destination = new(target.position.x, 0, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }

    }

        public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative health
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = $"Enemy Health: {currentHealth:F0}/{maxHealth:F0}";
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void ChangeForm()
    {

    }
}
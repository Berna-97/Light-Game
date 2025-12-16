using UnityEngine;

public class Opacity : MonoBehaviour
{
    public SpriteRenderer sprite;      // Drag in Inspector or auto-get
    public HealthSystem playerHealth;        // Reference to your health script

    void Start()
    {
        // Auto-get SpriteRenderer if none assigned
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateOpacity(playerHealth.health);
    }

    void UpdateOpacity(int health)
    {
        // Clamp between 1 and 5
        health = Mathf.Clamp(health, 1, 5);

        float opacity;

        switch (health)
        {
            case 5:
                opacity = 1.0f;
                break;

            case 4:
                opacity = 0.7f;
                break;

            case 3:
                opacity = 0.45f;
                break;

            case 2:
                opacity = 0.2f;
                break;

            case 1:
                opacity = 0.05f;
                break;

            default:
                opacity = 0.01f; // fallback if health is outside 1–5
                break;
        }

        // Apply alpha while keeping the sprite fully white
        Color c = sprite.color;
        c.a = opacity;
        sprite.color = c;
    }
}
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Opacity : MonoBehaviour
{
    public SpriteRenderer sprite;      // Drag in Inspector or auto-get
    //public HealthSystem playerHealth;        // Reference to your health script
    public Material material;

    void Start()
    {
        // Auto-get SpriteRenderer if none assigned
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();

        if (material == null)
            material = sprite.material;
    }

    //void Update()
    //{
    //    UpdateOpacity(playerHealth.health);
    //}

    public void UpdateOpacity(int health)
    {
        // Clamp between 1 and 5
        //health = Mathf.Clamp(health, 1, 5);

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

        material.SetFloat("_Glow_Amount", health - 0.5f);
        // Apply alpha while keeping the sprite fully white
        Debug.Log(opacity);
        Color c = sprite.color;
        c.a = opacity;
        sprite.color = c;
    }
}
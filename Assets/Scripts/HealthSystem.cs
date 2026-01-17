using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthSystem : MonoBehaviour
{
    public GameOverScreen deathScreen;
    public int health;
    public int maxHealth = 5;
    public int minHealth = 0;
    public int damage = 1;

    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFlashing = false;

    public void GameOver() {  

        //death sfx
        deathScreen.Setup();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        AudioSource sfx = GameObject.Find("DamagedSFX").GetComponent<AudioSource>();
        sfx.Play();

        if (spriteRenderer != null && !isFlashing)
        {
            StartCoroutine(DamageFlash());
        }

        if (health < 9000) { 
        Opacity opacityScript = this.GetComponent<Opacity>();
        opacityScript.UpdateOpacity(health);
        }


        if (health <= minHealth)
        {
            Destroy(this.gameObject);
            GameOver();
        }
    }

    private IEnumerator DamageFlash()
    {
        isFlashing = true;

        // Flash red
        spriteRenderer.color = flashColor;

        // Wait for flash duration
        yield return new WaitForSeconds(flashDuration);

        // Return to original color
        spriteRenderer.color = originalColor;

        isFlashing = false;
    }

}

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EnemyMoveScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 40.0f;
    public float maxHealth = 4f;
    public TextMeshProUGUI healthText;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    public float flashDuration = 0.1f;
    public float spawnDuration = 1.0f;
    public float deathDuration = 0.5f;

    private Transform target;
    private float currentHealth;
    private bool isKnockedBack = false;
    private bool isSpawning = true;
    private bool isDying = false;
    private Vector3 knockbackVelocity;

    public SpriteRenderer spriteRenderer;
    public Sprite hexagon;
    public Sprite pentagon;
    public Sprite square;
    public Sprite triangle;
    public Sprite twoangle;
    public Sprite line;

    public bool isGate;
    public bool isSingleButton;
    public GameObject gate;
    private Color originalColor;
    public bool isBlocked = false;
    private AudioSource damaged;

    private Material enemyMaterial;

    private List<GameObject> repetitions;

    public GameObject hitVFXPrefab;
    public float VFXDestroyDelay = 2f;

    private void Awake()
    {
        GameObject damagedSfx = GameObject.Find("DamagedSFX");
        damaged = damagedSfx.GetComponent<AudioSource>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        target = playerObj != null ? playerObj.transform : null;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store the original color

        // Get or create material instance
        enemyMaterial = spriteRenderer.material;

        if (speed == 0)
        {
            isGate = true;
        }
        else
        {
            isGate = false;
        }
    }

    private void Start()
    {
        SetHealthToMax();
        UpdateHealth();

        // Start the spawn animation
        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation()
    {
        float elapsed = 0f;

        // Set initial dissolve value to 1
        if (enemyMaterial.HasProperty("_DissolveAmount"))
        {
            enemyMaterial.SetFloat("_DissolveAmount", 1f);
        }

        while (elapsed < spawnDuration)
        {
            elapsed += Time.deltaTime;
            float dissolveValue = Mathf.Lerp(1f, 0f, elapsed / spawnDuration);

            // Update the dissolve property
            if (enemyMaterial.HasProperty("_DissolveAmount"))
            {
                enemyMaterial.SetFloat("_DissolveAmount", dissolveValue);
            }

            yield return null;
        }

        // Ensure dissolve is exactly 0 at the end
        if (enemyMaterial.HasProperty("_DissolveAmount"))
        {
            enemyMaterial.SetFloat("_DissolveAmount", 0f);
        }

        // Spawning complete, enemy can now move
        isSpawning = false;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (target != null && !isGate && !isKnockedBack && !isSpawning && !isDying)
        {
            var step = speed * Time.deltaTime;
            Vector3 destination = new(target.position.x, 0, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }

        if (isKnockedBack)
        {
            transform.position += knockbackVelocity * Time.deltaTime;
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    public void TakeDamage(float damage, Vector3 projectilePosition)
    {
        if (!isGate)
        {
            ApplyKnockback(projectilePosition);
        }

        StartCoroutine(FlashRed());
        damaged.Play();

        if(!isBlocked) { 
        PlayHitVFX();
        }

        repetitions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Repetition"));

        if (repetitions.Count > currentHealth)
        {
            isBlocked = true;
        }
        if (repetitions.Count == currentHealth && !isBlocked)
        {
            repetitions.Clear();
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative health
            UpdateHealth();

            if (currentHealth <= 0)
            {
                Die();

                foreach (GameObject rep in repetitions)
                {
                    Destroy(rep);
                }
            }
        }
        if (repetitions.Count == 1)
        {
            isBlocked = false;
        }


    }

    private void PlayHitVFX()
    {
        if (hitVFXPrefab != null)
        {
            // Instantiate the VFX at the enemy's position with optional offset
            Vector3 spawnPosition = transform.position;
            GameObject vfxInstance = Instantiate(hitVFXPrefab, spawnPosition, Quaternion.identity);

            // Destroy the VFX after a delay
            Destroy(vfxInstance, VFXDestroyDelay);
        }
        else
        {
            Debug.LogWarning("Hit VFX Prefab is not assigned on " + gameObject.name);
        }
    }


    private void ApplyKnockback(Vector3 projectilePosition)
    {
        Vector3 knockbackDirection = (transform.position - projectilePosition).normalized;
        knockbackVelocity = knockbackDirection * knockbackForce;

        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
        knockbackVelocity = Vector3.zero;
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private void UpdateHealth()
    {
        if (!isGate)
        {
            ChangeForm();
        }
    }

    public void Die()
    {
        if (gate != null)
        {
            if (isGate)
            {
                gate.GetComponent<GateScript>().enabled = true;
            }
            if (isSingleButton)
            {
                Transform gate2 = transform.parent.Find("Walls/Gate2");
                if (gate2.GetComponent<GateScript>() != null)
                {
                    gate2.GetComponent<GateScript>().enabled = true;
                }
            }
        }

        if (!isGate)
        {
            StartCoroutine(DeathAnimation());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DeathAnimation()
    {
        isDying = true;
        float elapsed = 0f;

        // Set initial dissolve value to 0
        if (enemyMaterial.HasProperty("_DissolveAmount"))
        {
            enemyMaterial.SetFloat("_DissolveAmount", 0f);
        }

        while (elapsed < deathDuration)
        {
            elapsed += Time.deltaTime;
            float dissolveValue = Mathf.Lerp(0f, 1f, elapsed / deathDuration);

            // Update the dissolve property
            if (enemyMaterial.HasProperty("_DissolveAmount"))
            {
                enemyMaterial.SetFloat("_DissolveAmount", dissolveValue);
            }

            yield return null;
        }

        // Ensure dissolve is exactly 1 at the end
        if (enemyMaterial.HasProperty("_DissolveAmount"))
        {
            enemyMaterial.SetFloat("_DissolveAmount", 1f);
        }

        Destroy(this.gameObject);
    }

    public void ChangeForm()
    {
        if (currentHealth == 6)
        {
            spriteRenderer.sprite = hexagon;
            ChangeSpeed(1f);
        }
        if (currentHealth == 5)
        {
            spriteRenderer.sprite = pentagon;
            ChangeSpeed(1.5f);
        }
        if (currentHealth == 4)
        {
            spriteRenderer.sprite = square;
            ChangeSpeed(2f);
        }
        if (currentHealth == 3)
        {
            spriteRenderer.sprite = triangle;
            ChangeSpeed(3f);
        }
        if (currentHealth == 2)
        {
            spriteRenderer.sprite = twoangle;
            ChangeSpeed(3f);
        }
        if (currentHealth == 1)
        {
            spriteRenderer.sprite = line;
            ChangeSpeed(3f);
        }
    }

    void ChangeSpeed(float newSpeed)
    {
        if (!isGate)
        {
            speed = newSpeed;
        }
    }

    IEnumerator HealthTimer()
    {
        yield return new WaitForSeconds(0.7f);
        currentHealth = maxHealth;
    }

    public void SetHealthToMax()
    {
        currentHealth = maxHealth;
        ChangeForm();
    }
}
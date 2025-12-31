using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EnemyMoveScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 40.0f;
    public float maxHealth = 4f;
    public TextMeshProUGUI healthText; // Reference to UI text element
    public float knockbackForce = 5f; // Knockback strength
    public float knockbackDuration = 0.2f; // How long knockback lasts
    public float flashDuration = 0.1f; // How long the red flash lasts

    private Transform target;
    private float currentHealth;
    private bool isKnockedBack = false;
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



    private List<GameObject> repetitions;

    private void Awake()
    {
        GameObject damagedSfx = GameObject.Find("DamagedSFX");
        damaged = damagedSfx.GetComponent<AudioSource>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        target = playerObj != null ? playerObj.transform : null;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store the original color

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
        //ChangeForm();

    }

    void Update()
    {
        //3d
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        //2d
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (target != null && !isGate && !isKnockedBack)
        {
            var step = speed * Time.deltaTime;
            Vector3 destination = new(target.position.x, 0, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }

        // Apply knockback movement
        if (isKnockedBack)
        {
            transform.position += knockbackVelocity * Time.deltaTime;
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    [System.Obsolete]
    public void TakeDamage(float damage, Vector3 projectilePosition)
    {
        if (!isGate)
        {
            ApplyKnockback(projectilePosition);
        }

        StartCoroutine(FlashRed());
        damaged.Play();



        repetitions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Repetition"));
      

        if(repetitions.Count > currentHealth)
        {
            isBlocked = true;
        }
        if(repetitions.Count == currentHealth && !isBlocked)
        {
            repetitions.Clear();
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative health
            UpdateHealth();


            //if (isGate) { StartCoroutine("HealthTimer"); }

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
        //repetitions.Clear();

    }

    /*
     public void TakeDamage(float damage)
    {
        TakeDamage(damage, target != null ? target.position : transform.position);
    }
    */

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

    private void Die()
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
        else
        {
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
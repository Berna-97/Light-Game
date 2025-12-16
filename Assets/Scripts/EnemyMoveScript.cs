using UnityEngine;
using TMPro;
using NUnit.Framework; // Add this for TextMeshPro support
using NUnit.Framework.Internal;
using System.Collections;

public class EnemyMoveScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 40.0f;
    public float maxHealth = 4f;
    public TextMeshProUGUI healthText; // Reference to UI text element

    private Transform target;
    private float currentHealth;

    public SpriteRenderer spriteRenderer;
    public Sprite hexagon;
    public Sprite pentagon;
    public Sprite square;
    public Sprite triangle;
    public Sprite twoangle;
    public Sprite line;

    private bool isGate;
    public bool isSingleButton;
    public GameObject gate;


    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        target = playerObj != null ? playerObj.transform : null;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

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
    }

    void Update()
    {
        //3d
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        //2d
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (target != null && !isGate) { 
        var step = speed * Time.deltaTime;
        Vector3 destination = new(target.position.x, 0, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }

    }

        public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative health
        UpdateHealth();

        if (isGate) { StartCoroutine("HealthTimer"); }
       

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealth()
    {
        if (healthText != null)
        {
            healthText.text = $"Enemy Health: {currentHealth:F0}/{maxHealth:F0}";
        }
        if (!isGate)
        {
            ChangeForm();
        }
        
    }

    private void Die()
    {
        if (gate !=null) { 
            if (speed == 0)
            {
                gate.GetComponent<GateScript>().enabled = true;
            }
            if (isSingleButton)
            {
                Transform gate2 = transform.parent.Find("Walls/Gate2");
                Debug.Log(gate2);
                if (gate2.GetComponent<GateScript>() != null)
                {
                    gate2.GetComponent<GateScript>().enabled = true;
                }
            }
        }
        Destroy(this.gameObject);
    }

    private void ChangeForm()
    {
        if (currentHealth == 6)
        {
            spriteRenderer.sprite = hexagon;
            speed = 1f;
        }
        if (currentHealth == 5)
        {
            spriteRenderer.sprite = pentagon;
            speed = 1.5f;
        }
        if (currentHealth == 4)
        {
            spriteRenderer.sprite = square;
            speed = 2f;
        }
        if (currentHealth == 3)
        {
            spriteRenderer.sprite = triangle;
            speed = 3f;
        }
        if (currentHealth == 2)
        {
            spriteRenderer.sprite = twoangle;
            speed = 3f;
        }
        if (currentHealth == 1)
        {
            spriteRenderer.sprite = line;
            speed = 3f;
        }
        else
        {
            //nothing
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
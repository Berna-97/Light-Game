using UnityEngine;
using TMPro;
using NUnit.Framework; // Add this for TextMeshPro support
using NUnit.Framework.Internal;
using UnityEngine.Splines.ExtrusionShapes;

public class GateButtonScript : MonoBehaviour
{
    public float rotationSpeed = 40.0f;
    public float maxHealth = 4f;

    private Transform target;
    private float currentHealth;

    private float timer = 0;
    private bool tookDamage = false;

    public SpriteRenderer spriteRenderer;
    public Sprite hexagon;
    public Sprite pentagon;
    public Sprite square;
    public Sprite triangle;


    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        target = playerObj != null ? playerObj.transform : null;
        ResetHp();
    }

    void Update()
    {
        print(currentHealth);
        //3d
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        //2d
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (tookDamage)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer > 2)
            {
                tookDamage = false;
                currentHealth = maxHealth;
                ChangeForm();

            }
        }
    }

    //public void takedamage(float damage)
    //{
    //    debug.log("yes");
    //    currenthealth -= damage;
    //    currenthealth = mathf.max(currenthealth, 0); // prevent negative health

    //    if (currenthealth <= 0)
    //    {
    //        die();
    //    }

    //    tookdamage = true;
    //}


    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void ResetHp()
    {
        currentHealth = maxHealth;
    }

    private void ChangeForm()
    {
        if (currentHealth == 6)
        {
            spriteRenderer.sprite = hexagon;
        }
        if (currentHealth == 5)
        {
            spriteRenderer.sprite = pentagon;
        }
        if (currentHealth == 4)
        {
            spriteRenderer.sprite = square;
        }
        if (currentHealth == 3)
        {
            spriteRenderer.sprite = triangle;
        }
        else
        {
            //nothing
        }
    }

}

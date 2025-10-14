using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// O inimigo move-se em direção ao player, com uma ligeira rotação
public class EnemyMoveScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 40.0f;
    public float damageRange = 2.0f;
    private Transform target;
    


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);


        //Vector3 pos = transform.position;
        //pos.x += 2 * Time.deltaTime; // usa Time.deltaTime para movimento suave
        //transform.position = pos;

        // Move our position a step closer to the target.
        var step = speed * Time.deltaTime; // calculate distance to move
        Vector3 destination = new (target.position.x, 0, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < damageRange)
        {
            Destroy(this.gameObject);
        }
    }
}

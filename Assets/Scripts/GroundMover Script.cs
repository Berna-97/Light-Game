using Unity.VisualScripting;
using UnityEngine;

// O chão move-se uniformemente de acordo com uma velocidade definida por groundSpeed
public class GroundMoverScript : MonoBehaviour
{
    int groundSpeed = 15;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(groundSpeed * Time.deltaTime, 0, 0);

        Destroy(gameObject, groundSpeed * Time.deltaTime * 70);

    }
}

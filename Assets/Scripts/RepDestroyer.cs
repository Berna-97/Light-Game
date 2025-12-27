using NUnit.Framework;
using UnityEngine;

public class RepDestroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RepDestroy()
    {

        Destroy(this.gameObject);
     
    }
}

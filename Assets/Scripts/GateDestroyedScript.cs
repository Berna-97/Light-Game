using Unity.VisualScripting;
using UnityEngine;

public class GateDestroyedScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem[] particleSystems;
    private void OnTriggerEnter(Collider other)
    {
        GameObject gate1 = GameObject.Find("Walls/Gate1");
        GameObject gate2 = GameObject.Find("Walls/Gate2");
        GateScript script1 = gate1.GetComponent<GateScript>();
        GateScript script2 = gate2.GetComponent<GateScript>();

        if (other.gameObject.CompareTag("Player") && (!script1.isActiveAndEnabled || !script2.isActiveAndEnabled))
        {
            AudioSource sfx = GameObject.Find("DoorCrashSFX").GetComponent<AudioSource>();
            sfx.Play();


            GameObject vfx = GameObject.Find("VfxExplosion");
            particleSystems = vfx.GetComponentsInChildren<ParticleSystem>(true);

            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
            }
        }

    }

}

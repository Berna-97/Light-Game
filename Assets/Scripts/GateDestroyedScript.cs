using UnityEngine;

public class GateDestroyedScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem[] particleSystems;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
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

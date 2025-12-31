using UnityEngine;

public class LightUpScripts : MonoBehaviour
{
    Material[] materials;

    public float maxIntensity = 5f;
    public float speed = 2f;

    float currentIntensity = 0f;
    bool isTriggered = false;
    AudioSource sfx;

    void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
            materials[i].EnableKeyword("_EMISSION");
            materials[i].SetColor("_EmissionColor", Color.black);
        }
    }

    void Update()
    {
        if (!isTriggered) return;

        currentIntensity = Mathf.MoveTowards(
            currentIntensity,
            maxIntensity,
            speed * Time.deltaTime
        );

        foreach (Material mat in materials)
        {
            mat.SetColor("_EmissionColor", Color.white * currentIntensity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            sfx = GameObject.Find("LightUpSFX").GetComponent<AudioSource>();
            sfx.Play();
        }
    }
}

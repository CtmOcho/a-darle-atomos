using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class SodiumExplosion : MonoBehaviour
{
    public float duration;
    public float force;
    private VisualEffect vfx;
    private AudioSource audioSource;
    private Rigidbody rb;
    private MeshRenderer mesh;
    public float sodiumMass;


    public SodiumLabProgressController progressController;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponentInChildren<VisualEffect>();
        vfx.Stop();
        audioSource = GetComponentInChildren<AudioSource>();
        rb = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<MeshRenderer>();
        sodiumMass = rb.mass;
        progressController = FindObjectOfType<SodiumLabProgressController>();

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Liquid"))
        {

            StartCoroutine(ExplosionCorroutine());
            StartCoroutine(ReduceSize());
        }
    }

    IEnumerator ExplosionCorroutine()
    {
        float time = 0;
        vfx.Play();
        yield return new WaitForSeconds(duration / 5);
        vfx.SendEvent(Shader.PropertyToID("OnPlayExplosion"));
        audioSource.Play();
        if (sodiumMass >= 0.12f)
        {
            progressController.blinkingIntermediaryStart();
        }
        while (time < duration)
        {
            ApplyRandomForce();
            float randFloat = Random.Range(0.5f, 2);
            yield return new WaitForSeconds(randFloat);
            time += randFloat;
        }
        vfx.Stop();
        audioSource.Stop();

        if (sodiumMass >= 0.12f)
        {
            progressController.blinkingIntermediaryEnd();
        }
        Destroy(gameObject);

        progressController.sodiumComsumed = true;

    }

    IEnumerator ReduceSize()
    {
        float deltaTime = 0;
        while (true)
        {
            float newSize = 1 - (deltaTime / duration);
            mesh.transform.localScale = new Vector3(newSize, newSize, newSize);
            yield return new WaitForUpdate();
            deltaTime += Time.deltaTime;
        }
    }

    void ApplyRandomForce()
    {
        Vector3 randVector = Random.onUnitSphere;
        randVector.y = 0;
        rb.AddForce(randVector * force);
    }
}

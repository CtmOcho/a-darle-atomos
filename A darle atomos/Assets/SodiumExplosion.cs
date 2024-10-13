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
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponentInChildren<VisualEffect>();
        audioSource = GetComponentInChildren<AudioSource>();
        rb = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<MeshRenderer>();
        StartCoroutine(ExplosionCorroutine());
        StartCoroutine(ReduceSize());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ExplosionCorroutine()
    {
        float time = 0;
        vfx.Play();
        audioSource.Play();
        while (time < duration)
        {
            ApplyRandomForce();
            float randFloat = Random.Range(0.5f, 2);
            yield return new WaitForSeconds(randFloat);
            time += randFloat;
        }
        vfx.Stop();
        audioSource.Stop();
        Destroy(gameObject);
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

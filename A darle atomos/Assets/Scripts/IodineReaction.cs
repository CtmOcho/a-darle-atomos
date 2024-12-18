using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class IodineReaction : MonoBehaviour
{
    public VisualEffect vfx;
    private MeshRenderer mesh;
    private Transform iodine;
    private float elapsedTime;
    public float duration;
    public Vector3 originalScale;
    private Transform generator;
    public float temperature;

    // Start is called before the first frame update
    void Start()
    {
        vfx.SetInt("spawn_rate", 0);
        iodine = GetComponent<Transform>();
        mesh = GetComponent<MeshRenderer>();
        GetComponentInChildren<inverseSublimation>().isActive = false;
        originalScale = iodine.localScale;
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (temperature > 113)
        {
            GetComponentInChildren<inverseSublimation>().isActive = true;
            Sublimate();
        }
    }

    void Sublimate()
    {
        elapsedTime += Time.fixedDeltaTime;
        vfx.SetInt("spawn_rate", 50);
        iodine.localScale = Vector3.Lerp(originalScale, new Vector3(0, 0, 0), elapsedTime / duration);
        if (iodine.localScale.magnitude < 0.000001)
        {
            mesh.enabled = false;
            GetComponentInChildren<inverseSublimation>().isActive = false;
            vfx.SetInt("spawn_rate", 0);
            Destroy(transform.gameObject);
        }
    }
}
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
    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        vfx.SetInt("spawn_rate", 0);
        iodine = GetComponent<Transform>();
        mesh = GetComponent<MeshRenderer>();
        on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            StartCoroutine(Sublimate());
            on = !on;
        }
    }

    IEnumerator Sublimate()
    {
        Vector3 originalScale = iodine.localScale;
        vfx.SetInt("spawn_rate", 50);
        for (int i = 0; i < 300; i++)
        {
            yield return new WaitForFixedUpdate();
            iodine.localScale -= originalScale / 500;
        }
        mesh.enabled = false;
        vfx.SetInt("spawn_rate", 0);
        yield return new WaitForSeconds(3);
        Destroy(transform.parent.gameObject);
    }
}

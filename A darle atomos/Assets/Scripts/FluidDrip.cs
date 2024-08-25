using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrip : MonoBehaviour
{
    public GameObject dripPrefab;
    public float dripInterval = 1.0f;

    private void Start()
    {
        StartCoroutine(DripCoroutine());
    }

    private IEnumerator DripCoroutine()
    {
        while (true)
        {
            Instantiate(dripPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(dripInterval);
        }
    }
}
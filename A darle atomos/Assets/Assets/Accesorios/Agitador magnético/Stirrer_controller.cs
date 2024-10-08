using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stirrer_controller : MonoBehaviour
{
    public GameObject liquid;
    public GameObject stirBar;
    public GameObject bubbles;
    public Renderer objectRenderer;

    public float liquidHeight = 0.4f;
    public bool isActive = false;
    bool stirFlag = false;

    float stirCounter = 0;
    float barCounter = 0;
    public ChangeColor changeColor;

    void Start()
    {
        liquid.transform.localScale = new Vector3(1f, liquidHeight, 1f);
        liquid.transform.GetChild(0).transform.localScale = new Vector3(1f, 0.001f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {

            if (!stirFlag)
            {
                stirCounter = 0f;
                barCounter = 0f;
                StopAllCoroutines();
                StartCoroutine(rotateBar());
                StartCoroutine(stirLiquidStart());
                changeColor.ColorChange();
                stirFlag = true;
            }

        }
        else
        {
            if (stirFlag)
            {
                stirCounter = 1f;
                barCounter = 1f;
                StopAllCoroutines();
                StartCoroutine(stopBar());
                StartCoroutine(stirLiquidStop());
                stirFlag = false;
            }
        }
    }
    IEnumerator rotateBar()
    {
        while (true)
        {
            barCounter += 0.01f;
            float barAngleY = Mathf.Lerp(0, 30f, barCounter);
            stirBar.transform.Rotate(0, barAngleY, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator stopBar()
    {
        while (true)
        {
            barCounter -= 0.004f;
            float barAngleY = Mathf.Lerp(0, 30f, barCounter);
            stirBar.transform.Rotate(0, barAngleY, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator stirLiquidStart()
    {
        objectRenderer.material.SetFloat("_bool", 1);
        bubbles.SetActive(true);
        while (true)
        {
            stirCounter += 0.0001f;
            float yScale = Mathf.Lerp(liquid.transform.GetChild(0).transform.localScale.y, liquidHeight, stirCounter);
            liquid.transform.GetChild(0).transform.localScale = new Vector3(1f, yScale, 1f);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator stirLiquidStop()
    {
        objectRenderer.material.SetFloat("_bool", 0);
        bubbles.SetActive(false);
        while (true)
        {
            stirCounter -= 0.0001f;
            float yScale = Mathf.Lerp(0.001f, liquid.transform.GetChild(0).transform.localScale.y, stirCounter);
            liquid.transform.GetChild(0).transform.localScale = new Vector3(1f, yScale, 1f);
            yield return new WaitForSeconds(0.03f);
        }
    }
}

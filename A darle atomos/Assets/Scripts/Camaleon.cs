using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camaleon : MonoBehaviour
{

    public Color[] colors;
    public float delay;
    public ChangeColor changeColorScript;

    public void StartCamaleonRutina(){

        StartCoroutine(CamaleonCoroutine());

    }

    public IEnumerator CamaleonCoroutine()
    {
        //Debug.Log("Cambio de color camaleon");
        for (int i = 0; i < colors.Length; i++)
        {
            changeColorScript.ColorChange(colors[i]);
            yield return new WaitForSeconds(delay);
        }
    }
}

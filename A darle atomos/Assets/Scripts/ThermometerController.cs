using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThermometerController : MonoBehaviour
{
    TextMeshPro thermometerLCD;

    public float speed;
    public float minTemp;
    public float maxTemp;

    public bool pause = true;
    public bool chill = false;

    public AnimationCurve warmOverTime;
    public AnimationCurve chillOverTime;

    float timeCounter = 0;

    void Start()
    {
        thermometerLCD = GetComponentInChildren<TextMeshPro>();
        thermometerLCD.text = minTemp.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = Mathf.Clamp(timeCounter,0,25);
        if (!pause)
        {
            if (!chill)
            {
                timeCounter += Time.deltaTime;
                thermometerLCD.text = Mathf.Lerp(minTemp, maxTemp, warmOverTime.Evaluate(timeCounter * speed)).ToString("F1");
            }
            else
            {
                timeCounter -= Time.deltaTime;
                thermometerLCD.text = Mathf.Lerp(minTemp, maxTemp, chillOverTime.Evaluate(timeCounter * speed)).ToString("F1");
            }

        }
    }
}

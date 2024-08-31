using TMPro;
using UnityEngine;

public class AutoclaveController : MonoBehaviour
{
    public TextMeshPro tempLCD;
    public TextMeshPro pressureLCD;

    public float temperature;
    public float pressure;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tempLCD.text = temperature.ToString("F1");
        pressureLCD.text = pressure.ToString("F1");
    }
}

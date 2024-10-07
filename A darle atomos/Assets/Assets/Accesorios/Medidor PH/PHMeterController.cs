using TMPro;
using UnityEngine;

public class PHMeterController : MonoBehaviour
{
    public DropCollisionController properties;

    TextMeshPro phLCD;
    // Start is called before the first frame update
    void Start()
    {

        phLCD = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        phLCD.text = properties.actualPHvalue.ToString("F2");
    }
}

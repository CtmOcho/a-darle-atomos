using UnityEngine;
using TMPro;

public class ParticleCounterController : MonoBehaviour
{
    public bool isOn = false;
    public TextMeshPro counterTMP;

    bool moved = false;
    public bool labCompleted = false;
    int particleCounter = 0;
    public void RotatePlus()
    {
        if(isOn)
        {
            if (transform.GetChild(0).transform.localEulerAngles.z < 270.0f)
            {
                transform.GetChild(0).transform.Rotate(0, 0, 15);
                moved = true;
            }
        }
        
    }
    public void RotateMinus()
    {
        if(isOn)
        {
            if (transform.GetChild(0).transform.localEulerAngles.z > 180.0f)
            {
                transform.GetChild(0).transform.Rotate(0, 0, -15);
                moved = true;

            }
        }
    }
    public void OnOff()
    {
        if (!isOn) 
        { 
            particleCounter = 0;
            counterTMP.text = particleCounter.ToString();
        }
        else counterTMP.text = "";
        isOn = !isOn;    
    }
    public void updateCounter()
    {
        particleCounter++;
        counterTMP.text = particleCounter.ToString();
        if(moved){
            labCompleted = true;
        }
    }
}

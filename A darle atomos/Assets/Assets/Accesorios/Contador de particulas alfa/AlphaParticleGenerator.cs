using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlphaParticleGenerator : MonoBehaviour
{
    public ParticleCounterController controller;
    public GameObject alphaParticle;
    public int maxParticles;
    public AnimationCurve weight;
    [Header("Variables Experiencia Molecular")]
    public bool isSubExp = false;
    public GameObject mainCam;
    public GameObject povCam;
    public GameObject titulos;
    public GameObject panel;
    public TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI explanation;

    int subCounter;
    int swapTimeoutCounter;
    float randAngle;
    float randRadius;
    float radius;
    bool mainCamFlag;

    GameObject[] particleArr;
    GameObject[] particleCameraArr;
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        subCounter = 0;
        
        if (isSubExp) 
        {
            radius = 0.6f;
            povCam.GetComponent<Camera>().enabled = false; 
        }
        else
        {
            radius = 0.006f;
        }
        mainCamFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isOn)
        {
            particleArr = GameObject.FindGameObjectsWithTag("alphaParticle");
            particleCameraArr = GameObject.FindGameObjectsWithTag("particleCam");
            if (particleArr.Length < maxParticles && subCounter > 50)
            {
                randAngle = UnityEngine.Random.Range(0, 360);
                randRadius = Mathf.Lerp(0, radius, GetWeightedNumber());
                Vector3 point = new Vector3(randRadius, 0, 0);
                Vector3 rotatedPoint = Quaternion.Euler(0, 0, randAngle) * point;
                GameObject particle = Instantiate(alphaParticle, transform.TransformPoint(rotatedPoint), transform.rotation);
                Destroy(particle, 3);
                if (isSubExp && particleCameraArr.Length < 1)
                { 
                    GameObject povCamInstance = Instantiate(povCam, transform.TransformPoint(rotatedPoint), transform.rotation);
                    povCamInstance.GetComponent<Camera>().enabled = true;
                    povCamInstance.tag = "particleCam";
                    povCamInstance.transform.SetParent(particle.transform, false);
                    povCamInstance.transform.localPosition = Vector3.zero;
                }
                subCounter = 0;
            }
            subCounter++;
        }
    }
    public float GetWeightedNumber()
    {
        return weight.Evaluate(UnityEngine.Random.value);
    }

    public void SwapView()
    {
        //Si mainCamFlag es true, entonces se activa la povCam, de lo contrario, se activa la mainCam
        if (isSubExp)
        {
            mainCamFlag = !mainCamFlag;
            swapTimeoutCounter = 0;
            controller.isOn = mainCamFlag;
            titulos.SetActive(!mainCamFlag);
            panel.SetActive(mainCamFlag);
            particleCameraArr = GameObject.FindGameObjectsWithTag("particleCam");
            while (swapTimeoutCounter < 500)
            {
                if (particleCameraArr.Length >= 1)
                {
                    particleCameraArr[0].GetComponent<Camera>().enabled = mainCamFlag;
                    if (!mainCamFlag) Destroy(particleCameraArr[0]); 
                    mainCam.GetComponent<Camera>().enabled = !mainCamFlag;
                    break;
                }
                swapTimeoutCounter++;
            }
            
            if (!mainCamFlag)
            {
                buttonText.text = "INCIAR";
                explanation.text = "En esta experiencia, observará el experimento de Rutherford desde la perspectiva de una particula alfa.";
            }
            else 
            { 
                buttonText.text = "DETENER";
                explanation.text = "Se puede observar que los átomos de oro están compuestos mayoritariamente de vacío, es por esto que algunas partículas rebotan y otras traspasan la lámina de oro.";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BoilingBehaviour : MonoBehaviour
{
    private Glass glass;
    private EdgeSlide edgeSlide;
    private FluidDrip fluidDrip;
    private BubblesAnimation bubblesAnimation;
    private AudioSource audioSource;
    private VisualEffect[] visualEffects;
    private ThermometerController termoController;

    public GameObject liquido;
    public bool LabCompleted = false;
    private bool ethanolBoiled = false;
    private distillationController distillationCtrl;

    void Start()
    {
        glass = GetComponentInChildren<Glass>();
        edgeSlide = GetComponentInChildren<EdgeSlide>();
        fluidDrip = GetComponentInChildren<FluidDrip>();
        bubblesAnimation = GetComponentInChildren<BubblesAnimation>();
        audioSource = GetComponentInChildren<AudioSource>();
        visualEffects = GetComponentsInChildren<VisualEffect>();
        termoController = GetComponentInChildren<ThermometerController>();

        if (liquido != null)
        {
            distillationCtrl = liquido.GetComponent<distillationController>();
        }
    }

    void Update()
    {
        if (!LabCompleted)
        {
            if (glass.temperature >= 80 && !ethanolBoiled)
            {
                StartCoroutine(BoilingEthanol());
            }/*
            else if (glass.temperature >= 100f && ethanolBoiled)
            {
                termoController.isDestilationLab = true;
                StartCoroutine(BoilingWater());
            }*/
        }
    }

    private IEnumerator BoilingEthanol()
    {
        // Activar efectos de ebullición
        edgeSlide.enabled = true;
        audioSource.enabled = true;
        fluidDrip.enabled = true;
        bubblesAnimation.enabled = true;
        visualEffects[0].enabled = true;
        visualEffects[1].enabled = true;
        distillationCtrl.enabled = true;

        // Esperar a que el volumen alcance el máximo
        if (distillationCtrl != null)
        {
            while (distillationCtrl.volume < distillationCtrl.maxVolume)
            {
                yield return null;
            }
        }

        // Desactivar efectos de ebullición
        edgeSlide.enabled = false;
        audioSource.enabled = false;
        Destroy(fluidDrip.gameObject);
        Destroy(bubblesAnimation.gameObject);
        visualEffects[0].enabled = false;
        visualEffects[1].enabled = false;
        distillationCtrl.enabled = false;

        // Configurar para la siguiente fase
        ethanolBoiled = true;
        glass.maxTemperature = 99.4f;
        distillationCtrl.maxVolume = 0.7f;
        LabCompleted = true;

    }
    /*
        private IEnumerator BoilingWater()
        {
            // Activar efectos de ebullición
            edgeSlide.enabled = true;
            audioSource.enabled = true;
            fluidDrip.enabled = true;
            bubblesAnimation.enabled = true;
            visualEffects[0].enabled = true;
            visualEffects[1].enabled = true;
            distillationCtrl.enabled = true;

            // Esperar a que el volumen alcance el máximo
            if (distillationCtrl != null)
            {
                while (distillationCtrl.volume < distillationCtrl.maxVolume)
                {
                    yield return null;
                }
            }

            // Desactivar efectos de ebullición y finalizar el experimento
            edgeSlide.enabled = false;
            audioSource.enabled = false;
            Destroy(fluidDrip.gameObject);
            Destroy(bubblesAnimation.gameObject);
            visualEffects[0].enabled = false;
            visualEffects[1].enabled = false;
            distillationCtrl.enabled = false;

            LabCompleted = true;
        }
        */
}

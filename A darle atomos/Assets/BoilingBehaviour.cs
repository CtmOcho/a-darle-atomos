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
    private bool isBoiling = false;
    public float boilingTime = 120;

    // Start is called before the first frame update
    void Start()
    {
        glass = GetComponentInChildren<Glass>();
        edgeSlide = GetComponentInChildren<EdgeSlide>();
        fluidDrip = GetComponentInChildren<FluidDrip>();
        bubblesAnimation = GetComponentInChildren<BubblesAnimation>();
        audioSource = GetComponentInChildren<AudioSource>();
        visualEffects = GetComponentsInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (glass.temperature >= 80 && isBoiling == false)
        {
            StartCoroutine(Boiling());
            isBoiling = true;
        }

    }

    public IEnumerator Boiling()
    {
        edgeSlide.enabled = true;
        audioSource.enabled = true;
        fluidDrip.enabled = true;
        bubblesAnimation.enabled = true;
        visualEffects[0].enabled = true;
        visualEffects[1].enabled = true;

        yield return new WaitForSeconds(boilingTime);

        edgeSlide.enabled = false;
        audioSource.enabled = false;
        Destroy(fluidDrip.gameObject);
        Destroy(bubblesAnimation.gameObject);
        visualEffects[0].enabled = false;
        visualEffects[1].enabled = false;
    }
}

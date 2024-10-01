using UnityEngine;


public class ParticleDetectorTrigger : MonoBehaviour
{
    public ParticleCounterController controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("alphaParticle"))
        {
            if(controller.isOn)
            {
                controller.updateCounter();
            }
        }
    }
}

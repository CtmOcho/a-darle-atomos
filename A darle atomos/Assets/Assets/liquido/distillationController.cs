using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distillationController : MonoBehaviour
{
    public float maxVolume = 100;
    public float volumeIncrease = 1;

    float volume = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (volume <= maxVolume)
        {
            volume += volumeIncrease;
            transform.localScale = new Vector3(1.1f, 1.1f, volume);
        }
    }
}

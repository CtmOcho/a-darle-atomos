using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorld : MonoBehaviour
{

    [Header("Tweaks")]
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;

    [Header("Logic")]
    private Camera cam;

    void Update()
    {
        Vector3 pos = lookAt.position + offset;

        if(transform.position != pos){
            transform.position = pos;
        }    
    }
}

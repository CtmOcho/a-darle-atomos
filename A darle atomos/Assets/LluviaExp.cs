using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LluviaExp : MonoBehaviour
{
    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void PlayAnimation(string name)
    {
        anim.Play(name);
    }
}
using System;
using UnityEngine;

public class AlphaParticle : MonoBehaviour
{
    public float particleSpeed = 0.1f;
    public AnimationCurve weight;
    public LayerMask mask;
    Vector3 destinationPos;

    public bool isSubExp = false;
    void Start()
    {
        
        if(isSubExp)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().AddForce(transform.forward * particleSpeed);
        }
        else
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, 5f, mask);
            destinationPos = hit.point;
            transform.LookAt(destinationPos);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSubExp)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos, particleSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destinationPos) < 0.001f)
            {
                float randAngle = Mathf.Lerp(0, 360, GetWeightedNumber());
                transform.Rotate(0, -randAngle, 0);
                destinationPos = destinationPos + transform.forward * 2;
            }
        }
        
    }
    public float GetWeightedNumber()
    {
        return weight.Evaluate(UnityEngine.Random.value);
    }
}

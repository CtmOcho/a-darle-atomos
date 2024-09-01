using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIodine : MonoBehaviour
{
    public GameObject vaso;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Iodine")
        {
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.transform.parent.transform.parent = vaso.transform;
            vaso.GetComponent<Glass>().contents.Add(other.gameObject);
        }
    }
}

using UnityEngine;

public class LockWatchGlass : MonoBehaviour
{
    public GameObject vaso;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "VidrioReloj")
        {
            other.gameObject.layer = 0;
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.transform.parent = vaso.transform;
            other.gameObject.transform.localPosition = new Vector3(0,0,0.14f);
            other.gameObject.transform.rotation = Quaternion.Euler(-90,0,-180);
        }
    }
}

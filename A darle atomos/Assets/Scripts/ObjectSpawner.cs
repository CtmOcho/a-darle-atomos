using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7 && !other.gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            GrabbableObjectInitPos g = other.gameObject.GetComponent<GrabbableObjectInitPos>();
            other.transform.SetPositionAndRotation(g.GetInitPos(),g.GetInitRot());
        }
    }
}

using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            GrabbableObjectInitPos g = other.gameObject.GetComponent<GrabbableObjectInitPos>();
            Instantiate(other.gameObject, g.GetInitPos(), g.GetInitRot());
            Destroy(other.gameObject);
        }
    }
}

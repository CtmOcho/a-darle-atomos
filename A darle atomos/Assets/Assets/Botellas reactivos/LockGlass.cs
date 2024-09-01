using UnityEngine;

public class LockGlass : MonoBehaviour
{
    public LayerMask layerM;
    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f, layerM)) 
        {
            transform.parent = null;
            foreach (Transform t in transform)
            {
                if (t.tag == "VidrioReloj") t.parent = null;
            }
            Destroy(GetComponent<Rigidbody>());
            transform.gameObject.layer = 0;
            transform.position = hit.collider.gameObject.transform.position;
            transform.rotation = Quaternion.Euler(-90, 0, -180);
        }
    }
}

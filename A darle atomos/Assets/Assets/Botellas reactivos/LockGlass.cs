using UnityEngine;

public class LockGlass : MonoBehaviour
{
    public LayerMask layerM;
    bool flag = true;
    private void Update()
    {
        if (flag) 
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f, layerM))
            {
                transform.parent = null;
                transform.position = hit.collider.gameObject.transform.position;
                transform.rotation = Quaternion.Euler(-90, 0, -180);
                transform.gameObject.layer = 0;
                foreach (Transform t in transform)
                {
                    if (t.CompareTag("VidrioReloj") || t.CompareTag("Iodine")) t.parent = null;
                    else t.gameObject.layer = 0;
                }
                foreach (Transform t in transform)
                {
                    if (t.CompareTag("GlassTrigger")) Destroy(t.gameObject);
                }
                //Destroy(GetComponent<Rigidbody>());
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().useGravity = true;
                flag = false;
            }
        }
    }
}

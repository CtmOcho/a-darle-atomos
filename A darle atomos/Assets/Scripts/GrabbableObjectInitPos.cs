using UnityEngine;

public class GrabbableObjectInitPos : MonoBehaviour
{
    Vector3 initPos;
    Quaternion initRot;
    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }
    public Vector3 GetInitPos() { return initPos; }
    public Quaternion GetInitRot() {  return initRot; }
}

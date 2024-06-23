using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    public Transform pivot;

    public float distanceThreshold;
    public LayerMask grabbableLayer;

    Transform lCurrentObject;
    Transform lThumbFingerTip;
    Transform lIndexFingerTip;
    Transform lMiddleFingerTip;
    Transform lRingFingerTip;
    Transform lWrist;
    Vector3 lThumbToIndexDist;
    Vector3 lWristToMiddleDist;
    Vector3 lWristToRingDist;

    Transform rCurrentObject;
    Transform rThumbFingerTip;
    Transform rIndexFingerTip;
    Transform rMiddleFingerTip;
    Transform rRingFingerTip;
    Transform rWrist;
    Vector3 rThumbToIndexDist;
    Vector3 rWristToMiddleDist;
    Vector3 rWristToRingDist;

    bool lGrabbingObject;
    bool rGrabbingObject;

    void Start()
    {
        lWrist = leftHand.transform.GetChild(1);
        lThumbFingerTip = lWrist.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        lIndexFingerTip = lWrist.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        lMiddleFingerTip = lWrist.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        lRingFingerTip = lWrist.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);

        rWrist = rightHand.transform.GetChild(1);
        rThumbFingerTip = rWrist.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        rIndexFingerTip = rWrist.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        rMiddleFingerTip = rWrist.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        rRingFingerTip = rWrist.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        lThumbToIndexDist = lThumbFingerTip.position - lIndexFingerTip.position;
        lWristToMiddleDist = lWrist.position - lMiddleFingerTip.position;
        lWristToRingDist = lWrist.position - lRingFingerTip.position;

        rThumbToIndexDist = rThumbFingerTip.position - rIndexFingerTip.position;
        rWristToMiddleDist = rWrist.position - rMiddleFingerTip.position;
        rWristToRingDist = rWrist.position - rRingFingerTip.position;
        if (lThumbToIndexDist.magnitude < distanceThreshold * 0.8f || lWristToMiddleDist.magnitude < distanceThreshold || lWristToRingDist.magnitude < distanceThreshold)
        {
            //Debug.DrawRay(lIndexFingerTip.position, lThumbToIndexDist, Color.white);
            //Debug.DrawRay(lMiddleFingerTip.position, lWristToMiddleDist, Color.white);
            //Debug.DrawRay(lRingFingerTip.position, lWristToRingDist, Color.white);
            if (Physics.Raycast(lIndexFingerTip.position, lThumbToIndexDist, out hit, distanceThreshold, grabbableLayer))
            {
                lGrabbingObject = true;
                lCurrentObject = hit.transform;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                lCurrentObject.parent = lThumbFingerTip;
            }
            else if (Physics.Raycast(lMiddleFingerTip.position, lWristToMiddleDist, out hit, distanceThreshold, grabbableLayer))
            {
                lGrabbingObject = true;
                lCurrentObject = hit.transform;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                lCurrentObject.parent = lWrist;
            }
            else if (Physics.Raycast(lRingFingerTip.position, lWristToRingDist, out hit, distanceThreshold, grabbableLayer))
            {
                lGrabbingObject = true;
                lCurrentObject = hit.transform;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                lCurrentObject.parent = lWrist;
            }
        }
        else
        {
            if(lGrabbingObject)
            {
                lCurrentObject.parent = null;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                lCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                lGrabbingObject = false;
            }
        }

        if (rThumbToIndexDist.magnitude < distanceThreshold*0.8f || rWristToMiddleDist.magnitude < distanceThreshold || rWristToRingDist.magnitude < distanceThreshold)
        {
            //Debug.DrawRay(lIndexFingerTip.position, lThumbToIndexDist, Color.white);
            //Debug.DrawRay(lMiddleFingerTip.position, lWristToMiddleDist, Color.white);
            //Debug.DrawRay(lRingFingerTip.position, lWristToRingDist, Color.white);
            if (Physics.Raycast(rIndexFingerTip.position, rThumbToIndexDist, out hit, distanceThreshold, grabbableLayer))
            {
                rGrabbingObject = true;
                rCurrentObject = hit.transform;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rCurrentObject.parent = rThumbFingerTip;
            }
            else if (Physics.Raycast(rMiddleFingerTip.position, rWristToMiddleDist, out hit, distanceThreshold, grabbableLayer))
            {
                rGrabbingObject = true;
                rCurrentObject = hit.transform;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rCurrentObject.parent = rWrist;
            }
            else if (Physics.Raycast(rRingFingerTip.position, rWristToRingDist, out hit, distanceThreshold, grabbableLayer))
            {
                rGrabbingObject = true;
                rCurrentObject = hit.transform;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rCurrentObject.parent = rWrist;
            }
        }
        else
        {
            if (rGrabbingObject)
            {
                rCurrentObject.parent = null;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                rCurrentObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                rGrabbingObject = false;
            }
        }

    }
}

using UnityEngine;

public class DataInterpreter : MonoBehaviour
{
    public Transform pivot;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject client;

    Transform lWrist;
    Transform lThumb;
    Transform lMiddle;
    Transform lRing;
    Transform lIndex;
    Transform lPinkie;

    Transform rWrist;
    Transform rThumb;
    Transform rMiddle;
    Transform rRing;
    Transform rIndex;
    Transform rPinkie;

    WebSocketClient ws;

    void Start()
    {
        lWrist = leftHand.transform.GetChild(0);
        lThumb = lWrist.transform.GetChild(0);
        lMiddle = lWrist.transform.GetChild(1);
        lRing = lWrist.transform.GetChild(2);
        lIndex = lWrist.transform.GetChild(3);
        lPinkie = lWrist.transform.GetChild(4);

        rWrist = rightHand.transform.GetChild(0);
        rThumb = rWrist.transform.GetChild(0);
        rMiddle = rWrist.transform.GetChild(1);
        rRing = rWrist.transform.GetChild(2);
        rIndex = rWrist.transform.GetChild(3);
        rPinkie = rWrist.transform.GetChild(4);

        ws = client.GetComponent<WebSocketClient>();
    }
    private void FixedUpdate()
    {
        if (ws.GetHandData() != null)
        {
            float[] data = ws.GetHandData();
            if (data[1] == -1 && data[22] == -1)
            {
                leftHand.SetActive(false);
                rightHand.SetActive(false);
            }
            else
            {
                leftHand.SetActive(true);
                rightHand.SetActive(true);
                lWrist.position = pivot.TransformPoint(new Vector3(data[0], data[1], data[2]));
                rWrist.position = pivot.TransformPoint(new Vector3(data[63], data[64], data[65]));

                setBonePos(lThumb, data, 3);
                setBonePos(lIndex, data, 15);
                setBonePos(lMiddle, data, 27);
                setBonePos(lRing, data, 39);
                setBonePos(lPinkie, data, 51);

                setBonePos(rThumb, data, 66);
                setBonePos(rIndex, data, 78);
                setBonePos(rMiddle, data, 90);
                setBonePos(rRing, data, 102);
                setBonePos(rPinkie, data, 114);
            }
        }
    }
    void setBonePos(Transform bone, float[] data, int offset)
    {
        bone.position = pivot.TransformPoint(new Vector3(data[offset], data[1 + offset], data[2 + offset]));
        
        if (bone.childCount > 0){
            Transform childBone = bone.GetChild(0);
            setBonePos(childBone, data, offset+3);
        }
        return;
    }
}

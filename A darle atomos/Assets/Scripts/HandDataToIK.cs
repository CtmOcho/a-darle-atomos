using UnityEngine;
using NativeWebSocket;

public class HandDataToIK : MonoBehaviour
{
    public Transform handsPivot;
    public float angle;
    WebSocketClient wsc;
    WebSocket ws;

    [Header("Mano Derecha")]
    public GameObject rightHand;
    public Transform wristTargetR;
    public Transform thumbTargetR;
    public Transform indexTargetR;
    public Transform middleTargetR;
    public Transform ringTargetR;
    public Transform pinkieTargetR;

    public Transform thumbTargetAimR;
    public Transform indexTargetAimR;
    public Transform middleTargetAimR;
    public Transform ringTargetAimR;
    public Transform pinkieTargetAimR;
    Transform rightHandTr;

    [Header("Mano Izquerda")]
    public GameObject leftHand;
    public Transform wristTargetL;
    public Transform thumbTargetL;
    public Transform indexTargetL;
    public Transform middleTargetL;
    public Transform ringTargetL;
    public Transform pinkieTargetL;

    public Transform thumbTargetAimL;
    public Transform indexTargetAimL;
    public Transform middleTargetAimL;
    public Transform ringTargetAimL;
    public Transform pinkieTargetAimL;
    Transform leftHandTr;

    void Start()
    {
        rightHandTr = rightHand.transform;
        leftHandTr = leftHand.transform;

        wsc = gameObject.GetComponent<WebSocketClient>();
        ws = wsc.GetSocket();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float[] data = wsc.GetHandData();
        if (data[1] == -1 && data[22] == -1)
        {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
        }
        else
        {
            leftHand.SetActive(true);
            rightHand.SetActive(true);

            setTargetPosition(leftHandTr, handsPivot, new Vector3(data[0], data[1], data[2]));
            setTargetPositionAndRotation(wristTargetL, handsPivot, new Vector3(data[0], data[1], data[2]), new Vector3(data[27], data[28], data[29]), new Vector3(data[39], data[40], data[41]), angle);
            setTargetPosition(thumbTargetL, handsPivot, new Vector3(data[9], data[10], data[11]));
            setTargetPosition(indexTargetL, handsPivot, new Vector3(data[21], data[22], data[23]));
            setTargetPosition(middleTargetL, handsPivot, new Vector3(data[33], data[34], data[35]));
            setTargetPosition(ringTargetL, handsPivot, new Vector3(data[45], data[46], data[47]));
            setTargetPosition(pinkieTargetL, handsPivot, new Vector3(data[57], data[58], data[59]));

            setTargetPosition(thumbTargetAimL, handsPivot, new Vector3(data[12], data[13], data[14]));
            setTargetPosition(indexTargetAimL, handsPivot, new Vector3(data[24], data[25], data[26]));
            setTargetPosition(middleTargetAimL, handsPivot, new Vector3(data[36], data[37], data[38]));
            setTargetPosition(ringTargetAimL, handsPivot, new Vector3(data[48], data[49], data[50]));
            setTargetPosition(pinkieTargetAimL, handsPivot, new Vector3(data[60], data[61], data[62]));

            setTargetPosition(rightHandTr, handsPivot, new Vector3(data[63], data[64], data[65]));
            setTargetPositionAndRotation(wristTargetR, handsPivot, new Vector3(data[63], data[64], data[65]), new Vector3(data[78], data[79], data[80]), new Vector3(data[90], data[91], data[92]), angle);
            setTargetPosition(thumbTargetR, handsPivot, new Vector3(data[72], data[73], data[74]));
            setTargetPosition(indexTargetR, handsPivot, new Vector3(data[84], data[85], data[86]));
            setTargetPosition(middleTargetR, handsPivot, new Vector3(data[96], data[97], data[98]));
            setTargetPosition(ringTargetR, handsPivot, new Vector3(data[108], data[109], data[110]));
            setTargetPosition(pinkieTargetR, handsPivot, new Vector3(data[120], data[121], data[122]));
                
            setTargetPosition(thumbTargetAimR, handsPivot, new Vector3(data[75], data[76], data[77]));
            setTargetPosition(indexTargetAimR, handsPivot, new Vector3(data[87], data[88], data[89]));
            setTargetPosition(middleTargetAimR, handsPivot, new Vector3(data[99], data[100], data[101]));
            setTargetPosition(ringTargetAimR, handsPivot, new Vector3(data[111], data[112], data[113]));
            setTargetPosition(pinkieTargetAimR, handsPivot, new Vector3(data[123], data[124], data[125]));
        }
    }
    void setTargetPosition(Transform target, Transform pivot, Vector3 A)
    {
        target.position = pivot.TransformPoint(A);
    }
    void setTargetPositionAndRotation(Transform target, Transform pivot, Vector3 A, Vector3 B, Vector3 C, float angle)
    {
        target.position = pivot.TransformPoint(A);
        Vector3 direction = target.position - pivot.TransformPoint(B);
        Vector3 upward = -Vector3.Cross(direction, pivot.TransformPoint(C)).normalized;
        if (direction != Vector3.zero) {
            target.rotation = Quaternion.LookRotation(direction,upward);
        }
    }
}


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

    public Transform thumbHintAimL;
    public Transform indexHintAimL;
    public Transform middleHintAimL;
    public Transform ringHintAimL;
    public Transform pinkieHintAimL;
    Transform leftHandTr;
    float[] data;
    int offset0 = 0; // Offset de la primera mitad de datos
    int offset1 = 0; // Offset de la otra mitad de datos
    void Start()
    {
        rightHandTr = rightHand.transform;
        leftHandTr = leftHand.transform;

        wsc = gameObject.GetComponent<WebSocketClient>();
        ws = wsc.GetSocket();
        data = new float[127];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wsc.GetHandData() != null)
        {
            data = wsc.GetHandData();
            if ((int)data[126] == 1)
            {
                offset0 = 0;
                offset1 = 63;
            }
            else
            {
                offset0 = 63;
                offset1 = 0;
            }
            setHandTargetPosAndRot(leftHandTr, thumbTargetL, indexTargetL, middleTargetL, ringTargetL, pinkieTargetL, handsPivot, data, offset0);
            setHandTargetPosAndRot(rightHandTr, thumbTargetR, indexTargetR, middleTargetR, ringTargetR, pinkieTargetR, handsPivot, data, offset1);
            setFingersHintPos(leftHandTr,thumbHintAimL,indexHintAimL,middleHintAimL,ringHintAimL,pinkieHintAimL);
            setTargetPositionAndRotation(wristTargetL, handsPivot, new Vector3(data[0 + offset0], data[1 + offset0], data[2 + offset0]), new Vector3(data[27 + offset0], data[28 + offset0], data[29+ offset0]), new Vector3(data[15 + offset0], data[16 + offset0], data[17 + offset0]), new Vector3(data[51 + offset0], data[52 + offset0], data[53 + offset0]));
            setTargetPositionAndRotation(wristTargetR, handsPivot, new Vector3(data[0 + offset1], data[1 + offset1], data[2 + offset1]), new Vector3(data[27 + offset1], data[28 + offset1], data[29 + offset1]), new Vector3(data[51 + offset1], data[52 + offset1], data[53 + offset1]), new Vector3(data[15 + offset1], data[16 + offset1], data[17 + offset1]));
            setFingersAimPos(thumbTargetAimL, indexTargetAimL, middleTargetAimL, ringTargetAimL, pinkieTargetAimL, handsPivot, data, offset0);
            setFingersAimPos(thumbTargetAimR, indexTargetAimR, middleTargetAimR, ringTargetAimR, pinkieTargetAimR, handsPivot, data, offset1);
        }
    }
    void setTargetPosition(Transform target, Transform pivot, Vector3 A)
    {
        target.position = pivot.TransformPoint(A);
    }
    void setTargetPositionAndRotation(Transform target, Transform pivot, Vector3 O, Vector3 B, Vector3 C, Vector3 D)
    {
        target.position = pivot.TransformPoint(O);
        Vector3 BO = pivot.TransformPoint(O) - pivot.TransformPoint(B); //Direction vec
        Vector3 CO = pivot.TransformPoint(O) - pivot.TransformPoint(C);
        Vector3 DO = pivot.TransformPoint(O) - pivot.TransformPoint(D);
        if (BO != Vector3.zero && CO != Vector3.zero) {
            Vector3 upwardBO = -Vector3.Cross(CO, DO);
            target.rotation = Quaternion.LookRotation(BO, upwardBO);
        }
    }
    void setHandTargetPosAndRot(Transform wrist, Transform thumb, Transform index, Transform middle, Transform ring, Transform pinkie, Transform pivot, float[] data, int offset)
    {
        setTargetPosition(wrist, pivot, new Vector3(data[0 + offset], data[1 + offset], data[2 + offset]));
        setTargetPosition(thumb, pivot, new Vector3(data[9 + offset], data[10 + offset], data[11 + offset]));
        setTargetPosition(index, pivot, new Vector3(data[21 + offset], data[22 + offset], data[23 + offset]));
        setTargetPosition(middle, pivot, new Vector3(data[33 + offset], data[34 + offset], data[35 + offset]));
        setTargetPosition(ring, pivot, new Vector3(data[45 + offset], data[46 + offset], data[47 + offset]));
        setTargetPosition(pinkie, pivot, new Vector3(data[57 + offset], data[58 + offset], data[59 + offset]));
    }
    void setFingersAimPos(Transform thumb, Transform index, Transform middle, Transform ring, Transform pinkie, Transform pivot, float[] data, int offset)
    {
        setTargetPosition(thumb, pivot, new Vector3(data[12 + offset], data[13 + offset], data[14 + offset]));
        setTargetPosition(index, pivot, new Vector3(data[24 + offset], data[25 + offset], data[26 + offset]));
        setTargetPosition(middle, pivot, new Vector3(data[36 + offset], data[37 + offset], data[38 + offset]));
        setTargetPosition(ring, pivot, new Vector3(data[48 + offset], data[49 + offset], data[50 + offset]));
        setTargetPosition(pinkie, pivot, new Vector3(data[60 + offset], data[61 + offset], data[62 + offset]));
    }
    void setFingersHintPos(Transform hand,Transform thumb, Transform index, Transform middle, Transform ring, Transform pinkie)
    {
        thumb.position = hand.GetChild(1).transform.GetChild(0).transform.GetChild(0).position;
        index.position = hand.GetChild(1).transform.GetChild(1).transform.GetChild(0).position;
        middle.position = hand.GetChild(1).transform.GetChild(2).transform.GetChild(0).position;
        ring.position = hand.GetChild(1).transform.GetChild(3).transform.GetChild(0).position;
        pinkie.position = hand.GetChild(1).transform.GetChild(4).transform.GetChild(0).position;
    }
}


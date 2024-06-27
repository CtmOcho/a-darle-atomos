using UnityEngine;
using NativeWebSocket;
using UnityEngine.XR;

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

    Vector3[] vecArray;
    float[] data;
    bool fistData;
    int offset0 = 0; // Offset de la primera mitad de datos
    int offset1 = 0; // Offset de la otra mitad de datos
    void Start()
    {
        rightHandTr = rightHand.transform;
        leftHandTr = leftHand.transform;

        wsc = gameObject.GetComponent<WebSocketClient>();
        ws = wsc.GetSocket();
        data = new float[127];
        vecArray = new Vector3[42];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wsc.GetHandData() != null)
        {

            data = wsc.GetHandData();
            if (fistData)
            {

            }
            if ((int)data[126] == 1)
            {
                offset0 = 0;
                offset1 = 21;
            }
            else
            {
                offset0 = 21;
                offset1 = 0;
            }
            getDataVectors(data, vecArray);
            setHandTargetPosAndRot(leftHandTr, thumbTargetL, indexTargetL, middleTargetL, ringTargetL, pinkieTargetL, handsPivot, vecArray, offset0);
            setHandTargetPosAndRot(rightHandTr, thumbTargetR, indexTargetR, middleTargetR, ringTargetR, pinkieTargetR, handsPivot, vecArray, offset1);
            setFingersHintPos(leftHandTr,thumbHintAimL,indexHintAimL,middleHintAimL,ringHintAimL,pinkieHintAimL);
            setHandPositionAndRotation(wristTargetL, handsPivot, vecArray[0 + offset0], vecArray[9 + offset0], vecArray[5 + offset0], vecArray[17 + offset0]);
            setHandPositionAndRotation(wristTargetR, handsPivot, vecArray[0 + offset1], vecArray[9 + offset1], vecArray[17 + offset1], vecArray[5 + offset1]);
            setFingersAimPos(thumbTargetAimL, indexTargetAimL, middleTargetAimL, ringTargetAimL, pinkieTargetAimL, handsPivot, vecArray, offset0);
            setFingersAimPos(thumbTargetAimR, indexTargetAimR, middleTargetAimR, ringTargetAimR, pinkieTargetAimR, handsPivot, vecArray, offset1);
        }
    }
    void setTargetPosition(Transform target, Transform pivot, Vector3 O)
    {
        target.position = pivot.TransformPoint(O);
    }
    void setTargetPositionAndRotation(Transform target, Transform pivot, Vector3 O, Vector3 B, Vector3 C, Vector3 D, int offset)
    {
        target.position = pivot.TransformPoint(O);
        Vector3 BO = pivot.TransformPoint(B) - pivot.TransformPoint(O);
        Vector3 CD = pivot.TransformPoint(C) - pivot.TransformPoint(D);
        if (BO != Vector3.zero && CD != Vector3.zero)
        {
            Vector3 upwardBO = Vector3.Cross(CD, BO);
            target.rotation = Quaternion.LookRotation(BO, upwardBO);
        }
    }
    void setHandPositionAndRotation(Transform hand, Transform pivot, Vector3 O, Vector3 B, Vector3 C, Vector3 D)
    {
        hand.position = pivot.TransformPoint(O);
        Vector3 BO = pivot.TransformPoint(O) - pivot.TransformPoint(B); //Direction vector
        Vector3 CO = pivot.TransformPoint(O) - pivot.TransformPoint(C);
        Vector3 DO = pivot.TransformPoint(O) - pivot.TransformPoint(D);
        if (BO != Vector3.zero && CO != Vector3.zero) {
            Vector3 upwardBO = -Vector3.Cross(CO, DO);
            hand.rotation = Quaternion.LookRotation(BO, upwardBO);
        }
    }
    void setHandTargetPosAndRot(Transform wrist, Transform thumb, Transform index, Transform middle, Transform ring, Transform pinkie, Transform pivot, Vector3[] vecArr, int offset)
    {
        setTargetPosition(wrist, pivot, vecArray[0 + offset]);
        setTargetPositionAndRotation(thumb, pivot, vecArr[3 + offset], vecArr[4 + offset], vecArr[1 + offset], vecArr[5 + offset], offset);
        setTargetPositionAndRotation(index, pivot, vecArr[7 + offset], vecArr[8 + offset], vecArr[5 + offset], vecArr[9 + offset], offset);
        setTargetPositionAndRotation(middle,pivot, vecArr[11 + offset],vecArr[12 + offset],vecArr[5 + offset], vecArr[9 + offset], offset);
        setTargetPositionAndRotation(ring,  pivot, vecArr[15 + offset],vecArr[16 + offset], vecArr[9 + offset], vecArr[13 + offset],offset);
        setTargetPositionAndRotation(pinkie,pivot, vecArr[19 + offset],vecArr[20 + offset], vecArr[13 + offset], vecArr[17 + offset],offset);
    }
    void setFingersAimPos(Transform thumb, Transform index, Transform middle, Transform ring, Transform pinkie, Transform pivot, Vector3[] vecArr, int offset)
    {
        setTargetPosition(thumb, pivot, vecArr[4 + offset]);
        setTargetPosition(index, pivot, vecArr[8 + offset]);
        setTargetPosition(middle,pivot, vecArr[12 + offset]);
        setTargetPosition(ring,  pivot, vecArr[16 + offset]);
        setTargetPosition(pinkie,pivot, vecArr[20 + offset]);
    }
    void setFingersHintPos(Transform hand,Transform thumb, Transform index, Transform middle, Transform ring, Transform pinkie)
    {
        thumb.position = hand.GetChild(1).transform.GetChild(0).transform.GetChild(0).position;
        index.position = hand.GetChild(1).transform.GetChild(1).transform.GetChild(0).position;
        middle.position = hand.GetChild(1).transform.GetChild(2).transform.GetChild(0).position;
        ring.position = hand.GetChild(1).transform.GetChild(3).transform.GetChild(0).position;
        pinkie.position = hand.GetChild(1).transform.GetChild(4).transform.GetChild(0).position;
    }

    void getDataVectors(float[] data, Vector3[] vecArray)
    {
        int counter = 0;
        for(int i = 0; i < 126; i+=3) {
            vecArray[counter] = new Vector3(data[i], data[i+1], data[i+2]);
            counter++;
        }
    }
}
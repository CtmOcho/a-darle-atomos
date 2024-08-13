using UnityEngine;
using System;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.Rendering;
using System.Collections;

public class HandDataToIK : MonoBehaviour
{
    public float tolerance = 0.4f;
    float currTolerance;
    public float smoothTime = 0.3f;
    private Vector3 velocityL = Vector3.zero;
    private Vector3 velocityR = Vector3.zero;
    WebSocketClient wsc;

    Transform handsPivot;

    //Mano Izquerda
    Transform leftHand;
    Transform wristTargetL;
    Transform thumbTargetL;
    Transform indexTargetL;
    Transform middleTargetL;
    Transform ringTargetL;
    Transform pinkieTargetL;
    Transform thumbTargetAimL;
    Transform indexTargetAimL;
    Transform middleTargetAimL;
    Transform ringTargetAimL;
    Transform pinkieTargetAimL;

    Transform thumbHintAimL;
    Transform indexHintAimL;
    Transform middleHintAimL;
    Transform ringHintAimL;
    Transform pinkieHintAimL;

    //Mano derecha
    Transform rightHand;
    Transform wristTargetR;
    Transform thumbTargetR;
    Transform indexTargetR;
    Transform middleTargetR;
    Transform ringTargetR;
    Transform pinkieTargetR;
    Transform thumbTargetAimR;
    Transform indexTargetAimR;
    Transform middleTargetAimR;
    Transform ringTargetAimR;
    Transform pinkieTargetAimR;

    Transform thumbHintAimR;
    Transform indexHintAimR;
    Transform middleHintAimR;
    Transform ringHintAimR;
    Transform pinkieHintAimR;

    Vector3[] vecArray;
    Vector3[] prevVecArray;
    float[] data;
    float lWristDistanceMagnitude;
    float rWristDistanceMagnitude;
    bool firstCycleFlag = true;
    int offset0 = 0; // Offset de la primera mitad de datos
    int offset1 = 0; // Offset de la otra mitad de datos
    bool interpreterReady = true;

    void Start()
    {
        handsPivot = gameObject.transform.GetChild(0).transform;
        leftHand = gameObject.transform.GetChild(1).transform;
        rightHand = gameObject.transform.GetChild(2).transform;

        wristTargetL = leftHand.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform;
        thumbTargetL = leftHand.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform;
        indexTargetL = leftHand.GetChild(2).transform.GetChild(1).transform.GetChild(0).transform;
        middleTargetL = leftHand.GetChild(2).transform.GetChild(2).transform.GetChild(0).transform;
        ringTargetL = leftHand.GetChild(2).transform.GetChild(3).transform.GetChild(0).transform;
        pinkieTargetL = leftHand.GetChild(2).transform.GetChild(4).transform.GetChild(0).transform;

        thumbHintAimL = leftHand.GetChild(2).transform.GetChild(0).transform.GetChild(1).transform;
        indexHintAimL = leftHand.GetChild(2).transform.GetChild(1).transform.GetChild(1).transform;
        middleHintAimL = leftHand.GetChild(2).transform.GetChild(2).transform.GetChild(1).transform;
        ringHintAimL = leftHand.GetChild(2).transform.GetChild(3).transform.GetChild(1).transform;
        pinkieHintAimL = leftHand.GetChild(2).transform.GetChild(4).transform.GetChild(1).transform;

        thumbTargetAimL = leftHand.GetChild(4).transform.GetChild(0).transform.GetChild(0).transform;
        indexTargetAimL = leftHand.GetChild(4).transform.GetChild(1).transform.GetChild(0).transform;
        middleTargetAimL = leftHand.GetChild(4).transform.GetChild(2).transform.GetChild(0).transform;
        ringTargetAimL = leftHand.GetChild(4).transform.GetChild(3).transform.GetChild(0).transform;
        pinkieTargetAimL = leftHand.GetChild(4).transform.GetChild(4).transform.GetChild(0).transform;

        wristTargetR = rightHand.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform;
        thumbTargetR = rightHand.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform;
        indexTargetR = rightHand.GetChild(2).transform.GetChild(1).transform.GetChild(0).transform;
        middleTargetR = rightHand.GetChild(2).transform.GetChild(2).transform.GetChild(0).transform;
        ringTargetR = rightHand.GetChild(2).transform.GetChild(3).transform.GetChild(0).transform;
        pinkieTargetR = rightHand.GetChild(2).transform.GetChild(4).transform.GetChild(0).transform;
        thumbTargetAimR = rightHand.GetChild(4).transform.GetChild(0).transform.GetChild(0).transform;
        indexTargetAimR = rightHand.GetChild(4).transform.GetChild(1).transform.GetChild(0).transform;
        middleTargetAimR = rightHand.GetChild(4).transform.GetChild(2).transform.GetChild(0).transform;
        ringTargetAimR = rightHand.GetChild(4).transform.GetChild(3).transform.GetChild(0).transform;
        pinkieTargetAimR = rightHand.GetChild(4).transform.GetChild(4).transform.GetChild(0).transform;

        thumbHintAimR = rightHand.GetChild(2).transform.GetChild(0).transform.GetChild(1).transform;
        indexHintAimR = rightHand.GetChild(2).transform.GetChild(1).transform.GetChild(1).transform;
        middleHintAimR = rightHand.GetChild(2).transform.GetChild(2).transform.GetChild(1).transform;
        ringHintAimR = rightHand.GetChild(2).transform.GetChild(3).transform.GetChild(1).transform;
        pinkieHintAimR = rightHand.GetChild(2).transform.GetChild(4).transform.GetChild(1).transform;

        wsc = gameObject.GetComponent<WebSocketClient>();
        data = new float[127];
        setVectorsZero(ref prevVecArray);
        setVectorsZero(ref vecArray);
        currTolerance = 100;
    }

    private void Update()
    {
        Vector3 newPosL = prevVecArray[0 + offset0], newPosR = prevVecArray[0 + offset1];
        if (!interpreterReady)
        {
            if (offset0 + offset1 != 0 && lWristDistanceMagnitude < tolerance && rWristDistanceMagnitude < tolerance)
            {
                //newPosL = Vector3.SmoothDamp(prevVecArray[0 + offset0], vecArray[0 + offset0], ref velocityL, smoothTime);
                newPosL = Vector3.Slerp(prevVecArray[0 + offset0], vecArray[0 + offset0], smoothTime);
                //newPosR = Vector3.SmoothDamp(prevVecArray[0 + offset1], vecArray[0 + offset1], ref velocityR, smoothTime);
                newPosR = Vector3.Slerp(prevVecArray[0 + offset1], vecArray[0 + offset1], smoothTime);
            }
        }
        setTargetPosition(leftHand, handsPivot, newPosL);
        setTargetPosition(rightHand, handsPivot, newPosR);
        setHandPositionAndRotation(wristTargetL, handsPivot, newPosL, prevVecArray[9 + offset0], prevVecArray[5 + offset0], prevVecArray[17 + offset0]);
        setHandPositionAndRotation(wristTargetR, handsPivot, newPosR, prevVecArray[9 + offset1], prevVecArray[17 + offset1], prevVecArray[5 + offset1]);

    }
    void FixedUpdate()
    {
        if(interpreterReady)
        {
            StartCoroutine(dataInterpreter(0.035f));
        }
    }

    IEnumerator dataInterpreter(float dataDelay)
    {
        interpreterReady = false;
        if (wsc.GetHandData() != null)
        {
            data = wsc.GetHandData();
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
            if (firstCycleFlag)
            {
                getDataVectors(data,ref prevVecArray);
                setHandTargetPosAndRot(leftHand, thumbTargetL, indexTargetL, middleTargetL, ringTargetL, pinkieTargetL, handsPivot, prevVecArray, offset0);
                setHandTargetPosAndRot(rightHand, thumbTargetR, indexTargetR, middleTargetR, ringTargetR, pinkieTargetR, handsPivot, prevVecArray, offset1);
                setFingersHintPos(leftHand, thumbHintAimL, indexHintAimL, middleHintAimL, ringHintAimL, pinkieHintAimL);
                setFingersHintPos(rightHand, thumbHintAimR, indexHintAimR, middleHintAimR, ringHintAimR, pinkieHintAimR);

                setFingersAimPos(thumbTargetAimL, indexTargetAimL, middleTargetAimL, ringTargetAimL, pinkieTargetAimL, handsPivot, prevVecArray, offset0);
                setFingersAimPos(thumbTargetAimR, indexTargetAimR, middleTargetAimR, ringTargetAimR, pinkieTargetAimR, handsPivot, prevVecArray, offset1);
                firstCycleFlag = false;
            }
            else
            {
                getDataVectors(data, ref vecArray);
                lWristDistanceMagnitude = (vecArray[0 + offset0] - prevVecArray[0 + offset0]).magnitude;
                rWristDistanceMagnitude = (vecArray[0 + offset1] - prevVecArray[0 + offset1]).magnitude;
                if (lWristDistanceMagnitude > 0 && rWristDistanceMagnitude > 0)
                {
                    //if (lWristDistanceMagnitude < tolerance && rWristDistanceMagnitude < tolerance)
                    //{
                        Debug.Log((vecArray[0 + offset0] - prevVecArray[0 + offset0]).magnitude.ToString("F8"));
                        setHandTargetPosAndRot(leftHand, thumbTargetL, indexTargetL, middleTargetL, ringTargetL, pinkieTargetL, handsPivot, prevVecArray, offset0);
                        setHandTargetPosAndRot(rightHand, thumbTargetR, indexTargetR, middleTargetR, ringTargetR, pinkieTargetR, handsPivot, prevVecArray, offset1);
                        setFingersHintPos(leftHand, thumbHintAimL, indexHintAimL, middleHintAimL, ringHintAimL, pinkieHintAimL);
                        setFingersHintPos(rightHand, thumbHintAimR, indexHintAimR, middleHintAimR, ringHintAimR, pinkieHintAimR);
                        setFingersAimPos(thumbTargetAimL, indexTargetAimL, middleTargetAimL, ringTargetAimL, pinkieTargetAimL, handsPivot, prevVecArray, offset0);
                        setFingersAimPos(thumbTargetAimR, indexTargetAimR, middleTargetAimR, ringTargetAimR, pinkieTargetAimR, handsPivot, prevVecArray, offset1);
                    //}
                }
                getDataVectors(data, ref prevVecArray);
            }
        }
        yield return new WaitForSeconds(dataDelay);
        interpreterReady = true;
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
        //setTargetPosition(wrist, pivot, vecArray[0 + offset]);
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

    void getDataVectors(float[] data, ref Vector3[] vecArray)
    {
        int counter = 0;
        for(int i = 0; i < 126; i+=3) {
            vecArray[counter] = new Vector3(Mathf.Round(data[i]*100.0f)*0.01f, Mathf.Round(data[i+1] * 100.0f) * 0.01f, Mathf.Round(data[i+2] * 100.0f) * 0.01f);
            counter++;
        }
    }
    void setVectorsZero(ref Vector3[] arr)
    {
        arr = new Vector3[42];
        for (int i = 0; i < 42; i += 1)
        {
            arr[i] = Vector3.zero;
        }
    }
}
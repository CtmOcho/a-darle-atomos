import cv2 as cv
import mediapipe as mp
import numpy as np
import sys
from utils import DLT, get_projection_matrix, write_keypoints_to_disk
import asyncio
import websockets
import json

async def send_data(uri, data):
    try:
        async with websockets.connect(uri) as websocket:
            wrapped_data = data
            await websocket.send(json.dumps(wrapped_data))
            
            print("Data sent to: ", uri)
            print("Data:", data[-1])
    except Exception as e:
        print(f"Failed to send data to {uri}: {e}")

ws_uri = "ws://localhost:8765"

mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands

frame_shape = [720, 1280]

def run_mp(input_stream1, input_stream2, P0, P1):
    #input video stream
    cap0 = cv.VideoCapture(input_stream1)
    cap1 = cv.VideoCapture(input_stream2)
    caps = [cap0, cap1]

    #set camera resolution if using webcam to 1280x720. Any bigger will cause some lag for hand detection
    for cap in caps:
        cap.set(3, frame_shape[1])
        cap.set(4, frame_shape[0])

    #create hand keypoints detector object.
    hands0 = mp_hands.Hands(min_detection_confidence=0.5, max_num_hands=2, min_tracking_confidence=0.5)
    hands1 = mp_hands.Hands(min_detection_confidence=0.5, max_num_hands=2, min_tracking_confidence=0.5)

    #containers for detected keypoints for each camera
    kpts_cam0 = []
    kpts_cam1 = []
    kpts_3d = []
    
    while True:
        #read frames from stream
        ret0, frame0 = cap0.read()
        ret1, frame1 = cap1.read()

        if not ret0 or not ret1: break

        #crop to 720x720.
        #Note: camera calibration parameters are set to this resolution. If you change this, make sure to also change camera intrinsic parameters
        #if frame0.shape[1] != 720:
        #    frame0 = frame0[:, frame_shape[1] // 2 - frame_shape[0] // 2:frame_shape[1] // 2 + frame_shape[0] // 2]
        #    frame1 = frame1[:, frame_shape[1] // 2 - frame_shape[0] // 2:frame_shape[1] // 2 + frame_shape[0] // 2]

        # the BGR image to RGB.
        frame0 = cv.cvtColor(frame0, cv.COLOR_BGR2RGB)
        frame1 = cv.cvtColor(frame1, cv.COLOR_BGR2RGB)

        # To improve performance, optionally mark the image as not writeable to pass by reference.
        frame0.flags.writeable = False
        frame1.flags.writeable = False
        results0 = hands0.process(frame0)
        results1 = hands1.process(frame1)

        #prepare list of hand keypoints of this frame
        frame0_keypoints = []
        if results0.multi_hand_landmarks:
            for hand_landmarks in results0.multi_hand_landmarks:
                for p in range(21):
                    pxl_x = int(round(frame0.shape[1] * hand_landmarks.landmark[p].x))
                    pxl_y = int(round(frame0.shape[0] * hand_landmarks.landmark[p].y))
                    kpts = [pxl_x, pxl_y]
                    frame0_keypoints.append(kpts)
        
        if len(frame0_keypoints) < 42:
            frame0_keypoints.extend([[-1, -1]] * (42 - len(frame0_keypoints)))

        kpts_cam0.append(frame0_keypoints)

        frame1_keypoints = []
        if results1.multi_hand_landmarks:
            for hand_landmarks in results1.multi_hand_landmarks:
                for p in range(21):
                    pxl_x = int(round(frame1.shape[1] * hand_landmarks.landmark[p].x))
                    pxl_y = int(round(frame1.shape[0] * hand_landmarks.landmark[p].y))
                    kpts = [pxl_x, pxl_y]
                    frame1_keypoints.append(kpts)

        if len(frame1_keypoints) < 42:
            frame1_keypoints.extend([[-1, -1]] * (42 - len(frame1_keypoints)))

        kpts_cam1.append(frame1_keypoints)

        #calculate 3d position
        frame_p3ds = []
        for uv1, uv2 in zip(frame0_keypoints, frame1_keypoints):
            if uv1[0] == -1 or uv2[0] == -1:
                _p3d = [-1, -1, -1]
            else:
                _p3d = DLT(P0, P1, uv1, uv2) #calculate 3d position of keypoint
            frame_p3ds.append(_p3d)

        '''
        This contains the 3d position of each keypoint in current frame.
        For real time application, this is what you want.
        '''
        if frame_p3ds.size == 42 * 3:  # Verificamos si tiene el tamaño correcto para hacer reshape
            frame_p3ds = np.array(frame_p3ds).reshape((42, 3))
        else:
            print("No se puede hacer reshape, continuando...")
            continue
        #kpts_3d.append(frame_p3ds)
        #print(frame_p3ds)
        if -1 not in frame_p3ds:
            if results0.multi_hand_landmarks:
                for idx, hand_handedness in enumerate(results0.multi_handedness):
                    results0_handedness = hand_handedness.classification[0].label
                    if results0_handedness == "Right": #Al estar mirrored image, están invertidas, por lo que si es right es 0 pero se interpreta como 1. y viceversa DE LA PRIMERA MANO QUE APARECE
                        results0_handedness = 0
                    else:
                        results0_handedness = 1
            data = frame_p3ds.flatten().tolist()
            data.append(results0_handedness)
            asyncio.get_event_loop().run_until_complete(send_data(ws_uri,data))

        # Draw the hand annotations on the image.
        frame0.flags.writeable = True
        frame1.flags.writeable = True
        frame0 = cv.cvtColor(frame0, cv.COLOR_RGB2BGR)
        frame1 = cv.cvtColor(frame1, cv.COLOR_RGB2BGR)

        if results0.multi_hand_landmarks:
            for hand_landmarks in results0.multi_hand_landmarks:
                mp_drawing.draw_landmarks(frame0, hand_landmarks, mp_hands.HAND_CONNECTIONS)

        if results1.multi_hand_landmarks:
            for hand_landmarks in results1.multi_hand_landmarks:
                mp_drawing.draw_landmarks(frame1, hand_landmarks, mp_hands.HAND_CONNECTIONS)
        cv.imshow('cam1', frame1)
        cv.imshow('cam0', frame0)

        k = cv.waitKey(1)
        if k & 0xFF == 27: break #27 is ESC key.

    cv.destroyAllWindows()
    for cap in caps:
        cap.release()

    return np.array(kpts_cam0), np.array(kpts_cam1), np.array(kpts_3d)

if __name__ == '__main__':
    input_stream1 = 0  # Cámara 1
    input_stream2 = 2  # Cámara 2

    if len(sys.argv) == 3:
        input_stream1 = int(sys.argv[1])
        input_stream2 = int(sys.argv[2])

    #projection matrices
    P0 = get_projection_matrix(0)
    P1 = get_projection_matrix(1)

    kpts_cam0, kpts_cam1, kpts_3d = run_mp(input_stream1, input_stream2, P0, P1)

    #this will create keypoints file in current working folder
    # write_keypoints_to_disk('kpts_cam0.dat', kpts_cam0)
    # write_keypoints_to_disk('kpts_cam1.dat', kpts_cam1)
    # write_keypoints_to_disk('kpts_3d.dat', kpts_3d)

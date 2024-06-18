using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NativeWebSocket;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket websocket;
    IntArrayWrapper arrayMessage;

    async void Start()
    {
        websocket = new WebSocket("ws://localhost:8765");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            StartRequestLoop();
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var message = Encoding.UTF8.GetString(bytes);
            //Debug.Log("Received message: " + message);

            // Deserialize the JSON message to an IntArrayWrapper
            arrayMessage = JsonUtility.FromJson<IntArrayWrapper>(message);
            //Debug.Log("Deserialized array: " + string.Join(", ", arrayMessage.array));
        };

        try
        {
            // Connect to the server
            await websocket.Connect();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    private async void StartRequestLoop()
    {
        while (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("request");
            await Task.Delay(100);  // Wait for 1 second before sending the next request
        }
    }
    public WebSocket GetSocket()
    {
        return websocket;
    }

    public float[] GetHandData()
    {
        return arrayMessage.array;
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}

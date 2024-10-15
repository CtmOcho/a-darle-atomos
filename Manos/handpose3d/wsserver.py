import asyncio
import websockets
import json


connected_clients = set()
last_message = None

async def handler(websocket, path):
    global last_message
    connected_clients.add(websocket)
    try:
        async for message in websocket:
            if message != "request":
                last_message = {"array": json.loads(message)}  # Ensure it's a dictionary with "array"
                print("Received message from handpose3d.py")
            else:
                if last_message is not None:
                    await websocket.send(json.dumps(last_message))
                    print(f"Sent last message: {last_message}")
    except websockets.exceptions.ConnectionClosed as e:
        print(f"Connection closed: {e}")
    finally:
        connected_clients.remove(websocket)


async def main():
    async with websockets.serve(handler, "localhost", 8765):
        print("WebSocket server is ready on ws://localhost:8765")  
        await asyncio.Future()  # run forever

if __name__ == "__main__":
    asyncio.run(main())

# A darle átomos
Un proyecto para la _Feria de Software 2024_ creado por _UltimateSpiderMonke (USM)_ pre-empresa de desarrollo informático compuesto por el mejor equipo de ingenieros en informática de la generación 2019 😎👌.  

Este proyecto integra el reconocimiento de manos, la calibración de cámaras, un servidor backend, un frontend en React y un proyecto en Unity para ofrecer una experiencia completa. A continuación, se detallan los pasos para la configuración y ejecución de cada componente del proyecto.

## 1. Reconocimiento de Manos

### 1.1 Requerimientos

- Mediapipe
- Python 3.8
- OpenCV
- Matplotlib

### 1.2 Calibración de Cámaras

Para realizar la calibración de las cámaras, navega a la carpeta `Manos/handpose3d/calib`.

**Proceso de Calibración:**
1. Se te preguntará por consola si deseas recalibrar las cámaras de manera individual.
   - Ingresa `""` (sin comillas) para hacer la calibración individual o intrínseca.
2. Sigue el proceso descrito en el `README.md` de la carpeta `calib` para calibrar ambas cámaras.
   - Selecciona los frames que reconozcan mejor los puntos de referencia.
   - Se recomienda apuntar a un RMSE < 0.5.

### 1.3 Ejecución

Para compilar y ejecutar el reconocimiento de manos:

1. Ejecuta el servidor WebSocket:
   ```bash
   python wsserver.py
2. En otra consola, ejecuta el siguiente comando:
   ```bash
   python handpose3d.py cam_0 cam_1
- Donde cam_0 y cam_1 corresponden a las cámaras calibradas previamente.
## 2 Backend
### 2.1 Ejecución del Servidor
1. Navega a la carpeta `Backend/`.
2. Ejecuta el servidor backend localmente:
    ```bash
    npm run production
### 2.2 Configuración de Ngrok
1. Si no tienes `ngrok.exe` descárgalo [aquí](https://dashboard.ngrok.com/get-started/setup/windows).
2. Ejecuta `ngrok.exe`.
3. Ingresa el token de autorización:
    ```bash
    ngrok config add-authtoken $_TOKEN

- El token debe solicitarse al encargado de la cuenta para tunneling [Octavio V.](github.com/CtmOcho/).
### 2.3 Configuración de Túneles
1. En la consola de `ngrok.exe` edita la configuración:
    ```bash
    ngrok config edit
2. Agrega la siguiente configuración de túneles al archivo `.yaml`
    ```yaml
        tunnels:
    react-app:
        addr: 3000
        proto: http
    
    node-backend:
        addr: 13756
        proto: http
### 2.4 Inicio de los túneles
1. En la consola `ngrok.exe` inicia los túneles:
    ```bash 
    ngrok start node-backend react-app
2. Copia la URL generada para el Backend (`localhost:13756`) para actualizarla en el Frontend ## y en Unity.
## 3 Frontend
### 3.1 Configuración
1. Navega a la carpeta `frontend/atomos`.
2. Actualiza el valor de `BackendUrl` en el archivo `src/config/config.js` con la URL copiada desde` la termina`l `Ngrok para` `el Backend.`
### 3.2 Compilación
1. Buildea el proyecto con el siguiente comando:
    ```bash 
    npm run build
### 3.3 Servicio del Proyecto
1. Asegurate de tener instalado `serve`, sino ejecuta en la terminal:
    ```bash
    npm install -g serve
2. Sirve el proyecto:
    ```bash
    serve -s build 3000
- Ahora podrás acceder al frontend desde la URL generada por Ngrok para `localhost:3000/ `

# 4 Unity
## 4.1 Configuración
1. Navega a la carpeta `A darle átomos/`.
2. Actualiza el valor de la variable `baseBackendUrl` en el archivo `Assets/Scripts/Login.cs` con la URL generada por Ngrok para el backend (``localhost:13756``).
## 4.2 Ejecución
1. Ejecuta el proyecto en modo juego desde la escena Inicio.
- Esta es una alternativa temporal a la compilación de una versión del proyecto en Unity.

# Futuro
En el futuro, se planea que `wsserver.py`, `calib.py` y `handpose3d.py` sean ejecutables con rutas relativas, permitiendo su uso como aplicativos sin necesidad de compilación en Python.




# a-darle-atomos
GitHub proyecto FSW-2024 Grupo Ultimate Spider Monke

### Reconocimiento de manos
Es necesario tener 2 cámaras en el computador donde se ejecute el algoritmo _handpose3d.py_ en caso de no tener una como tal instalen DroidCamClient en su pc y en su teléfono, básicamente usa una conexión entre la ip de su celu y su pc para usar la cámara del celu en el pc.
### De momento no hemos implementado la calibración de las cámaras
Para recibir la data de las manos en tiempo real a la simulación en unity, se debe ejecutar el archivo _wsserver.py_ luego el _handpose3d.py_ y finalmente la simulación en unity, la visualización en las pantallas del código se ven medio lentas, pero la manda de data está muy optimizada, se manda cada frame y en el archivo _WebServerSocket.cs_ en la carpeta _Scripts_ se tiene un timer de 100 ms para consultar por los datos de los frames, se puede bajar para que las consultas sean en menor tiempo entre unity y el webserver.
El array que se manda corresponde a un _flatten()_ de la matriz de tamaño (42 x 3), por lo que es un array de 126 elementos, para rearmar la matriz se debería iterar hasta 42 e iterar hasta 3 para crear la matriz de coordenadas de los 21 nodos creados por [mediapipe ](https://chuoling.github.io/mediapipe/solutions/hands.html).

# a-darle-atomos
GitHub proyecto FSW-2024 Grupo Ultimate Spider Monke

### Reconocimiento de manos
Es necesario tener 2 cámaras en el computador donde se ejecute el algoritmo _handpose3d.py_ en caso de no tener una como tal instalen DroidCamClient en su pc y en su teléfono, básicamente usa una conexión entre la ip de su celu y su pc para usar la cámara del celu en el pc.
Actualizacion: Droidcam te utiliza para siempre el slot 1, en caso de tener las 2 camaras, cambiar 2 y 3 los id en handpose3d.py.
## PARA LA CALIBRACIÓN:
### Se debe ejecutar el archivo _calib.py_ en la carpeta calib que está dentro de la carpeta handpose3d en Manos, se debe ingresar por consola un espacio en blanco y enter para recalibrar de manera intrínseca las cámaras, los valores que están por defecto son para la cámara 0 y 1 dentro de _calibration_settings.yaml_ si no le reconoce las cámaras deben cambiar estos números para las correctas. Luego se hará la calibración estereo, donde se tomaran captura de los frames de ambas cámaras y debe verse el _checkerboard_ en ambas cámaras.
### Para ambas calibraciones, la intrínseca y la externa, se aceptan los frames con "ESPACIO" y se ignoran con "S" se termina el programa con "ESC" luego para la calibración de coordenadas del WORLD se mostrarán las capturas de la calibración externa y se debe seleccionar el par de frames que mejor representen el "origen" para ambas cámaras y donde estará el lugar de trabajo final. 
# SE DEBE ESPERAR UNA CALIBRACIÓN CON RMSE < 5 PARA QUE SEA ACEPTABLE, PERO LO IDEAL ES LLEVAR EL VALOR DE RMSE < 0.5 o RMSE < 0.3 , LA CALIBRACIÓN MODIFICARÁ LOS ARCHIVOS EN LA CARPETA DE HANDPOSE3D PARA LA TOMA DE COORDENADAS 3D

  




Para recibir la data de las manos en tiempo real a la simulación en unity, se debe ejecutar el archivo _wsserver.py_ luego el _handpose3d.py_ y finalmente la simulación en unity, la visualización en las pantallas del código se ven medio lentas, pero la manda de data está muy optimizada, se manda cada frame y en el archivo _WebServerSocket.cs_ en la carpeta _Scripts_ se tiene un timer de 100 ms para consultar por los datos de los frames, se puede bajar para que las consultas sean en menor tiempo entre unity y el webserver.
El array que se manda corresponde a un _flatten()_ de la matriz de tamaño (42 x 3), por lo que es un array de 126 elementos, para rearmar la matriz se debería iterar hasta 42 e iterar hasta 3 para crear la matriz de coordenadas de los 21 nodos creados por [mediapipe ](https://chuoling.github.io/mediapipe/solutions/hands.html).


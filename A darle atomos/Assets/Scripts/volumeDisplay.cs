using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importar TextMeshProUGUI y TMP_Text

public class volumeDisplay : MonoBehaviour
{
    public GameObject lcdPresionObject;     // GameObject que contiene el TMP_Text para la presión
    public GameObject lcdTemperaturaObject; // GameObject que contiene el TMP_Text para la temperatura
    public TMP_Text displayVolumen;         // TMP_Text para mostrar la información de volumen, presión y temperatura

    private TMP_Text lcdPresion;     // TMP_Text para mostrar la presión
    private TMP_Text lcdTemperatura; // TMP_Text para mostrar la temperatura
    private ParticleBehaviour particleBehaviour; // Para almacenar la referencia al script ParticleBehaviour

    void Start()
    {
        // Buscar el componente TMP_Text en los GameObjects
        lcdPresion = lcdPresionObject.GetComponent<TMP_Text>();
        lcdTemperatura = lcdTemperaturaObject.GetComponent<TMP_Text>();

        if (lcdPresion == null || lcdTemperatura == null)
        {
            Debug.LogError("No se encontró el componente TMP_Text en los GameObjects.");
        }

        // Buscar el script ParticleBehaviour en la escena
        particleBehaviour = FindObjectOfType<ParticleBehaviour>();

        if (particleBehaviour == null)
        {
            Debug.LogError("No se encontró el script ParticleBehaviour en la escena.");
        }

        if (displayVolumen == null)
        {
            Debug.LogError("El texto de TMP para volumen no ha sido asignado correctamente.");
        }
    }

    void Update()
    {
        if (particleBehaviour != null && lcdPresion != null && lcdTemperatura != null)
        {
            // Obtener el tamaño del array de Rigidbody en ParticleBehaviour
            int volumen = particleBehaviour.rb.Count; // Asumiendo que rb es la lista de Rigidbody

            // Obtener los valores de presión y temperatura desde los textos de TMP
            string presion = lcdPresion.text;
            string temperatura = lcdTemperatura.text;

            // Actualizar el texto del display con la información
            displayVolumen.text = $"Volumen: {volumen} cc\nPresión: {presion} KPa\nTemperatura: {temperatura} ºC";
        }
    }
}

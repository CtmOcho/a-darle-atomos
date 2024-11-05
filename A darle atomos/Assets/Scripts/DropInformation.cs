using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInformation : MonoBehaviour
{
    public float liquidPH; // Almacena el pH del líquido
    public bool hasPHDetector; // Indica si tiene fenolftaleína
    public bool isPHExp; // Indica si es un experimento PH
    public float dropAmmount; // Cantidad de líquido representada por esta gota
    public bool isRainExp;
    public float temp;
    public string elementData;
    public bool isChameleonExp;
    public Color liquidColor;
    public bool isElephantExp;
    public bool isSoap;
    public bool isDust;
    public bool isColorant;
}

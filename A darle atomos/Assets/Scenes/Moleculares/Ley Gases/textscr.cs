using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffectTMP : MonoBehaviour
{
    public TMP_Text uiText;
    public string fullText;
    public float delay = 0.05f;

    private string currentText = "";

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {

            uiText.text =fullText;
            yield return new WaitForSeconds(delay);
        
    }
}

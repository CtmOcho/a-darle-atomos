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
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            uiText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}

using System.Collections;
using UnityEngine;

public class FoamReaction : MonoBehaviour
{
    public bool startReaction;
    [Header("Parametros reaccion")]
    public Color foamColor;
    public Material foamMaterial;
    
    [Header("Objetos de la reaccion")]
    public GameObject liquid;
    public GameObject foam;

    bool animInProgress = false;
    private void Start()
    {
        foam.SetActive(false);
    }
    void Update()
    {
        if(startReaction && !animInProgress)
        {
            foamMaterial.color = foamColor;
            Material[] temp = liquid.GetComponent<Renderer>().materials;
            temp[0] = foamMaterial;
            liquid.GetComponent<Renderer>().materials = temp;
            StartCoroutine(reactionAnim());
            animInProgress = true;
        }
    }
    IEnumerator reactionAnim()
    {
        while (liquid.transform.localScale.z < 1.05f)
        {
            liquid.transform.localScale = liquid.transform.localScale + new Vector3(0, 0, 0.01f);
            yield return new WaitForSeconds(0.02f);
        }
        foam.SetActive(true);
        yield return null;
    }
}

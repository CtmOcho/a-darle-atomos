using UnityEngine;

public class DustTrigger : MonoBehaviour
{
    public string chemicalName;
    public float ph;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spoon"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            other.gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(0.25f, 0.35f, 0.25f);
            other.gameObject.GetComponent<SpoonDustProperties>().chemicalName = chemicalName;
            other.gameObject.GetComponent<SpoonDustProperties>().ph = ph;
        }
    }
}

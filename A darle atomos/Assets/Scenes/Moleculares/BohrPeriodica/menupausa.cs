using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject objectToToggle;

    public void ToggleObject()
    {
        bool isActive = objectToToggle.activeSelf;
        objectToToggle.SetActive(!isActive);
    }
}

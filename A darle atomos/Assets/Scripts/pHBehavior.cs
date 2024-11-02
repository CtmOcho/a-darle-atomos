using UnityEngine;

public class pHBehavior : MonoBehaviour
{
    public float currentpH = 7f;
    private Rigidbody[] particleRigidbodies;
    private Vector3[] initialLocalPositions;

    public Transform hydrogen1; // Primer átomo de hidrógeno
    public Transform hydrogen2; // Segundo átomo de hidrógeno
    public Transform oxygen;    // Átomo de oxígeno

    public GameObject hydrogenPrefab; // Prefab del átomo de hidrógeno adicional

    public float upwardSpeed = 0.1f; // Velocidad de subida
    public float returnSpeed = 0.1f; // Velocidad de retorno
    public float maxYPosition = 5.0f; // Límite máximo de posición en el eje y al subir

    private Vector3 hydrogen1Velocity;
    private Vector3 hydrogen2Velocity;
    private bool isHydrogen1Selected;
    private bool hydrogenSelected = false;

    private GameObject additionalHydrogen;
    private bool isAdditionalHydrogenInstantiated = false;

    private Transform deactivatedHydrogen;
    private bool isHydrogenDeactivated = false;

    void Start()
    {
        particleRigidbodies = GetComponentsInChildren<Rigidbody>();
        int childCount = transform.childCount;
        initialLocalPositions = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            initialLocalPositions[i] = child.position;
            Debug.Log(initialLocalPositions[i]);
        }
    }

    void Update()
    {
        CheckDisassociation();
        HandleAdditionalHydrogen();
        HandleHydrogenDeactivation();
    }

    public void AdjustpH(float newpH)
    {
        currentpH = newpH;
        
        // Resetear selección al cambiar el pH
        if (currentpH <= 7f)
        {
            hydrogenSelected = false;
            hydrogen1Velocity = Vector3.zero;
            hydrogen2Velocity = Vector3.zero;
        }
    }

    void CheckDisassociation()
    {
        if (currentpH < 7f)
        {
            if (hydrogen1.localPosition.y < maxYPosition)
            {
                hydrogen1Velocity = Vector3.up * upwardSpeed;
            }
            else
            {
                hydrogen1Velocity = Vector3.zero;
            }

            if (hydrogen2.localPosition.y < maxYPosition)
            {
                hydrogen2Velocity = Vector3.up * upwardSpeed;
            }
            else
            {
                hydrogen2Velocity = Vector3.zero;
            }
        }
        else if (currentpH > 7f)
        {
            if (!hydrogenSelected)
            {
                isHydrogen1Selected = Random.value > 0.5f;
                hydrogenSelected = true;
            }

            if (isHydrogen1Selected)
            {
                if (hydrogen1.localPosition.y > 0)
                {
                    hydrogen1Velocity = Vector3.down * returnSpeed;
                }
                else
                {
                    hydrogen1Velocity = Vector3.zero;
                }
                hydrogen2Velocity = Vector3.zero;
            }
            else
            {
                if (hydrogen2.localPosition.y > 0)
                {
                    hydrogen2Velocity = Vector3.down * returnSpeed;
                }
                else
                {
                    hydrogen2Velocity = Vector3.zero;
                }
                hydrogen1Velocity = Vector3.zero;
            }
        }
        else if (currentpH == 7f)
        {
            hydrogen1Velocity = Vector3.zero;
            hydrogen2Velocity = Vector3.zero;
        }

        hydrogen1.localPosition += hydrogen1Velocity * Time.deltaTime;
        hydrogen2.localPosition += hydrogen2Velocity * Time.deltaTime;

        // Si existe un hidrógeno adicional, hereda la velocidad del hidrógeno al que fue instanciado cerca
        if (isAdditionalHydrogenInstantiated && additionalHydrogen != null)
        {
            Rigidbody additionalRb = additionalHydrogen.GetComponent<Rigidbody>();
            if (additionalRb != null)
            {
                additionalRb.velocity = (additionalHydrogen.transform.position.x < hydrogen1.position.x + 0.5f) ? hydrogen1Velocity : hydrogen2Velocity;
            }
        }
    }

    void HandleAdditionalHydrogen()
    {
        if (currentpH < 6.8f && !isAdditionalHydrogenInstantiated && Random.value < 0.1f) // 20% de probabilidad
        {
            Transform parentHydrogen = (Random.value > 0.5f) ? hydrogen1 : hydrogen2;
            additionalHydrogen = Instantiate(hydrogenPrefab, parentHydrogen.position + Vector3.right * 0.5f, Quaternion.identity, transform);
            isAdditionalHydrogenInstantiated = true;

            // Agregar un Rigidbody al hidrógeno adicional para heredar la velocidad
            Rigidbody additionalRb = additionalHydrogen.GetComponent<Rigidbody>();
            if (additionalRb == null)
            {
                additionalRb = additionalHydrogen.AddComponent<Rigidbody>();
                additionalRb.useGravity = false; // Opcional: desactivar gravedad
            }
        }

        if (isAdditionalHydrogenInstantiated && currentpH >= 7f)
        {
            Destroy(additionalHydrogen);
            isAdditionalHydrogenInstantiated = false;
        }
    }

    void HandleHydrogenDeactivation()
    {
        if (currentpH >= 7.2f && !isHydrogenDeactivated && Random.value < 0.1f) // 20% de probabilidad
        {
            // Seleccionar el hidrógeno que está más lejos del origen (posición en y más alta)
            deactivatedHydrogen = (hydrogen1.localPosition.y > hydrogen2.localPosition.y) ? hydrogen1 : hydrogen2;
            deactivatedHydrogen.gameObject.SetActive(false);
            isHydrogenDeactivated = true;
        }

        if (isHydrogenDeactivated && currentpH <= 7f)
        {
            deactivatedHydrogen.gameObject.SetActive(true);
            isHydrogenDeactivated = false;
        }
    }
}

using UnityEngine;

public class ChameleonBehaviourController : MonoBehaviour
{
    public int oxidationLevel = 4; // Nivel de oxidación inicial (KMnO4)
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el Animator
        SetIdleState(); // Configurar el primer estado visible al inicio
    }

    public void Oxidize()
    {
        switch (oxidationLevel)
        {
            case 4:
                // KMnO4: Si se oxida, no hacer nada
                break;

            case 3:
                // K2MnO4: Llamar a la animación remove_K1 y cambiar a KMnO4
                TriggerTransition("remove_K1", 4);
                break;

            case 2:
                // K3MnO4: Llamar a la animación remove_K2 y cambiar a K2MnO4
                TriggerTransition("remove_K2", 3);
                break;

            case 1:
                // MnO2: Llamar a la animación add_Ks_Os y cambiar a K3MnO4
                TriggerTransition("add_Ks_Os", 2);
                break;
        }
    }

    public void Reduce()
    {
        switch (oxidationLevel)
        {
            case 4:
                // KMnO4: Llamar a la animación addK_1 y cambiar a K2MnO4
                TriggerTransition("add_K1", 3);
                break;

            case 3:
                // K2MnO4: Llamar a la animación add_K2 y cambiar a K3MnO4
                TriggerTransition("add_K2", 2);
                break;

            case 2:
                // K3MnO4: Llamar a la animación remove_Ks_Os y cambiar a MnO2
                TriggerTransition("remove_Ks_Os", 1);
                break;

            case 1:
                // MnO2: Si se reduce, no hacer nada
                break;
        }
    }

    private void TriggerTransition(string triggerName, int newOxidationLevel)
    {
        //animator.ResetTrigger(triggerName); // Asegurarse de que el trigger esté limpio
        animator.SetTrigger(triggerName); // Activar el trigger de transición
        oxidationLevel = newOxidationLevel; // Actualizar el nivel de oxidación
    }

    private void SetIdleState()
    {
        switch (oxidationLevel)
        {
            case 4:
                animator.Play("idleMolecule");
                break;
            case 3:
                animator.Play("idleMoleculeK2MnO4");
                break;
            case 2:
                animator.Play("idleMoleculeK3MnO4");
                break;
            case 1:
                animator.Play("idleMoleculeMnO2");
                break;
        }
    }
}

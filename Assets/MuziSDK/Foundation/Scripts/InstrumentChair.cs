using Opsive.UltimateCharacterController.Traits;
using Opsive.UltimateCharacterController.Character;
using UnityEngine;

public class InstrumentChair : MonoBehaviour, IInteractableTarget
{
    GameObject currentCharacter;
    GameObject instructionToast;
    private readonly string keyInstructionToastString = "TWToast_KeyInstruction";

    public bool CanInteract(GameObject character)
    {
        return currentCharacter == null;
    }

    public void Interact(GameObject character)
    {
        currentCharacter = character;
        if (instructionToast != null)
            Destroy(instructionToast);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentCharacter == null)
        {
            instructionToast = TW.AddTWByName_s(keyInstructionToastString);
            instructionToast.GetComponent<TWToast_KeyInstruction>().SetSpawnTarget(other.transform, new Vector3(0, 1.2f, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (instructionToast != null)
            Destroy(instructionToast);
        if (other.transform.parent.parent == null) return;
        if (other.transform.parent.parent.gameObject == currentCharacter)
            currentCharacter = null;
    }
}

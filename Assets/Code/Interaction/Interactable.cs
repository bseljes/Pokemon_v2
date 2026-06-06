using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private string interactionName = "Interactable";

    public string InteractionName => interactionName;

    public abstract void Interact(GameObject player);
}

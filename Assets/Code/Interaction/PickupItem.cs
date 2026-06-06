using UnityEngine;

public class PickupItem : Interactable
{
    [SerializeField] private string itemName = "Potion";

    public override void Interact(GameObject player)
    {
        Debug.Log($"Picked up {itemName}. Later this will be added to the player's inventory.");
        Destroy(gameObject);
    }
}

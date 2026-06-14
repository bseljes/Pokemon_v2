using UnityEngine;

public class PickupItem : Interactable
{
    [SerializeField] private ItemData itemData;

    private bool hasBeenPickedUp;

    public override void Interact(GameObject player)
    {
        if (hasBeenPickedUp)
        {
            return;
        }

        hasBeenPickedUp = true;

        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogWarning("Player does not have a PlayerInventory component.");
            hasBeenPickedUp = false;
            return;
        }

        if (itemData == null)
        {
            Debug.LogWarning($"{name} does not have ItemData assigned.");
            hasBeenPickedUp = false;
            return;
        }

        inventory.AddItem(itemData);
        
        if (PickupPopupUI.Instance != null)
        {
            PickupPopupUI.Instance.ShowPickup(itemData.ItemName);
        }
        Destroy(gameObject);
    }
}
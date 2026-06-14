using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();

    public IReadOnlyList<ItemData> Items => items;

    public void AddItem(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogWarning("Tried to add a null item to inventory.");
            return;
        }

        items.Add(itemData);
        Debug.Log($"Added {itemData.ItemName} to inventory.");

        PrintInventory();
    }

    public void PrintInventory()
    {
        if (items.Count == 0)
        {
            Debug.Log("Inventory is empty.");
            return;
        }

        Debug.Log($"Inventory contains {items.Count} item(s):");

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log($"{i + 1}. {items[i].ItemName} ({items[i].ItemType})");
        }
    }
}
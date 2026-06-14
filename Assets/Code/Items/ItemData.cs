using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Pokemon/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [TextArea(2, 5)]
    [SerializeField] private string description;
    [SerializeField] private ItemType itemType;

    public string ItemName => itemName;
    public string Description => description;
    public ItemType ItemType => itemType;
}
public enum ItemType
{
    Consumable,
    PokeBall,
    Machine,
    Berry,
    KeyItem
}

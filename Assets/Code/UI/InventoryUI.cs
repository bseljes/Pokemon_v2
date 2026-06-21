using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryRoot;
    [SerializeField] private TMP_Text inventoryText;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Key toggleKey = Key.I;

    private bool isOpen;

    private void Awake()
    {
        if (playerInventory == null)
        {
            playerInventory = FindAnyObjectByType<PlayerInventory>();
        }

        CloseInventory();
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current[toggleKey].wasPressedThisFrame)
        {
            if (isOpen)
            {
                CloseInventory();
            }
            else
            {
                if (GameManager.Instance != null && !GameManager.Instance.IsGameplay)
                {
                    return;
                }

                OpenInventory();
            }
        }
    }

    public void OpenInventory()
    {
        if (inventoryRoot == null || inventoryText == null)
        {
            Debug.LogWarning("InventoryUI needs Inventory Root and Inventory Text references.");
            return;
        }

        RefreshInventoryText();
        inventoryRoot.SetActive(true);
        isOpen = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnterInventory();
        }
    }

    public void CloseInventory()
    {
        if (inventoryRoot != null)
        {
            inventoryRoot.SetActive(false);
        }

        isOpen = false;

        if (GameManager.Instance != null && TextBubbleUI.Instance != null && TextBubbleUI.Instance.IsShowing)
        {
            GameManager.Instance.EnterDialogue();
        }
        else if (GameManager.Instance != null)
        {
            GameManager.Instance.EnterGameplay();
        }
    }

    private void RefreshInventoryText()
    {
        if (playerInventory == null)
        {
            inventoryText.text = "No player inventory found.";
            return;
        }

        if (playerInventory.Items.Count == 0)
        {
            inventoryText.text = "Inventory is empty.";
            return;
        }

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Inventory");
        builder.AppendLine();

        for (int i = 0; i < playerInventory.Items.Count; i++)
        {
            ItemData item = playerInventory.Items[i];
            builder.AppendLine($"{i + 1}. {item.ItemName} ({item.ItemType})");
        }

        inventoryText.text = builder.ToString();
    }
}

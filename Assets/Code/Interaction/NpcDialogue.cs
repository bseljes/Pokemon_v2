using UnityEngine;

public class NpcDialogue : Interactable
{
    [SerializeField] private string npcName = "NPC";

    [TextArea(2, 5)]
    [SerializeField] private string[] dialogueLines;

    [SerializeField] private int currentLineIndex;
    [SerializeField] private string line;

    public override void Interact(GameObject player)
    {
        if (dialogueLines.Length == 0)
        {
            Debug.Log($"{npcName} has nothing to say.");
            return;
        }

        line = dialogueLines[currentLineIndex];

        if (TextBubbleUI.Instance != null)
        {
            Debug.Log($"BubbleUI found: {npcName}: {line}");
            TextBubbleUI.Instance.ShowMessage(line);
        }
        else
        {
            Debug.Log($"{npcName}: {line}");
        }
        Debug.Log($"currentLineIndex = {currentLineIndex}. currentLineIndex++.");
        currentLineIndex++;

        if (currentLineIndex >= dialogueLines.Length)
        {
            Debug.Log($"Resetting dialogue for {npcName}.");
            currentLineIndex = 0;
        }
    }
}
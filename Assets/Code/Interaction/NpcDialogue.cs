using UnityEngine;

public class NpcDialogue : Interactable
{
    [SerializeField] private string npcName = "NPC";

    [TextArea(2, 5)]
    [SerializeField] private string[] firstEncounterDialogueLines;
    [SerializeField] private string[] loopDialogueLines;

    [SerializeField] private int currentLineIndex;
    [SerializeField] private string line;
    // [SerializeField] private bool hasLoopDialogue = true;
    [SerializeField] private bool firstEncounterDialogueComplete = false;

    public override void Interact(GameObject player)
    {
        string[] dialogueLines = firstEncounterDialogueComplete ? loopDialogueLines : firstEncounterDialogueLines;

        if (dialogueLines.Length == 0)
        {
            Debug.Log($"{npcName} has nothing to say.");
            return;
        }

        line = dialogueLines[currentLineIndex];

        if (TextBubbleUI.Instance != null)
        {
            TextBubbleUI.Instance.ShowMessage(line);
        }
        else
        {
            Debug.Log("No TextBubbleUI instance found. Cannot display dialogue.");
        }
        currentLineIndex++;

        if (currentLineIndex >= dialogueLines.Length)
        {
            currentLineIndex = 0;
            if (!firstEncounterDialogueComplete)
            {
                firstEncounterDialogueComplete = true;
            }
        }
    }
}
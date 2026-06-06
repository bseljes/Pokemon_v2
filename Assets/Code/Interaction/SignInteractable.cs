using UnityEngine;

public class SignInteractable : Interactable
{
    [TextArea(2, 5)]
    [SerializeField] private string signText = "The sign is blank.";

    public override void Interact(GameObject player)
    {
        if (TextBubbleUI.Instance == null)
        {
            Debug.Log(signText);
            Debug.LogWarning("No TextBubbleUI found in the scene, so the sign text was only logged.");
            return;
        }

        TextBubbleUI.Instance.ShowMessage(signText);
    }
}

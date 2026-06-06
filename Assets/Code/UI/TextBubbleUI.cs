using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TextBubbleUI : MonoBehaviour
{
    public static TextBubbleUI Instance { get; private set; }
    public bool IsShowing => isShowing;
    public bool WasClosedThisFrame => Time.frameCount == messageClosedFrame;

    [SerializeField] private GameObject bubbleRoot;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Key closeKey = Key.E;

    private bool isShowing;
    private int messageOpenedFrame;
    private int messageClosedFrame = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        HideMessage(false);
    }

    private void Update()
    {
        if (!isShowing || Keyboard.current == null)
        {
            return;
        }

        if (Time.frameCount > messageOpenedFrame && Keyboard.current[closeKey].wasPressedThisFrame)
        {
            HideMessage();
        }
    }

    public void ShowMessage(string message)
    {
        if (bubbleRoot == null || messageText == null)
        {
            Debug.LogWarning("TextBubbleUI needs Bubble Root and Message Text references.");
            return;
        }

        messageText.text = message;
        bubbleRoot.SetActive(true);
        isShowing = true;
        messageOpenedFrame = Time.frameCount;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnterDialogue();
        }
    }

    public void HideMessage(bool enterGameplay = true)
    {
        if (bubbleRoot != null)
        {
            bubbleRoot.SetActive(false);
        }

        isShowing = false;
        messageClosedFrame = Time.frameCount;

        if (enterGameplay && GameManager.Instance != null)
        {
            GameManager.Instance.EnterGameplay();
        }
    }
}

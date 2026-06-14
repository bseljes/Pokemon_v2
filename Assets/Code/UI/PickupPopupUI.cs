using System.Collections;
using TMPro;
using UnityEngine;

public class PickupPopupUI : MonoBehaviour
{
    public static PickupPopupUI Instance { get; private set; }

    [SerializeField] private GameObject popupRoot;
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private float visibleSeconds = 1f;

    private Coroutine hideCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Hide();
    }

    public void ShowPickup(string itemName)
    {
        ShowMessage($"Picked up {itemName}.");
    }

    public void ShowMessage(string message)
    {
        if (popupRoot == null || popupText == null)
        {
            Debug.LogWarning("PickupPopupUI needs Popup Root and Popup Text references.");
            return;
        }

        popupText.text = message;
        popupRoot.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(visibleSeconds);
        Hide();
    }

    private void Hide()
    {
        if (popupRoot != null)
        {
            popupRoot.SetActive(false);
        }

        hideCoroutine = null;
    }
}

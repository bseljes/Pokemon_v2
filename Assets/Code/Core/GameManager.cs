using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameState currentState = GameState.Gameplay;

    public GameState CurrentState => currentState;
    public bool IsGameplay => currentState == GameState.Gameplay;
    public bool IsDialogue => currentState == GameState.Dialogue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void EnterGameplay()
    {
        SetState(GameState.Gameplay);
    }

    public void EnterDialogue()
    {
        SetState(GameState.Dialogue);
    }

    private void SetState(GameState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;
        Debug.Log($"Game state changed to {currentState}.");
    }
}

public enum GameState
{
    Gameplay,
    Dialogue,
    Paused
}

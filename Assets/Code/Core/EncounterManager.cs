using UnityEngine;
using UnityEngine.InputSystem;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance { get; private set; }
    public int stepsToEncounter;
    public PokemonData encounteredPokemon;
    public int encounteredLevel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsEncoutner)
        {
            return;
        }

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            EndEncounter();
        }
    }
    public void StartEncounter(PokemonData pokemon, int level)
    {
        if (pokemon == null)
        {
            Debug.LogWarning("Cannot start encounter because no PokemonData was provided.");
            return;
        }

        encounteredPokemon = pokemon;
        encounteredLevel = level;
        StartEncounter();
    }

    public void StartEncounter()
    {
        if (encounteredPokemon == null)
        {
            Debug.LogWarning("Cannot start encounter because encounteredPokemon is not set.");
            return;
        }

        Debug.Log($"Starting encounter with a {encounteredPokemon.name} at level {encounteredLevel}!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnterEncounter();
        }
        else
        {
            Debug.LogWarning("No GameManager found in the scene.");
        }
        // TODO: Add encounter setup logic here (e.g., spawn enemies, show encounter UI, etc.)
    }
    public void EndEncounter()
    {
        Debug.Log("Ending encounter...");
        GameManager.Instance.EnterGameplay();
        // TODO: Add encounter cleanup logic here (e.g., despawn enemies, hide encounter UI, etc.)
    }
}

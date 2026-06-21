using UnityEngine;

public class GrassPatchEncounterArea : MonoBehaviour
{
    [SerializeField] private int stepsToEncounterMin = 5;
    [SerializeField] private int stepsToEncounterMax = 10;
    [SerializeField] private int minPokemonLevel = 1;
    [SerializeField] private int maxPokemonLevel = 3;
    [SerializeField] private int stepsToEncounter;
    [SerializeField] private bool hasActiveStepCounter;
    public int StepsToEncounter => stepsToEncounter;
    [SerializeField] private PokemonData[] possibleEncounters;
    [SerializeField] private PokemonData encounteredPokemon;
    [SerializeField] private int encounteredLevel;

    public PokemonData GetRandomEncounter()
    {
        if (possibleEncounters == null || possibleEncounters.Length == 0)
        {
            Debug.LogWarning($"{name} has no possible encounters assigned.");
            return null;
        }

        int totalWeight = GetTotalEncounterWeight();

        if (totalWeight <= 0)
        {
            Debug.LogWarning($"{name} has possible encounters, but their encounter weights are all 0 or lower.");
            return null;
        }

        int selectedWeight = Random.Range(0, totalWeight);
        PokemonData selectedPokemon = PickPokemonByWeight(selectedWeight);

        if (selectedPokemon == null)
        {
            Debug.LogWarning($"{name} failed to pick a Pokemon from the encounter table.");
            return null;
        }

        encounteredLevel = Random.Range(minPokemonLevel, maxPokemonLevel + 1);
        return selectedPokemon;
    }

    private void EncounterAnnouncemnet()
    {
        if (TextBubbleUI.Instance == null)
        {
            Debug.LogWarning("No TextBubbleUI found in the scene, so the encoutner info was only logged.");
            return;
        }
        TextBubbleUI.Instance.ShowMessage($"A wild {encounteredPokemon.name} appeared at level {encounteredLevel}!");
        }

    private int GetTotalEncounterWeight()
    {
        int totalWeight = 0;

        foreach (PokemonData pokemon in possibleEncounters)
        {
            if (pokemon == null)
            {
                continue;
            }

            totalWeight += Mathf.Max(0, pokemon.encounterWeight);
        }

        return totalWeight;
    }

    private PokemonData PickPokemonByWeight(int selectedWeight)
    {
        int runningWeight = 0;

        foreach (PokemonData pokemon in possibleEncounters)
        {
            if (pokemon == null || pokemon.encounterWeight <= 0)
            {
                continue;
            }

            runningWeight += pokemon.encounterWeight;

            if (selectedWeight < runningWeight)
            {
                return pokemon;
            }
        }

        return null;
    }
    public void SetStepsToEncounter()
    {
        int steps = Random.Range(stepsToEncounterMin, stepsToEncounterMax + 1);
        stepsToEncounter = steps;
        hasActiveStepCounter = true;
        Debug.Log($"Steps to encounter set to {steps}.");
    }

    public void RegisterPlayerStep(GameObject player)
    {
        if (!hasActiveStepCounter)
        {
            SetStepsToEncounter();
        }

        stepsToEncounter--;
        Debug.Log($"Grass step registered. Steps to encounter: {stepsToEncounter}.");

        if (stepsToEncounter > 0)
        {
            return;
        }

        hasActiveStepCounter = false;
        encounteredPokemon = GetRandomEncounter();

        if (EncounterManager.Instance != null)
        {
            EncounterManager.Instance.StartEncounter(encounteredPokemon, encounteredLevel);
        }
        else
        {
            Debug.LogWarning("No EncounterManager found in the scene.");
        }
        EncounterAnnouncemnet();
    }
}

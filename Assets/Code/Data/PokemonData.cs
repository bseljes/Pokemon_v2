using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Pokemon Data")]
public class PokemonData : ScriptableObject
{
    [SerializeField] private int pokedexNumber;
    [SerializeField] private string pokemonName;
    [SerializeField] private string pokedexDescription;
    [SerializeField] private PokemonType primaryType;
    [SerializeField] private PokemonType secondaryType;
    [SerializeField] private bool hasSecondaryType;
    [SerializeField] private int catchRate;
    [SerializeField] private int baseExperienceYield;
    [SerializeField] private MoveData levelOneMoveOne;
    [SerializeField] private MoveData levelOneMoveTwo;
    [SerializeField] private MoveData levelOneMoveThree;
    [SerializeField] private MoveData levelOneMoveFour;
    [SerializeField] private bool hasEvolution;
    [SerializeField] private int evolutionLevel;
    [SerializeField] private PokemonData evolutionData;
    [SerializeField] public int encounterWeight;
    

    [Header("Base Stats")]
    [SerializeField] private int maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int speed;
    [SerializeField] private int special;

    public string PokemonName => pokemonName;
}

public enum PokemonType
{
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel,
    Fairy
}
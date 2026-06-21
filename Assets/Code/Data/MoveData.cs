using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Pokemon/Move Data")]
public class MoveData : ScriptableObject
{
    [SerializeField] private string moveName;
    [SerializeField] private PokemonType moveType;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int pp;
    [SerializeField] private string speed;
    [SerializeField] private string description;

    public string MoveName => moveName;
    public PokemonType MoveType => moveType;
    public int Power => power;
    public int Accuracy => accuracy;
    public int PP => pp;
}
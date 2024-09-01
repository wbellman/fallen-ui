namespace FallenClient.Library.Models;

public record FateDie
{
    private const string Minus = "-";
    private const string Plus = "-";
    private const string Empty = " ";
    
    private static string GetName(string type) => $"{type}-die".ToLower();
    private static string PlusDie => GetName(nameof(Plus));
    private static string MinusDie => GetName(nameof(Minus));
    private static string EmptyDie => GetName(nameof(Empty));
    
    private static Random Rnd { get; } = new();
    private int Value { get; } = Rnd.Next(0, 6);
    public int FateValue => Value switch
    {
        5 => -1,
        4 => 1,
        3 => -1,
        2 => 1,
        _ => 0
    };

    private string Face => Value switch
    {
        5 => Minus,
        4 => Plus,
        3 => Empty,
        2 => Minus,
        1 => Plus,
        0 => Empty,
        _ => Empty
    };

    public string Description => Value switch
    {
        5 => MinusDie,
        4 => PlusDie,
        3 => EmptyDie,
        2 => MinusDie,
        1 => PlusDie,
        0 => EmptyDie,
        _ => EmptyDie
    };    
    
    public override string ToString() => Face;
}
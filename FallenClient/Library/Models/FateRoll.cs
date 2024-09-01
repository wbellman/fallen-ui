namespace FallenClient.Library.Models;

public record FateRoll
{
    public FateDie[] Dice { get; } = [new(), new(), new(), new()];
    
    public int Total => Dice.Sum(d => d.FateValue);

    public override string ToString()
        => string.Join(" - ", Dice.Select(d => $"[{d}]"));
}
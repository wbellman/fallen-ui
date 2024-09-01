namespace FallenClient.Library.Models;

public record AdjustedRoll(int Modifier)
{
    
    public FateRoll Roll { get; } = new();
    
    public int Total => Roll.Total + Modifier;

    public override string ToString()
        => $"{Roll} {Modifier:+#;-#;0}";

    public string Face
        => Total switch
        {
            <= -4 => "🤮",
            -3 => "🤕",
            -2 => "😵‍💫",
            -1 => "🫣",
            0 => "😐",
            1 => "😊",
            2 => "😏",
            3 => "😁",
            >= 4 => "😎",
        };

    public string Outcome
        => Total switch
        {
            <= -4 => "catastrophe",
            -3 => "disaster",
            -2 => "miserable",
            -1 => "minor setback",
            0 => "mediocre",
            1 => "fair",
            2 => "solid",
            3 => "excellent",
            >= 4 => "succeed with style",
        };
}
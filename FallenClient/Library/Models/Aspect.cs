using FallenClient.Library.Values;

namespace FallenClient.Library.Models;

public record Aspect(
    AspectType Type,
    string Description,
    int Uses = 0, // Default value for Uses
    int Modifier = 0 // Default value for Modifier
);

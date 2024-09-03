using System.Text.Json.Serialization;
using FallenClient.Library.Values;

namespace FallenClient.Library.Models;

public record Aspect(
    AspectType Type,
    string Description,
    int Uses = 0, // Default value for Uses
    int Modifier = 0 // Default value for Modifier
)
{
    [JsonIgnore] public bool HasUses => Uses > 0;    
    [JsonIgnore] public bool EmptyDescription => string.IsNullOrWhiteSpace(Description);
}

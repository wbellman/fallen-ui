using System.Text.Json.Serialization;

namespace FallenClient.Library.Models;

public record Character(
    string? Name,
    List<Aspect> Aspects,
    List<OwnedSkill> Skills
)
{
    [JsonIgnore] public bool HasAspects => Aspects.Any();
    [JsonIgnore] public bool HasSkills => Skills.Any();
}
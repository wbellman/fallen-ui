namespace FallenClient.Library.Models;

public record Character(
    string? Name,
    List<Aspect> Aspects,
    List<OwnedSkill> Skills
);
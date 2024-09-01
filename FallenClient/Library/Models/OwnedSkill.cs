using FallenClient.Library.Values;

namespace FallenClient.Library.Models;

public record OwnedSkill(Skill Skill, int Modifier = 0);
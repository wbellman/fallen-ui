using System.Text.Json.Serialization;
using FallenClient.Library.Values;

namespace FallenClient.Library.Models;

public class Character
{
    private int _coins = 3;
    
    private const string DefaultPortrait = "./images/default-character.png";
    
    [JsonIgnore] public bool HasBenefits => Benefits?.Count != 0;
    [JsonIgnore] public bool HasConditions => Conditions?.Count != 0;
    [JsonIgnore] public bool HasSkills => Skills?.Count != 0;
    [JsonIgnore] public bool HasCoins => Coins > 0;

    public string? Name { get; init; }
    public string Portrait { get; init; } = DefaultPortrait;
    public Aspect HighConcept { get; init; } = Aspect.Empty.HighConcept;
    public Aspect Trouble { get; init; } = Aspect.Empty.Trouble;
    public List<Aspect> Benefits { get; init; } = [];
    public List<Aspect> Conditions { get; init; } = [];
    public List<OwnedSkill> Skills { get; init; } = [];

    public int Coins
    {
        get => _coins; 
        set => _coins = value < 0 ? 0 : value;
    }

    public void Deconstruct(
        out string? name,
        out int coins,
        out Aspect highConcept,
        out Aspect trouble,
        out List<Aspect> benefits,
        out List<Aspect> conditions,
        out List<OwnedSkill> skills
    )
    {
        name = this.Name;
        coins = this.Coins;
        highConcept = this.HighConcept ?? new Aspect(AspectType.Concept, string.Empty);
        trouble = this.Trouble ?? new Aspect(AspectType.Trouble, string.Empty);
        benefits = this.Benefits ?? [];
        conditions = this.Conditions ?? [];
        skills = this.Skills ?? [];
    }
}
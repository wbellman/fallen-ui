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

    public static class Empty
    {
        private static Aspect As(AspectType type) => new(type, string.Empty);
        public static Aspect Trouble => As(AspectType.Trouble);
        public static Aspect HighConcept => As(AspectType.Concept);
        public static Aspect Aspect => As(AspectType.Aspect);
        public static Aspect Boost => As(AspectType.Boost);
        public static Aspect Consequence => As(AspectType.Consequence);
        public static Aspect Situation => As(AspectType.Situation);
        public static Aspect Scene => As(AspectType.Scene);
        public static Aspect Magic => As(AspectType.Magic);
        public static Aspect Stressor => As(AspectType.Stressor);
        public static Aspect Stunt => As(AspectType.Stunt);
    }
}

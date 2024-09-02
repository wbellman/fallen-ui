namespace FallenClient.Library.Values;

[Flags]
public enum AspectType
{
   Aspect = 1 << 0,
   Boost = 1 << 1,
   Consequence = 1 << 2,
   Concept = 1 << 3,
   Stressor = 1 << 4,
   Stunt = 1 << 5,
   Trouble = 1 << 6,
   Situation = 1 << 7,
   Scene = 1 << 8,
   Magic = 1 << 9
}
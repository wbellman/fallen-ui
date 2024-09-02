namespace FallenClient.Library.Models;

public class ActiveAspect(Guid id, Aspect aspect)
{
    public Guid Id { get; } = id;
    public Aspect Aspect { get; set; } = aspect;
}
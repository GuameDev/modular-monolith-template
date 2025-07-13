namespace ModularMonolith.Template.Domain.Common;

public interface IDomainEvent
{
    public DateTime OccurredOnUtc { get; }
}

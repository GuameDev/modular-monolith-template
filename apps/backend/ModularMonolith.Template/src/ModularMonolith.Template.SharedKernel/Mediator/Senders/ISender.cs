namespace ModularMonolith.Template.SharedKernel.Mediator.Senders;

using ModularMonolith.Template.SharedKernel.Mediator.Handlers;
public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}

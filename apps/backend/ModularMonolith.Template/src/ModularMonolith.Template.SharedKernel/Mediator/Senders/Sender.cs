using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Template.SharedKernel.Mediator.Handlers;

namespace ModularMonolith.Template.SharedKernel.Mediator.Senders;

internal sealed class Sender : ISender
{
    private readonly IServiceProvider _provider;

    public Sender(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = _provider.GetRequiredService(handlerType);

        return await handler.Handle((dynamic)request, cancellationToken);
    }
}

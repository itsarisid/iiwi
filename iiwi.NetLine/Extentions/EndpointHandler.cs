using DotNetCore.Results;

namespace iiwi.NetLine.Extensions;
public class EndpointHandler<TRequest, TResponse>(IMediator mediator)
where TRequest : class, new()
where TResponse : class, new()
{
    private readonly TRequest T = new();
    internal Task<Result<TResponse>> HandleDelegate() => mediator.HandleAsync<TRequest, TResponse>(T);
    internal Task<Result<TResponse>> HandleDelegate(TRequest T) => mediator.HandleAsync<TRequest, TResponse>(T);
}

using iiwi.Model;

namespace iiwi.NetLine.Extentions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithMappingBehaviour<T>(this RouteHandlerBuilder builder)
    {
        return builder.Produces<T>();
    }

    public static RouteHandlerBuilder WithEndpointsGroup(this RouteHandlerBuilder builder, EndpointDetails group)
    {
        return builder.WithName(group.Name)
                      .WithSummary(group.Summary)
                      .WithDescription(group.Description);
    }
}

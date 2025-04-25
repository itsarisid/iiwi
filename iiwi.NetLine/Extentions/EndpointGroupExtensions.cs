using iiwi.Model;

namespace iiwi.NetLine.Extentions;

public static class EndpointGroupExtensions
{
    public static RouteGroupBuilder WithGroup(this RouteGroupBuilder builder, EndpointDetails group)
    {
        builder
            .WithTags(group.Tags)
            .WithName(group.Name)
            .WithDescription(group.Description)
            .WithOpenApi();
        return builder;
    }
}

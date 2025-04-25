using iiwi.Common;

namespace iiwi.NetLine.Extentions;

public static class EndpointGroupExtensions
{
    public static RouteGroupBuilder WithGroup(this RouteGroupBuilder builder, EndpointGroup group)
    {
        builder
            .WithTags(group.Tags)
            .WithName(group.Name)
            .WithDescription(group.Description)
            .WithOpenApi();
        return builder;
    }
}

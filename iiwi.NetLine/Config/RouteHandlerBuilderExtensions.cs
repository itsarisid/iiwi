namespace iiwi.NetLine.Config;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithMappingBehaviour<T>(this RouteHandlerBuilder builder)
    {
       return builder.Produces<T>();
    }
}

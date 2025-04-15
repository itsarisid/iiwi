namespace iiwi.NetLine.Config;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithMappingBehaviour<T>(this RouteHandlerBuilder builder)
    {
       return builder
            .Produces<T>(StatusCodes.Status400BadRequest)
            .Produces<T>(StatusCodes.Status401Unauthorized)
            .Produces<T>(StatusCodes.Status403Forbidden)
            .Produces<T>(StatusCodes.Status404NotFound)
            .Produces<T>(StatusCodes.Status500InternalServerError);
    }
}

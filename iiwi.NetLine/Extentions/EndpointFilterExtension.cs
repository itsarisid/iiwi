namespace iiwi.NetLine.Extensions;

public static class EndpointFilterExtension
{
    public static void AddFiltersByNames(this RouteHandlerBuilder builder, IEnumerable<string> filterNames)
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name;

        foreach (var filterName in filterNames)
        {
            // Search for the type by its full name within the current assembly.
            var filterType = Type.GetType($"{assemblyName}.Filters.{filterName}");

            if (filterType != null && typeof(IEndpointFilter).IsAssignableFrom(filterType))
            {
                // Instead of creating the instance here, create a factory delegate.
                // This delegate will be executed by the runtime when the endpoint is invoked.
                builder.AddEndpointFilter(async (context, next) =>
                {
                    // Resolve the filter instance using the request's service provider.
                    var filterInstance = (IEndpointFilter)context.HttpContext.RequestServices.GetRequiredService(filterType);

                    // Now invoke the filter's logic.
                    return await filterInstance.InvokeAsync(context, next);
                });
            }
            else
            {
                Console.WriteLine($"Filter type '{filterName}' not found or does not implement IEndpointFilter.");
            }
        }
    }

}

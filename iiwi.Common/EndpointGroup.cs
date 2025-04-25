namespace iiwi.Common;


public class EndpointGroup
{
    public string Name { get; set; }
    public string Tags { get; set; }
    public string Description { get; set; }
}

public class Endpoints
{
    public static EndpointGroup Accounts => new()
    {
        Name = "Accounts",
        Tags = "Accounts",
        Description = "This group contains all the endpoints related to accounts."
    };
    public static EndpointGroup Authentication => new()
    {
        Name = "Authentication",
        Tags = "Authentication",
        Description = "This group contains all the endpoints related to authentication."
    };
}



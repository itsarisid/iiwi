using iiwi.Model;

namespace iiwi.NetLine.Modules;

public class EndpointGroup
{
    public static EndpointDetails Accounts => new()
    {
        Name = "Accounts",
        Tags = "Accounts",
        Summary = "Accounts",
        Description = "This group contains all the endpoints related to accounts."
    };
    public static EndpointDetails Authentication => new()
    {
        Name = "Authentication",
        Tags = "Authentication",
        Summary = "Authentication",
        Description = "This group contains all the endpoints related to authentication."
    };
}

public class Endpoints
{
    public static EndpointDetails UpdateProfile => new()
    {
        Endpoint = "/update-profile",
        Name = "Update Profile",
        Summary = "This api can be used for update user profiles",
        Description = "This api can be used for update user profiles"
    };

    public static EndpointDetails SendVerificationDetails => new()
    {
        Endpoint = "/send-verification-email",
        Name = "Send Verification Details",
        Summary = "Send Verification Email Address",
        Description = "Send Verification Email Address"
    };


}



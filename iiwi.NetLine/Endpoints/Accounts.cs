using iiwi.Model;

namespace iiwi.NetLine.Endpoints;

public class Accounts
{
    public static EndpointDetails Group => new()
    {
        Name = "Accounts",
        Tags = "Accounts",
        Summary = "Accounts",
        Description = "This group contains all the endpoints related to accounts."
    };
    public static EndpointDetails UpdateProfile => new()
    {
        Endpoint = "/update-profile",
        Name = "Update Profile",
        Summary = "This API can be used to update user profiles",
        Description = "This API can be used to update user profiles"
    };
    public static EndpointDetails SendVerificationDetails => new()
    {
        Endpoint = "/send-verification-email",
        Name = "Send Verification Details",
        Summary = "Send Verification Email Address",
        Description = "Send Verification Email Address"
    };
    public static EndpointDetails DownloadPersonalData => new()
    {
        Endpoint = "/download-personal-data",
        Name = "Download Personal Data",
        Summary = "Download Personal Data",
        Description = "Download Personal Data"
    };

}

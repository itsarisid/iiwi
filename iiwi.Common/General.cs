
namespace iiwi.Common;

public static class General
{
    public static string SchemaName  = "iiwi";
    public static string Files = "Files";
    public static string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
}


namespace iiwi.Common;

public static class General
{
    public const string SchemaName = "iiwi";
    public const string DbSequenceName = "iiwi.Sequence";
    public const string DbConnectionName = "DefaultConnection";
    public const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    public static class Schema
    {
        public const string Identity = "Identity";
        public const string Log = "Log";
        public const string Files = "Files";
    }
}

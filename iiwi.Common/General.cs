
namespace iiwi.Common;

public static class General
{
    public const string DbSequenceName = "iiwi.Sequence";
    public const string DbConnectionName = "DefaultConnection";
    public const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    public static class Schema
    {
        public const string Name = "iiwi";
        public const string Identity = "Identity";
        public const string Log = "Log";
        public const string Files = "Files";
    }

    public static class Directories
    {
        public const string Logs = "Logs";
        public const string Audit = "Audit";
        public const string Files = "Files";
        public const string Temp = "Temp";
    }
}

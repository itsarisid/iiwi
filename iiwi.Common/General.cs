namespace iiwi.Common;

/// <summary>
/// Contains general-purpose constants and configuration values used throughout the application.
/// </summary>
public static class General
{
    /// <summary>
    /// The name of the database sequence used for generating unique IDs across tables.
    /// </summary>
    public const string DbSequenceName = "iiwi.Sequence";

    /// <summary>
    /// The name of the default database connection string in configuration files.
    /// </summary>
    public const string DbConnectionName = "DefaultConnection";

    /// <summary>
    /// URI format for generating TOTP (Time-based One-Time Password) authenticator QR codes.
    /// Format parameters: 0 = Issuer, 1 = Account, 2 = Secret Key
    /// </summary>
    public const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    /// <summary>
    /// Contains constants for database schema names used in the application.
    /// </summary>
    public static class Schema
    {
        /// <summary>
        /// The default schema name for core application tables.
        /// </summary>
        public const string Name = "iiwi";

        /// <summary>
        /// Schema name for identity and authentication related tables.
        /// </summary>
        public const string Identity = "Identity";

        /// <summary>
        /// Schema name for application logging tables.
        /// </summary>
        public const string Log = "Log";

        /// <summary>
        /// Schema name for file storage related tables.
        /// </summary>
        public const string Files = "Files";
    }

    /// <summary>
    /// Contains constants for directory names used in the application's file system structure.
    /// </summary>
    public static class Directories
    {
        /// <summary>
        /// Root directory name for application log files.
        /// </summary>
        public const string Logs = "Logs";

        /// <summary>
        /// Subdirectory name for audit log files (typically within the Logs directory).
        /// </summary>
        public const string Audit = "Audit";

        /// <summary>
        /// Root directory name for file uploads and storage.
        /// </summary>
        public const string Files = "Files";

        /// <summary>
        /// Directory name for temporary files that can be safely deleted.
        /// </summary>
        public const string Temp = "Temp";
    }
}

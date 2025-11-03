using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iiwi.Database.Migrations.Seeds
{
    /// <inheritdoc />
    public partial class SeedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Permission",
                columns: new[] { "Id", "CodeName", "CreatedByUserId", "CreationDate", "DeletedByUserId", "DeletedDate", "Description", "IsActive", "IsDeleted", "UpdateByUserId", "UpdateDate" },
                values: new object[,]
                {
                    { 1L, "TEST.READ", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Test.Read", true, null, null, null },
                    { 2L, "TEST.CREATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Test.Create", true, null, null, null },
                    { 3L, "TEST.UPDATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Test.Update", true, null, null, null },
                    { 4L, "TEST.DELETE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Test.Delete", true, null, null, null },
                    { 5L, "ACCOUNT.READ", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.Read", true, null, null, null },
                    { 6L, "ACCOUNT.CREATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.Create", true, null, null, null },
                    { 7L, "ACCOUNT.UPDATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.Update", true, null, null, null },
                    { 8L, "ACCOUNT.DELETE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.Delete", true, null, null, null },
                    { 9L, "ACCOUNT.UPDATEPROFILE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.UpdateProfile", true, null, null, null },
                    { 10L, "ACCOUNT.DELETEPERSONALDATA", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.DeletePersonalData", true, null, null, null },
                    { 11L, "ACCOUNT.DOWNLOADINFO", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.DownloadInfo", true, null, null, null },
                    { 12L, "ACCOUNT.SENDVERIFICATIONDETAILS", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.SendVerificationDetails", true, null, null, null },
                    { 13L, "ACCOUNT.CHANGEEMAIL", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.ChangeEmail", true, null, null, null },
                    { 14L, "ACCOUNT.UPDATEPHONENUMBER", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Account.UpdatePhoneNumber", true, null, null, null },
                    { 15L, "AUTHENTICATION.READ", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.Read", true, null, null, null },
                    { 16L, "AUTHENTICATION.CREATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.Create", true, null, null, null },
                    { 17L, "AUTHENTICATION.UPDATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.Update", true, null, null, null },
                    { 18L, "AUTHENTICATION.DELETE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.Delete", true, null, null, null },
                    { 19L, "AUTHENTICATION.LOADKEYANDQRCODEURI", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.LoadKeyAndQrCodeUri", true, null, null, null },
                    { 20L, "AUTHENTICATION.EXTERNALLOGINS", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.ExternalLogins", true, null, null, null },
                    { 21L, "AUTHENTICATION.REMOVELOGIN", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.RemoveLogin", true, null, null, null },
                    { 22L, "AUTHENTICATION.LINKLOGIN", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.LinkLogin", true, null, null, null },
                    { 23L, "AUTHENTICATION.LINKLOGINCALLBACK", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.LinkLoginCallback", true, null, null, null },
                    { 24L, "AUTHENTICATION.GENERATERECOVERYCODES", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.GenerateRecoveryCodes", true, null, null, null },
                    { 25L, "AUTHENTICATION.RESETAUTHENTICATOR", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.ResetAuthenticator", true, null, null, null },
                    { 26L, "AUTHENTICATION.SETPASSWORD", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.SetPassword", true, null, null, null },
                    { 27L, "AUTHENTICATION.CHANGEPASSWORD", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.ChangePassword", true, null, null, null },
                    { 28L, "AUTHENTICATION.ACCOUNTSTATUS", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.AccountStatus", true, null, null, null },
                    { 29L, "AUTHENTICATION.FORGOTBROWSER", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.ForgotBrowser", true, null, null, null },
                    { 30L, "AUTHENTICATION.DISABLE2FA", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authentication.Disable2fa", true, null, null, null },
                    { 31L, "AUTHORIZATION.READ", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.Read", true, null, null, null },
                    { 32L, "AUTHORIZATION.CREATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.Create", true, null, null, null },
                    { 33L, "AUTHORIZATION.UPDATE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.Update", true, null, null, null },
                    { 34L, "AUTHORIZATION.DELETE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.Delete", true, null, null, null },
                    { 35L, "AUTHORIZATION.ALLROLES", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.AllRoles", true, null, null, null },
                    { 36L, "AUTHORIZATION.ADDROLE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.AddRole", true, null, null, null },
                    { 37L, "AUTHORIZATION.UPDATEROLE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.UpdateRole", true, null, null, null },
                    { 38L, "AUTHORIZATION.DELETEROLE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.DeleteRole", true, null, null, null },
                    { 39L, "AUTHORIZATION.ROLESBYID", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.RolesById", true, null, null, null },
                    { 40L, "AUTHORIZATION.ADDCLAIM", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.AddClaim", true, null, null, null },
                    { 41L, "AUTHORIZATION.ADDROLECLAIM", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.AddRoleClaim", true, null, null, null },
                    { 42L, "AUTHORIZATION.REMOVEROLECLAIM", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.RemoveRoleClaim", true, null, null, null },
                    { 43L, "AUTHORIZATION.GETROLECLAIMS", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.GetRoleClaims", true, null, null, null },
                    { 44L, "AUTHORIZATION.ASSIGNROLE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.AssignRole", true, null, null, null },
                    { 45L, "AUTHORIZATION.ASSIGNROLE", null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Authorization.AssignRole", true, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 45L);
        }
    }
}

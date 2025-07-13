using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iiwi.Database.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialCreate_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                schema: "Identity",
                table: "UserRole");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Identity",
                table: "UserRole",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                schema: "Identity",
                table: "UserRole",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                schema: "Identity",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Identity",
                table: "UserRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                schema: "Identity",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" });
        }
    }
}

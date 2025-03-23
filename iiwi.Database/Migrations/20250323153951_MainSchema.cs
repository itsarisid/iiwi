using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iiwi.Database.Migrations
{
    /// <inheritdoc />
    public partial class MainSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "iiwi");

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "iiwi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "iiwi",
                table: "Permission",
                columns: new[] { "Id", "CodeName", "CreatedByUserId", "CreationDate", "DeletedByUserId", "DeletedDate", "IsActive", "Name", "UpdateByUserId" },
                values: new object[] { 1L, "admin", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Admin", new Guid("00000000-0000-0000-0000-000000000000") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permission",
                schema: "iiwi");
        }
    }
}

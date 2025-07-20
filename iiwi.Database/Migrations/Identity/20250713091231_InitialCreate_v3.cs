using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iiwi.Database.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialCreate_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "Identity");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Identity",
                table: "Permission");

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TablePk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "Identity");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Identity",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Identity",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "Identity",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                schema: "Identity",
                table: "RolePermission",
                column: "RoleId");
        }
    }
}

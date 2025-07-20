using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iiwi.Database.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialCreate_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiLogs",
                schema: "Identity",
                columns: table => new
                {
                    TraceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HttpMethod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FormVariables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionParameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "int", nullable: false),
                    ResponseStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseHeaders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelStateValid = table.Column<bool>(type: "bit", nullable: false),
                    ModelStateErrors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLogs", x => x.TraceId);
                });

            migrationBuilder.CreateTable(
                name: "ApiLog_Requeses",
                schema: "Identity",
                columns: table => new
                {
                    ApiLogId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLog_Requeses", x => x.ApiLogId);
                    table.ForeignKey(
                        name: "FK_ApiLog_Requeses_ApiLogs_ApiLogId",
                        column: x => x.ApiLogId,
                        principalSchema: "Identity",
                        principalTable: "ApiLogs",
                        principalColumn: "TraceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiLog_Responses",
                schema: "Identity",
                columns: table => new
                {
                    ApiLogId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLog_Responses", x => x.ApiLogId);
                    table.ForeignKey(
                        name: "FK_ApiLog_Responses_ApiLogs_ApiLogId",
                        column: x => x.ApiLogId,
                        principalSchema: "Identity",
                        principalTable: "ApiLogs",
                        principalColumn: "TraceId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLog_Requeses",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ApiLog_Responses",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ApiLogs",
                schema: "Identity");
        }
    }
}

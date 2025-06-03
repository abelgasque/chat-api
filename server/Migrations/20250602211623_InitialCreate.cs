using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerSide.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TENANT",
                columns: table => new
                {
                    TENANT_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENANT_GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TENENT_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TENENT_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TENENT_DELETED_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TENANT_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TENANT_DATABASE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TENANT", x => x.TENANT_ID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    USER_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    USER_MAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    USER_PASSWORD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    USER_AUTH_ATTEMPTS = table.Column<int>(type: "int", nullable: false),
                    USER_ACTIVE_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    USER_BLOCKED_AT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.USER_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_USER_MAIL",
                table: "USER",
                column: "USER_MAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TENANT");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}

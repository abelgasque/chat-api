using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    CUSTOMER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AUTH_ATTEMPTS = table.Column<int>(type: "int", nullable: false),
                    ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    BLOCK = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER", x => x.CUSTOMER);
                });

            migrationBuilder.InsertData(
                table: "CUSTOMER",
                columns: new[] { "CUSTOMER", "ACTIVE", "AUTH_ATTEMPTS", "BLOCK", "CREATION_DATE", "FIRST_NAME", "LAST_NAME", "MAIL", "PASSWORD", "UPDATE_DATE" },
                values: new object[] { new Guid("12c67a9f-4111-43e1-94f5-167c1ae0d20b"), true, 0, false, new DateTime(2023, 1, 5, 20, 12, 10, 869, DateTimeKind.Local).AddTicks(3635), "Abel", "Gasque L. Silva", "contato.abelgasque@gmail.com", "admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_MAIL",
                table: "CUSTOMER",
                column: "MAIL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CUSTOMER");
        }
    }
}

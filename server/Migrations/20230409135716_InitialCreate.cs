using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    ID_CUSTOMER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_CUSTOMER_ROLE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PASSWORD_TEMP_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PASSWORD_TEMP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AUTH_ATTEMPTS = table.Column<int>(type: "int", nullable: false),
                    ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    BLOCK = table.Column<bool>(type: "bit", nullable: false),
                    IS_NEW_CUSTOMER = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER", x => x.ID_CUSTOMER);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER_ROLE",
                columns: table => new
                {
                    ID_CUSTOMER_ROLE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER_ROLE", x => x.ID_CUSTOMER_ROLE);
                });

            migrationBuilder.CreateTable(
                name: "DEVICE",
                columns: table => new
                {
                    ID_DEVICE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_CUSTOMER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EXPIRATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CODE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ONLINE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEVICE", x => x.ID_DEVICE);
                });

            migrationBuilder.CreateTable(
                name: "MAIL_MESSAGE",
                columns: table => new
                {
                    ID_MAIL_MESSAGE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_CUSTOMER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SEND_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BODY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAIL_MESSAGE", x => x.ID_MAIL_MESSAGE);
                });

            migrationBuilder.InsertData(
                table: "CUSTOMER",
                columns: new[] { "ID_CUSTOMER", "ACTIVE", "AUTH_ATTEMPTS", "BLOCK", "CODE", "CREATION_DATE", "FIRST_NAME", "ID_CUSTOMER_ROLE", "IS_NEW_CUSTOMER", "LAST_NAME", "MAIL", "PASSWORD", "PASSWORD_TEMP", "PASSWORD_TEMP_DATE", "UPDATE_DATE" },
                values: new object[] { new Guid("08c26113-f776-4b2d-bc76-7d1e1fa7ec3a"), true, 0, false, null, new DateTime(2023, 4, 9, 10, 57, 16, 174, DateTimeKind.Local).AddTicks(1019), "Abel", new Guid("34b23fbf-8eae-4cb9-b6ee-8cd2a30a8197"), false, "Gasque L. Silva", "contato.abelgasque@gmail.com", "admin", null, null, null });

            migrationBuilder.InsertData(
                table: "CUSTOMER_ROLE",
                columns: new[] { "ID_CUSTOMER_ROLE", "ACTIVE", "CODE", "CREATION_DATE", "NAME", "UPDATE_DATE" },
                values: new object[] { new Guid("34b23fbf-8eae-4cb9-b6ee-8cd2a30a8197"), true, "ROLE_ADMINISTRATOR", new DateTime(2023, 4, 9, 10, 57, 16, 172, DateTimeKind.Local).AddTicks(9037), "Administrator", null });

            migrationBuilder.InsertData(
                table: "CUSTOMER_ROLE",
                columns: new[] { "ID_CUSTOMER_ROLE", "ACTIVE", "CODE", "CREATION_DATE", "NAME", "UPDATE_DATE" },
                values: new object[] { new Guid("0e698570-760b-48fa-9973-929005f50875"), true, "ROLE_CUSTOMER", new DateTime(2023, 4, 9, 10, 57, 16, 174, DateTimeKind.Local).AddTicks(467), "Customer", null });

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

            migrationBuilder.DropTable(
                name: "CUSTOMER_ROLE");

            migrationBuilder.DropTable(
                name: "DEVICE");

            migrationBuilder.DropTable(
                name: "MAIL_MESSAGE");
        }
    }
}

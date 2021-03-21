using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Raje.DAL.Migrations
{
    public partial class addUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "CityId", "CreatedAt", "CreatedBy", "Email", "FirstAccess", "FlagActive", "FullName", "LastGuidAuthentication", "ModifiedAt", "ModifiedBy", "PasswordHash", "RefreshToken", "StateId", "UserName", "UserRoleId" },
                values: new object[,]
                {
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1303908L, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "aline_mimile@outlook.com", false, true, "Aline de Oliveira Soares", "7fc55b62-2c80-406b-a2b2-911ea7f9ba63", new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "AQAAAAEAACcQAAAAEO6i2KT0GaiqqBNQX27Oq5r5jIjTVvAUa8KErs+tv8ItcpNEIsiM/vm6gp6C05UHnA==", null, 35L, "19004346", 1L },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1303908L, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "ed-0307@outlook.com.br", false, true, "Edmilson Bispo Paes dos Santos", "7fc55b62-2c80-406b-a2b2-911ea7f9ba63", new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "AQAAAAEAACcQAAAAEO6i2KT0GaiqqBNQX27Oq5r5jIjTVvAUa8KErs+tv8ItcpNEIsiM/vm6gp6C05UHnA==", null, 35L, "19010291", 1L },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1303908L, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "joni@tedenium.com.br", false, true, "Joni Welter Ramos", "7fc55b62-2c80-406b-a2b2-911ea7f9ba63", new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "AQAAAAEAACcQAAAAEO6i2KT0GaiqqBNQX27Oq5r5jIjTVvAUa8KErs+tv8ItcpNEIsiM/vm6gp6C05UHnA==", null, 35L, "19005636", 1L },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1303908L, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "rigarcia09@icloud.com", false, true, "Rita de Cassia Duarte Garcia", "7fc55b62-2c80-406b-a2b2-911ea7f9ba63", new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seed Setup", "AQAAAAEAACcQAAAAEO6i2KT0GaiqqBNQX27Oq5r5jIjTVvAUa8KErs+tv8ItcpNEIsiM/vm6gp6C05UHnA==", null, 35L, "19000448", 1L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5L);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.EntityFrameworkCore.Migrations
{
    public partial class AdUsersMockData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("22aa09e5-c7f4-401f-985e-1b50a5e83a8a"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("378f205f-8933-4a98-9618-54d035b99309"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("8cc8dbe3-0383-45d7-9b04-cf6c9a75e0dc"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a9ddcb1c-5fc2-40eb-9ca2-1c00e3459e88"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("b6d76fa7-655b-4258-ac55-1712d5017789"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("cf1c7c45-8d87-4d6b-aa65-231762b52943"));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedBy", "DeletedAt", "DeletedBy", "Name", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("08a33569-aee5-446f-84e2-0784a17b493c"), null, null, null, "Apple Macbook", 2400m, null, null },
                    { new Guid("40d246d7-607c-40aa-8886-dc73b6b7a1c7"), null, null, null, "Logitech Mouse", 80m, null, null },
                    { new Guid("4f4b4b42-bfa9-48f4-a3f0-c05edd74834d"), null, null, null, "Monster Notebook", 800m, null, null },
                    { new Guid("696252e2-bedb-4b6f-8f0f-b5b561ce4c9c"), null, null, null, "Sandisk Flash Drive", 40m, null, null },
                    { new Guid("9bd9d62a-68e5-41f1-8d17-429e5c38bfc3"), null, null, null, "Apple Airpod", 1200m, null, null },
                    { new Guid("cd9ca704-a689-40bd-8781-44ccfef93426"), null, null, null, "Logitech Keyboard", 120m, null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("c5dc4442-9361-46fb-87ae-19c5f4b10f43"), null, null, null, "user@mail.com", "Teoman", "Tıngır", "$2a$11$.MRL6qywoYpnruxA.Uo0deFr/2Psjh.JJI.9cesCtACteNkpEt836", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("08a33569-aee5-446f-84e2-0784a17b493c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("40d246d7-607c-40aa-8886-dc73b6b7a1c7"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("4f4b4b42-bfa9-48f4-a3f0-c05edd74834d"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("696252e2-bedb-4b6f-8f0f-b5b561ce4c9c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9bd9d62a-68e5-41f1-8d17-429e5c38bfc3"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("cd9ca704-a689-40bd-8781-44ccfef93426"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("c5dc4442-9361-46fb-87ae-19c5f4b10f43"));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Name", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("22aa09e5-c7f4-401f-985e-1b50a5e83a8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Apple Airpod", 1200m, null, null },
                    { new Guid("378f205f-8933-4a98-9618-54d035b99309"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Monster Notebook", 800m, null, null },
                    { new Guid("8cc8dbe3-0383-45d7-9b04-cf6c9a75e0dc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Logitech Mouse", 80m, null, null },
                    { new Guid("a9ddcb1c-5fc2-40eb-9ca2-1c00e3459e88"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Apple Macbook", 2400m, null, null },
                    { new Guid("b6d76fa7-655b-4258-ac55-1712d5017789"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Logitech Keyboard", 120m, null, null },
                    { new Guid("cf1c7c45-8d87-4d6b-aa65-231762b52943"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Sandisk Flash Drive", 40m, null, null }
                });
        }
    }
}

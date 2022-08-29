using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.EntityFrameworkCore.Migrations
{
    public partial class AddProductsMockData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedBy", "DeletedAt", "DeletedBy", "Name", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("22aa09e5-c7f4-401f-985e-1b50a5e83a8a"), null, null, null, "Apple Airpod", 1200m, null, null },
                    { new Guid("378f205f-8933-4a98-9618-54d035b99309"), null, null, null, "Monster Notebook", 800m, null, null },
                    { new Guid("8cc8dbe3-0383-45d7-9b04-cf6c9a75e0dc"), null, null, null, "Logitech Mouse", 80m, null, null },
                    { new Guid("a9ddcb1c-5fc2-40eb-9ca2-1c00e3459e88"), null, null, null, "Apple Macbook", 2400m, null, null },
                    { new Guid("b6d76fa7-655b-4258-ac55-1712d5017789"), null, null, null, "Logitech Keyboard", 120m, null, null },
                    { new Guid("cf1c7c45-8d87-4d6b-aa65-231762b52943"), null, null, null, "Sandisk Flash Drive", 40m, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

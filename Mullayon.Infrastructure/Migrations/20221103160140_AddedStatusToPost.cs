using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mullayon.Infrastructure.Migrations
{
    public partial class AddedStatusToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2988e309-7890-4db1-9746-e0426ff4f1cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "839c31b5-4adc-47e2-a069-f055742c613a");

            migrationBuilder.AddColumn<int>(
                name: "PublishStatus",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubmissionStatus",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "3ae682fe-96fc-44e4-bb2d-c9b6b82c5e42", "49924ef0-fa27-4b17-a096-f6a585b088ec", "ApplicationRole", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "b0af6ffb-7f1a-49a8-98f1-54c20b38b555", "6fa603b3-193a-480e-a106-e2ff0f1aeba4", "ApplicationRole", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ae682fe-96fc-44e4-bb2d-c9b6b82c5e42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0af6ffb-7f1a-49a8-98f1-54c20b38b555");

            migrationBuilder.DropColumn(
                name: "PublishStatus",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubmissionStatus",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2988e309-7890-4db1-9746-e0426ff4f1cc", "0ca72a16-70b9-4525-93bf-67c0fdc08876", "ApplicationRole", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "839c31b5-4adc-47e2-a069-f055742c613a", "80a278a2-16f9-44cb-a4ac-078bea29fed6", "ApplicationRole", "User", "USER" });
        }
    }
}

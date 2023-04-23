using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingListWebApp.Migrations
{
    public partial class AddedSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Food" });

            migrationBuilder.InsertData(
                table: "ShopItems",
                columns: new[] { "Id", "CategoryId", "Name", "Quantity", "UnitPrice" },
                values: new object[] { 1L, 1, "Tomaat", 4, 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShopItems",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

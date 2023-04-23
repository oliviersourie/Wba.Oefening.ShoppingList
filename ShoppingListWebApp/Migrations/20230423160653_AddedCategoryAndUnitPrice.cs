using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingListWebApp.Migrations
{
    public partial class AddedCategoryAndUnitPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShopItems",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ShopItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "ShopItems",
                type: "decimal(2)",
                precision: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_CategoryId",
                table: "ShopItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Categories_CategoryId",
                table: "ShopItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Categories_CategoryId",
                table: "ShopItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_ShopItems_CategoryId",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ShopItems");

            migrationBuilder.InsertData(
                table: "ShopItems",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[] { 1L, "Tomaat", 4 });
        }
    }
}

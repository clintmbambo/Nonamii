using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nonamii.Data.Migrations
{
    /// <inheritdoc />
    public partial class Menu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_MenuItems_MenuItemId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_MenuItemId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MenuItemId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "MenuItems");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_MenuType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MenuType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuCategory_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MenuItemId",
                table: "Recipes",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_TypeId",
                table: "Menu",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategory_CategoryId",
                table: "MenuCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategory_MenuId",
                table: "MenuCategory",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_MenuItems_MenuItemId",
                table: "Recipes",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_MenuItems_MenuItemId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "MenuCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuType");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_MenuItemId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId1",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MenuItemId1",
                table: "Recipes",
                column: "MenuItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_MenuItems_MenuItemId1",
                table: "Recipes",
                column: "MenuItemId1",
                principalTable: "MenuItems",
                principalColumn: "Id");
        }
    }
}

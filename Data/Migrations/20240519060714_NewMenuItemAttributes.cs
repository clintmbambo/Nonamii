using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nonamii.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMenuItemAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_MenuItems_MenuItemId",
                table: "Recipes");

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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MenuItems");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MenuItemId",
                table: "Recipes",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_MenuItems_MenuItemId",
                table: "Recipes",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

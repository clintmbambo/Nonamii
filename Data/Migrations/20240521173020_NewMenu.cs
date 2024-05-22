using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nonamii.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MenuType",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MenuType");
        }
    }
}

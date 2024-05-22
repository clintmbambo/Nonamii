using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nonamii.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCartViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrdersDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CartsDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrdersDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CartsDetails");
        }
    }
}

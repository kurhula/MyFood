using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseLayer.Migrations
{
    public partial class AddQuantityStartsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarsQuantity",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "StartsTotal",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartsTotal",
                table: "Foods",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartsTotal",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "StartsTotal",
                table: "Foods");

            migrationBuilder.AddColumn<int>(
                name: "StarsQuantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

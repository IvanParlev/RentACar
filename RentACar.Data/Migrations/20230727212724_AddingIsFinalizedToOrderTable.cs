using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    public partial class AddingIsFinalizedToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "Orders");
        }
    }
}

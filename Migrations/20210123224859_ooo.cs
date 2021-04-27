using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaApi.Migrations
{
    public partial class ooo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoryname",
                table: "Tv_Shows",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoryname",
                table: "Tv_Shows");
        }
    }
}

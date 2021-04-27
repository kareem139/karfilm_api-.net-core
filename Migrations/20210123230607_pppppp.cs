using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaApi.Migrations
{
    public partial class pppppp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowsTv_Show");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Tv_Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Episode",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tvshowname",
                table: "Shows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Shows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tv_ShowsId",
                table: "Shows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shows_tv_ShowsId",
                table: "Shows",
                column: "tv_ShowsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Tv_Shows_tv_ShowsId",
                table: "Shows",
                column: "tv_ShowsId",
                principalTable: "Tv_Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Tv_Shows_tv_ShowsId",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_tv_ShowsId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Tv_Shows");

            migrationBuilder.DropColumn(
                name: "Episode",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "Tvshowname",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "tv_ShowsId",
                table: "Shows");

            migrationBuilder.CreateTable(
                name: "ShowsTv_Show",
                columns: table => new
                {
                    showsId = table.Column<int>(type: "int", nullable: false),
                    tv_ShowsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsTv_Show", x => new { x.showsId, x.tv_ShowsId });
                    table.ForeignKey(
                        name: "FK_ShowsTv_Show_Shows_showsId",
                        column: x => x.showsId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowsTv_Show_Tv_Shows_tv_ShowsId",
                        column: x => x.tv_ShowsId,
                        principalTable: "Tv_Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowsTv_Show_tv_ShowsId",
                table: "ShowsTv_Show",
                column: "tv_ShowsId");
        }
    }
}

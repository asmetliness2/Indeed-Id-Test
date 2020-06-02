using Microsoft.EntityFrameworkCore.Migrations;

namespace PurseApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurseCurrencies",
                columns: table => new
                {
                    PurseId = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: false),
                    Sum = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurseCurrencies", x => new { x.PurseId, x.Currency });
                    table.ForeignKey(
                        name: "FK_PurseCurrencies_Purses_PurseId",
                        column: x => x.PurseId,
                        principalTable: "Purses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurseCurrencies");

            migrationBuilder.DropTable(
                name: "Purses");
        }
    }
}

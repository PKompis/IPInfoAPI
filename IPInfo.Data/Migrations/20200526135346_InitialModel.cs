using Microsoft.EntityFrameworkCore.Migrations;

namespace IPInfo.Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IP",
                columns: table => new
                {
                    Ip = table.Column<string>(maxLength: 100, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    Continent = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IP", x => x.Ip);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IP");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore_web.Migrations
{
    public partial class popustAktivan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aktivan",
                schema: "dbo",
                table: "Popust",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktivan",
                schema: "dbo",
                table: "Popust");
        }
    }
}

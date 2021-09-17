using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore_web.Migrations
{
    public partial class LinkIgre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrailerUrl",
                schema: "dbo",
                table: "Igra",
                newName: "LinkIgre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkIgre",
                schema: "dbo",
                table: "Igra",
                newName: "TrailerUrl");
        }
    }
}

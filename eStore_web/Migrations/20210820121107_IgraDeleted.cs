using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore_web.Migrations
{
    public partial class IgraDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                schema: "dbo",
                table: "Igra",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                schema: "dbo",
                table: "Igra");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore_web.Migrations
{
    public partial class patchAccountBaseDev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developer_AccountBase_AccountBaseID",
                schema: "dbo",
                table: "Developer");

            migrationBuilder.RenameColumn(
                name: "AccountBaseID",
                schema: "dbo",
                table: "Developer",
                newName: "BaseID");

            migrationBuilder.RenameIndex(
                name: "IX_Developer_AccountBaseID",
                schema: "dbo",
                table: "Developer",
                newName: "IX_Developer_BaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Developer_AccountBase_BaseID",
                schema: "dbo",
                table: "Developer",
                column: "BaseID",
                principalSchema: "dbo",
                principalTable: "AccountBase",
                principalColumn: "AccountBaseID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developer_AccountBase_BaseID",
                schema: "dbo",
                table: "Developer");

            migrationBuilder.RenameColumn(
                name: "BaseID",
                schema: "dbo",
                table: "Developer",
                newName: "AccountBaseID");

            migrationBuilder.RenameIndex(
                name: "IX_Developer_BaseID",
                schema: "dbo",
                table: "Developer",
                newName: "IX_Developer_AccountBaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Developer_AccountBase_AccountBaseID",
                schema: "dbo",
                table: "Developer",
                column: "AccountBaseID",
                principalSchema: "dbo",
                principalTable: "AccountBase",
                principalColumn: "AccountBaseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

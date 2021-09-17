using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore_web.Migrations
{
    public partial class AuthToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutentifikacijaToken",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vrijednost = table.Column<string>(nullable: true),
                    KorisnickiNalogId = table.Column<int>(nullable: false),
                    VrijemeEvidentiranja = table.Column<DateTime>(nullable: false),
                    IpAdresa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutentifikacijaToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutentifikacijaToken_LoginInfo_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalSchema: "dbo",
                        principalTable: "LoginInfo",
                        principalColumn: "LoginInfoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_KorisnickiNalogId",
                schema: "dbo",
                table: "AutentifikacijaToken",
                column: "KorisnickiNalogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutentifikacijaToken",
                schema: "dbo");
        }
    }
}

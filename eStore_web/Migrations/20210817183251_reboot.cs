using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore_web.Migrations
{
    public partial class reboot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Drzava",
                schema: "dbo",
                columns: table => new
                {
                    DrzavaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Kratica = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Iso = table.Column<string>(nullable: true),
                    Iso3 = table.Column<string>(nullable: true),
                    NumCode = table.Column<string>(nullable: true),
                    PhoneCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.DrzavaID);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                schema: "dbo",
                columns: table => new
                {
                    GameGenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivZanra = table.Column<string>(maxLength: 50, nullable: false),
                    OznakaZanra = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => x.GameGenreID);
                });

            migrationBuilder.CreateTable(
                name: "IgricaImage",
                schema: "dbo",
                columns: table => new
                {
                    IgricaImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IgraID = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgricaImage", x => x.IgricaImageID);
                });

            migrationBuilder.CreateTable(
                name: "RatingCategorie",
                schema: "dbo",
                columns: table => new
                {
                    RatingCategorieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivKategorije = table.Column<string>(maxLength: 50, nullable: false),
                    OznakaKategorije = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCategorie", x => x.RatingCategorieID);
                });

            migrationBuilder.CreateTable(
                name: "AccountBase",
                schema: "dbo",
                columns: table => new
                {
                    AccountBaseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoginInfoID = table.Column<int>(nullable: true),
                    EmailAddressID = table.Column<int>(nullable: true),
                    ImageID = table.Column<int>(nullable: true),
                    DrzavaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBase", x => x.AccountBaseID);
                    table.ForeignKey(
                        name: "FK_AccountBase_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalSchema: "dbo",
                        principalTable: "Drzava",
                        principalColumn: "DrzavaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountImages",
                schema: "dbo",
                columns: table => new
                {
                    AccountImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseID = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountImages", x => x.AccountImageID);
                    table.ForeignKey(
                        name: "FK_AccountImages_AccountBase_BaseID",
                        column: x => x.BaseID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                schema: "dbo",
                columns: table => new
                {
                    AdministratorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(maxLength: 50, nullable: true),
                    DatumRodenja = table.Column<DateTime>(type: "Date", nullable: false),
                    BaseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdministratorID);
                    table.ForeignKey(
                        name: "FK_Administrator_AccountBase_BaseID",
                        column: x => x.BaseID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Developer",
                schema: "dbo",
                columns: table => new
                {
                    DeveloperID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivKompanije = table.Column<string>(maxLength: 50, nullable: true),
                    DatumUtemeljenja = table.Column<DateTime>(nullable: false),
                    LokacijaAdresa = table.Column<string>(nullable: true),
                    Banovan = table.Column<bool>(nullable: false),
                    AccountBaseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.DeveloperID);
                    table.ForeignKey(
                        name: "FK_Developer_AccountBase_AccountBaseID",
                        column: x => x.AccountBaseID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddress",
                schema: "dbo",
                columns: table => new
                {
                    EmailAddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    BaseID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddress", x => x.EmailAddressID);
                    table.ForeignKey(
                        name: "FK_EmailAddress_AccountBase_BaseID",
                        column: x => x.BaseID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kupac",
                schema: "dbo",
                columns: table => new
                {
                    KupacID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 25, nullable: false),
                    PretplacenNaPremium = table.Column<bool>(nullable: false),
                    Reputacija = table.Column<int>(nullable: false),
                    BanStatus = table.Column<bool>(nullable: false),
                    BaseID = table.Column<int>(nullable: false),
                    WalletID = table.Column<int>(nullable: true),
                    Ime = table.Column<string>(maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(maxLength: 50, nullable: true),
                    DatumRodenja = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupac", x => x.KupacID);
                    table.ForeignKey(
                        name: "FK_Kupac_AccountBase_BaseID",
                        column: x => x.BaseID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginInfo",
                schema: "dbo",
                columns: table => new
                {
                    LoginInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    TipKorisnika = table.Column<int>(nullable: false),
                    BaseID = table.Column<int>(nullable: true),
                    SignalRToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginInfo", x => x.LoginInfoID);
                    table.ForeignKey(
                        name: "FK_LoginInfo_AccountBase_BaseID",
                        column: x => x.BaseID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifikacija",
                schema: "dbo",
                columns: table => new
                {
                    NotifikacijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vrijeme = table.Column<DateTime>(nullable: false),
                    Poruka = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    UserToID = table.Column<int>(nullable: true),
                    UserFromID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacija", x => x.NotifikacijaID);
                    table.ForeignKey(
                        name: "FK_Notifikacija_AccountBase_UserFromID",
                        column: x => x.UserFromID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifikacija_AccountBase_UserToID",
                        column: x => x.UserToID,
                        principalSchema: "dbo",
                        principalTable: "AccountBase",
                        principalColumn: "AccountBaseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Igra",
                schema: "dbo",
                columns: table => new
                {
                    IgraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Opis = table.Column<string>(maxLength: 1048, nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "Date", nullable: false),
                    TrailerUrl = table.Column<string>(maxLength: 1048, nullable: false),
                    PremiumStatus = table.Column<bool>(nullable: false),
                    Odobrena = table.Column<bool>(nullable: false),
                    GameGenreID = table.Column<int>(nullable: true),
                    RatingCategorieID = table.Column<int>(nullable: true),
                    DeveloperID = table.Column<int>(nullable: true),
                    AdministratorID = table.Column<int>(nullable: true),
                    IgricaImageID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igra", x => x.IgraID);
                    table.ForeignKey(
                        name: "FK_Igra_Administrator_AdministratorID",
                        column: x => x.AdministratorID,
                        principalSchema: "dbo",
                        principalTable: "Administrator",
                        principalColumn: "AdministratorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igra_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalSchema: "dbo",
                        principalTable: "Developer",
                        principalColumn: "DeveloperID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igra_GameGenre_GameGenreID",
                        column: x => x.GameGenreID,
                        principalSchema: "dbo",
                        principalTable: "GameGenre",
                        principalColumn: "GameGenreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igra_IgricaImage_IgricaImageID",
                        column: x => x.IgricaImageID,
                        principalSchema: "dbo",
                        principalTable: "IgricaImage",
                        principalColumn: "IgricaImageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igra_RatingCategorie_RatingCategorieID",
                        column: x => x.RatingCategorieID,
                        principalSchema: "dbo",
                        principalTable: "RatingCategorie",
                        principalColumn: "RatingCategorieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrijavaPremium",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KupacId = table.Column<int>(nullable: false),
                    DatumPrijave = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaPremium", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrijavaPremium_Kupac_KupacId",
                        column: x => x.KupacId,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RazlogReporta = table.Column<string>(nullable: true),
                    ReportovaniKupacId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Kupac_ReportovaniKupacId",
                        column: x => x.ReportovaniKupacId,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                schema: "dbo",
                columns: table => new
                {
                    WalletID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    balance = table.Column<double>(nullable: false),
                    KupacID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.WalletID);
                    table.ForeignKey(
                        name: "FK_Wallet_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Novost",
                schema: "dbo",
                columns: table => new
                {
                    NovostID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naslov = table.Column<string>(maxLength: 50, nullable: false),
                    Sadrzaj = table.Column<string>(maxLength: 2048, nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DeveloperID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novost", x => x.NovostID);
                    table.ForeignKey(
                        name: "FK_Novost_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalSchema: "dbo",
                        principalTable: "Developer",
                        principalColumn: "DeveloperID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Novost_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Popust",
                schema: "dbo",
                columns: table => new
                {
                    PopustID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IgraID = table.Column<int>(nullable: false),
                    PocetakPopusta = table.Column<DateTime>(type: "DateTime", nullable: false),
                    KrajPopusta = table.Column<DateTime>(type: "DateTime", nullable: false),
                    PopustProcent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Popust", x => x.PopustID);
                    table.ForeignKey(
                        name: "FK_Popust_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prati",
                schema: "dbo",
                columns: table => new
                {
                    PratiID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prati", x => x.PratiID);
                    table.ForeignKey(
                        name: "FK_Prati_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prati_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreuzimanjeIgre",
                schema: "dbo",
                columns: table => new
                {
                    PreuzimanjeIgreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrijemePreuzimanja = table.Column<DateTime>(type: "DateTime", nullable: false),
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreuzimanjeIgre", x => x.PreuzimanjeIgreID);
                    table.ForeignKey(
                        name: "FK_PreuzimanjeIgre_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreuzimanjeIgre_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                schema: "dbo",
                columns: table => new
                {
                    RecenzijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    RecenzijaText = table.Column<string>(maxLength: 1024, nullable: false),
                    Ocjena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.RecenzijaID);
                    table.ForeignKey(
                        name: "FK_Recenzija_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzija_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                schema: "dbo",
                columns: table => new
                {
                    RefundID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrijemeKupovine = table.Column<DateTime>(type: "DateTime", nullable: false),
                    VrijemeZahtijeva = table.Column<DateTime>(type: "DateTime", nullable: false),
                    VrijemeOdgovora = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RazlogRefunda = table.Column<string>(maxLength: 50, nullable: true),
                    IgraID = table.Column<int>(nullable: true),
                    KupacID = table.Column<int>(nullable: true),
                    AdministratorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.RefundID);
                    table.ForeignKey(
                        name: "FK_Refund_Administrator_AdministratorID",
                        column: x => x.AdministratorID,
                        principalSchema: "dbo",
                        principalTable: "Administrator",
                        principalColumn: "AdministratorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refund_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refund_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                schema: "dbo",
                columns: table => new
                {
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    DatumDodavanja = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => new { x.KupacID, x.IgraID });
                    table.ForeignKey(
                        name: "FK_WishList_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishList_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistory",
                schema: "dbo",
                columns: table => new
                {
                    WalletHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentBalance = table.Column<double>(nullable: false),
                    TransactionAmount = table.Column<double>(nullable: false),
                    IsIgra = table.Column<bool>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    WalletID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistory", x => x.WalletHistoryID);
                    table.ForeignKey(
                        name: "FK_WalletHistory_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletHistory_Wallet_WalletID",
                        column: x => x.WalletID,
                        principalSchema: "dbo",
                        principalTable: "Wallet",
                        principalColumn: "WalletID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KupacKupuje",
                schema: "dbo",
                columns: table => new
                {
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    PopustID = table.Column<int>(nullable: true),
                    VrijemeKupovine = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Cijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KupacKupuje", x => new { x.KupacID, x.IgraID });
                    table.ForeignKey(
                        name: "FK_KupacKupuje_Igra_IgraID",
                        column: x => x.IgraID,
                        principalSchema: "dbo",
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KupacKupuje_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KupacKupuje_Popust_PopustID",
                        column: x => x.PopustID,
                        principalSchema: "dbo",
                        principalTable: "Popust",
                        principalColumn: "PopustID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrijavaRecenzije",
                schema: "dbo",
                columns: table => new
                {
                    PrijavaRecenzijeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Razlog = table.Column<string>(maxLength: 50, nullable: false),
                    RecenzijaID = table.Column<int>(nullable: false),
                    KupacID = table.Column<int>(nullable: true),
                    AdministratorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaRecenzije", x => x.PrijavaRecenzijeID);
                    table.ForeignKey(
                        name: "FK_PrijavaRecenzije_Administrator_AdministratorID",
                        column: x => x.AdministratorID,
                        principalSchema: "dbo",
                        principalTable: "Administrator",
                        principalColumn: "AdministratorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrijavaRecenzije_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalSchema: "dbo",
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrijavaRecenzije_Recenzija_RecenzijaID",
                        column: x => x.RecenzijaID,
                        principalSchema: "dbo",
                        principalTable: "Recenzija",
                        principalColumn: "RecenzijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBase_DrzavaID",
                schema: "dbo",
                table: "AccountBase",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountImages_BaseID",
                schema: "dbo",
                table: "AccountImages",
                column: "BaseID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_BaseID",
                schema: "dbo",
                table: "Administrator",
                column: "BaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Developer_AccountBaseID",
                schema: "dbo",
                table: "Developer",
                column: "AccountBaseID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddress_BaseID",
                schema: "dbo",
                table: "EmailAddress",
                column: "BaseID",
                unique: true,
                filter: "[BaseID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_AdministratorID",
                schema: "dbo",
                table: "Igra",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_DeveloperID",
                schema: "dbo",
                table: "Igra",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_GameGenreID",
                schema: "dbo",
                table: "Igra",
                column: "GameGenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_IgricaImageID",
                schema: "dbo",
                table: "Igra",
                column: "IgricaImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_RatingCategorieID",
                schema: "dbo",
                table: "Igra",
                column: "RatingCategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_IgricaImage_IgraID",
                schema: "dbo",
                table: "IgricaImage",
                column: "IgraID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kupac_BaseID",
                schema: "dbo",
                table: "Kupac",
                column: "BaseID");

            migrationBuilder.CreateIndex(
                name: "IX_KupacKupuje_IgraID",
                schema: "dbo",
                table: "KupacKupuje",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_KupacKupuje_PopustID",
                schema: "dbo",
                table: "KupacKupuje",
                column: "PopustID");

            migrationBuilder.CreateIndex(
                name: "IX_LoginInfo_BaseID",
                schema: "dbo",
                table: "LoginInfo",
                column: "BaseID",
                unique: true,
                filter: "[BaseID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacija_UserFromID",
                schema: "dbo",
                table: "Notifikacija",
                column: "UserFromID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacija_UserToID",
                schema: "dbo",
                table: "Notifikacija",
                column: "UserToID");

            migrationBuilder.CreateIndex(
                name: "IX_Novost_DeveloperID",
                schema: "dbo",
                table: "Novost",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Novost_IgraID",
                schema: "dbo",
                table: "Novost",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Popust_IgraID",
                schema: "dbo",
                table: "Popust",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Prati_IgraID",
                schema: "dbo",
                table: "Prati",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Prati_KupacID",
                schema: "dbo",
                table: "Prati",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_PreuzimanjeIgre_IgraID",
                schema: "dbo",
                table: "PreuzimanjeIgre",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_PreuzimanjeIgre_KupacID",
                schema: "dbo",
                table: "PreuzimanjeIgre",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaPremium_KupacId",
                schema: "dbo",
                table: "PrijavaPremium",
                column: "KupacId");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaRecenzije_AdministratorID",
                schema: "dbo",
                table: "PrijavaRecenzije",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaRecenzije_KupacID",
                schema: "dbo",
                table: "PrijavaRecenzije",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaRecenzije_RecenzijaID",
                schema: "dbo",
                table: "PrijavaRecenzije",
                column: "RecenzijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_IgraID",
                schema: "dbo",
                table: "Recenzija",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KupacID",
                schema: "dbo",
                table: "Recenzija",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_AdministratorID",
                schema: "dbo",
                table: "Refund",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_IgraID",
                schema: "dbo",
                table: "Refund",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_KupacID",
                schema: "dbo",
                table: "Refund",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReportovaniKupacId",
                schema: "dbo",
                table: "Report",
                column: "ReportovaniKupacId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_KupacID",
                schema: "dbo",
                table: "Wallet",
                column: "KupacID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistory_IgraID",
                schema: "dbo",
                table: "WalletHistory",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistory_WalletID",
                schema: "dbo",
                table: "WalletHistory",
                column: "WalletID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_IgraID",
                schema: "dbo",
                table: "WishList",
                column: "IgraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountImages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EmailAddress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "KupacKupuje",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoginInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Notifikacija",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Novost",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Prati",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PreuzimanjeIgre",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrijavaPremium",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrijavaRecenzije",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Refund",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Report",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WalletHistory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WishList",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Popust",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Recenzija",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Wallet",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Igra",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Kupac",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Administrator",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Developer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GameGenre",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IgricaImage",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RatingCategorie",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AccountBase",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Drzava",
                schema: "dbo");
        }
    }
}

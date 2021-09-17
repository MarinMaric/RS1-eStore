﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eStore_web.EF;

namespace eStore_web.Migrations
{
    [DbContext(typeof(eContext))]
    [Migration("20210817204010_patchAccountBaseDev")]
    partial class patchAccountBaseDev
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eStore_web.EF_Models.AccountBase", b =>
                {
                    b.Property<int>("AccountBaseID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrzavaID");

                    b.Property<int?>("EmailAddressID");

                    b.Property<int?>("ImageID");

                    b.Property<int?>("LoginInfoID");

                    b.HasKey("AccountBaseID");

                    b.HasIndex("DrzavaID");

                    b.ToTable("AccountBase");
                });

            modelBuilder.Entity("eStore_web.EF_Models.AccountImage", b =>
                {
                    b.Property<int>("AccountImageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseID");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("AccountImageID");

                    b.HasIndex("BaseID")
                        .IsUnique();

                    b.ToTable("AccountImages");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Administrator", b =>
                {
                    b.Property<int>("AdministratorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseID");

                    b.Property<DateTime>("DatumRodenja")
                        .HasColumnType("Date");

                    b.Property<string>("Ime")
                        .HasMaxLength(50);

                    b.Property<string>("Prezime")
                        .HasMaxLength(50);

                    b.HasKey("AdministratorID");

                    b.HasIndex("BaseID");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Developer", b =>
                {
                    b.Property<int>("DeveloperID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Banovan");

                    b.Property<int>("BaseID");

                    b.Property<DateTime>("DatumUtemeljenja");

                    b.Property<string>("LokacijaAdresa");

                    b.Property<string>("NazivKompanije")
                        .HasMaxLength(50);

                    b.HasKey("DeveloperID");

                    b.HasIndex("BaseID");

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Drzava", b =>
                {
                    b.Property<int>("DrzavaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Iso");

                    b.Property<string>("Iso3");

                    b.Property<string>("Kratica");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NumCode");

                    b.Property<string>("PhoneCode");

                    b.HasKey("DrzavaID");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("eStore_web.EF_Models.EmailAddress", b =>
                {
                    b.Property<int>("EmailAddressID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BaseID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("EmailAddressID");

                    b.HasIndex("BaseID")
                        .IsUnique()
                        .HasFilter("[BaseID] IS NOT NULL");

                    b.ToTable("EmailAddress");
                });

            modelBuilder.Entity("eStore_web.EF_Models.GameGenre", b =>
                {
                    b.Property<int>("GameGenreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivZanra")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OznakaZanra")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("GameGenreID");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Igra", b =>
                {
                    b.Property<int>("IgraID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdministratorID");

                    b.Property<float>("Cijena");

                    b.Property<DateTime>("DatumObjave")
                        .HasColumnType("Date");

                    b.Property<int?>("DeveloperID");

                    b.Property<int?>("GameGenreID");

                    b.Property<int?>("IgricaImageID");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("Odobrena");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(1048);

                    b.Property<bool>("PremiumStatus");

                    b.Property<int?>("RatingCategorieID");

                    b.Property<string>("TrailerUrl")
                        .IsRequired()
                        .HasMaxLength(1048);

                    b.HasKey("IgraID");

                    b.HasIndex("AdministratorID");

                    b.HasIndex("DeveloperID");

                    b.HasIndex("GameGenreID");

                    b.HasIndex("IgricaImageID");

                    b.HasIndex("RatingCategorieID");

                    b.ToTable("Igra");
                });

            modelBuilder.Entity("eStore_web.EF_Models.IgricaImage", b =>
                {
                    b.Property<int>("IgricaImageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IgraID");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("IgricaImageID");

                    b.HasIndex("IgraID")
                        .IsUnique();

                    b.ToTable("IgricaImage");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Kupac", b =>
                {
                    b.Property<int>("KupacID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BanStatus");

                    b.Property<int>("BaseID");

                    b.Property<DateTime>("DatumRodenja")
                        .HasColumnType("Date");

                    b.Property<string>("Ime")
                        .HasMaxLength(50);

                    b.Property<bool>("PretplacenNaPremium");

                    b.Property<string>("Prezime")
                        .HasMaxLength(50);

                    b.Property<int>("Reputacija");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int?>("WalletID");

                    b.HasKey("KupacID");

                    b.HasIndex("BaseID");

                    b.ToTable("Kupac");
                });

            modelBuilder.Entity("eStore_web.EF_Models.KupacKupuje", b =>
                {
                    b.Property<int>("KupacID");

                    b.Property<int>("IgraID");

                    b.Property<float>("Cijena");

                    b.Property<int?>("PopustID");

                    b.Property<DateTime>("VrijemeKupovine")
                        .HasColumnType("DateTime");

                    b.HasKey("KupacID", "IgraID");

                    b.HasIndex("IgraID");

                    b.HasIndex("PopustID");

                    b.ToTable("KupacKupuje");
                });

            modelBuilder.Entity("eStore_web.EF_Models.LoginInfo", b =>
                {
                    b.Property<int>("LoginInfoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int?>("BaseID");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("SignalRToken");

                    b.Property<int>("TipKorisnika");

                    b.HasKey("LoginInfoID");

                    b.HasIndex("BaseID")
                        .IsUnique()
                        .HasFilter("[BaseID] IS NOT NULL");

                    b.ToTable("LoginInfo");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Notifikacije", b =>
                {
                    b.Property<int>("NotifikacijaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Poruka");

                    b.Property<string>("URL");

                    b.Property<int?>("UserFromID");

                    b.Property<int?>("UserToID");

                    b.Property<DateTime>("Vrijeme");

                    b.HasKey("NotifikacijaID");

                    b.HasIndex("UserFromID");

                    b.HasIndex("UserToID");

                    b.ToTable("Notifikacija");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Novost", b =>
                {
                    b.Property<int>("NovostID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumObjave")
                        .HasColumnType("DateTime");

                    b.Property<int>("DeveloperID");

                    b.Property<int>("IgraID");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasMaxLength(2048);

                    b.HasKey("NovostID");

                    b.HasIndex("DeveloperID");

                    b.HasIndex("IgraID");

                    b.ToTable("Novost");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Popust", b =>
                {
                    b.Property<int>("PopustID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IgraID");

                    b.Property<DateTime>("KrajPopusta")
                        .HasColumnType("DateTime");

                    b.Property<DateTime>("PocetakPopusta")
                        .HasColumnType("DateTime");

                    b.Property<int>("PopustProcent");

                    b.HasKey("PopustID");

                    b.HasIndex("IgraID");

                    b.ToTable("Popust");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Prati", b =>
                {
                    b.Property<int>("PratiID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IgraID");

                    b.Property<int>("KupacID");

                    b.HasKey("PratiID");

                    b.HasIndex("IgraID");

                    b.HasIndex("KupacID");

                    b.ToTable("Prati");
                });

            modelBuilder.Entity("eStore_web.EF_Models.PreuzimanjeIgre", b =>
                {
                    b.Property<int>("PreuzimanjeIgreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IgraID");

                    b.Property<int>("KupacID");

                    b.Property<DateTime>("VrijemePreuzimanja")
                        .HasColumnType("DateTime");

                    b.HasKey("PreuzimanjeIgreID");

                    b.HasIndex("IgraID");

                    b.HasIndex("KupacID");

                    b.ToTable("PreuzimanjeIgre");
                });

            modelBuilder.Entity("eStore_web.EF_Models.PrijavaPremium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumPrijave");

                    b.Property<int>("KupacId");

                    b.HasKey("Id");

                    b.HasIndex("KupacId");

                    b.ToTable("PrijavaPremium");
                });

            modelBuilder.Entity("eStore_web.EF_Models.PrijavaRecenzije", b =>
                {
                    b.Property<int>("PrijavaRecenzijeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdministratorID");

                    b.Property<int?>("KupacID");

                    b.Property<string>("Razlog")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("RecenzijaID");

                    b.HasKey("PrijavaRecenzijeID");

                    b.HasIndex("AdministratorID");

                    b.HasIndex("KupacID");

                    b.HasIndex("RecenzijaID");

                    b.ToTable("PrijavaRecenzije");
                });

            modelBuilder.Entity("eStore_web.EF_Models.RatingCategorie", b =>
                {
                    b.Property<int>("RatingCategorieID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivKategorije")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OznakaKategorije")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("RatingCategorieID");

                    b.ToTable("RatingCategorie");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Recenzija", b =>
                {
                    b.Property<int>("RecenzijaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IgraID");

                    b.Property<int>("KupacID");

                    b.Property<int>("Ocjena");

                    b.Property<string>("RecenzijaText")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.HasKey("RecenzijaID");

                    b.HasIndex("IgraID");

                    b.HasIndex("KupacID");

                    b.ToTable("Recenzija");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Refund", b =>
                {
                    b.Property<int>("RefundID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdministratorID");

                    b.Property<int?>("IgraID");

                    b.Property<int?>("KupacID");

                    b.Property<string>("RazlogRefunda")
                        .HasMaxLength(50);

                    b.Property<DateTime>("VrijemeKupovine")
                        .HasColumnType("DateTime");

                    b.Property<DateTime>("VrijemeOdgovora")
                        .HasColumnType("DateTime");

                    b.Property<DateTime>("VrijemeZahtijeva")
                        .HasColumnType("DateTime");

                    b.HasKey("RefundID");

                    b.HasIndex("AdministratorID");

                    b.HasIndex("IgraID");

                    b.HasIndex("KupacID");

                    b.ToTable("Refund");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RazlogReporta");

                    b.Property<int>("ReportovaniKupacId");

                    b.HasKey("Id");

                    b.HasIndex("ReportovaniKupacId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Wallet", b =>
                {
                    b.Property<int>("WalletID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KupacID");

                    b.Property<double>("balance");

                    b.HasKey("WalletID");

                    b.HasIndex("KupacID")
                        .IsUnique();

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("eStore_web.EF_Models.WalletHistory", b =>
                {
                    b.Property<int>("WalletHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CurrentBalance");

                    b.Property<int>("IgraID");

                    b.Property<bool>("IsIgra");

                    b.Property<double>("TransactionAmount");

                    b.Property<int?>("WalletID");

                    b.HasKey("WalletHistoryID");

                    b.HasIndex("IgraID");

                    b.HasIndex("WalletID");

                    b.ToTable("WalletHistory");
                });

            modelBuilder.Entity("eStore_web.EF_Models.WishList", b =>
                {
                    b.Property<int>("KupacID");

                    b.Property<int>("IgraID");

                    b.Property<DateTime>("DatumDodavanja")
                        .HasColumnType("Date");

                    b.HasKey("KupacID", "IgraID");

                    b.HasIndex("IgraID");

                    b.ToTable("WishList");
                });

            modelBuilder.Entity("eStore_web.EF_Models.AccountBase", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("DrzavaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.AccountImage", b =>
                {
                    b.HasOne("eStore_web.EF_Models.AccountBase", "Base")
                        .WithOne("Image")
                        .HasForeignKey("eStore_web.EF_Models.AccountImage", "BaseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.Administrator", b =>
                {
                    b.HasOne("eStore_web.EF_Models.AccountBase", "AccountBase")
                        .WithMany()
                        .HasForeignKey("BaseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.Developer", b =>
                {
                    b.HasOne("eStore_web.EF_Models.AccountBase", "AccountBase")
                        .WithMany()
                        .HasForeignKey("BaseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.EmailAddress", b =>
                {
                    b.HasOne("eStore_web.EF_Models.AccountBase", "AccountBase")
                        .WithOne("EmailAddress")
                        .HasForeignKey("eStore_web.EF_Models.EmailAddress", "BaseID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Igra", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID");

                    b.HasOne("eStore_web.EF_Models.Developer", "Developer")
                        .WithMany("Igre")
                        .HasForeignKey("DeveloperID");

                    b.HasOne("eStore_web.EF_Models.GameGenre", "GameGenre")
                        .WithMany("Igra")
                        .HasForeignKey("GameGenreID");

                    b.HasOne("eStore_web.EF_Models.IgricaImage", "IgricaImage")
                        .WithMany()
                        .HasForeignKey("IgricaImageID");

                    b.HasOne("eStore_web.EF_Models.RatingCategorie", "RatingCategorie")
                        .WithMany("Igre")
                        .HasForeignKey("RatingCategorieID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Kupac", b =>
                {
                    b.HasOne("eStore_web.EF_Models.AccountBase", "AccountBase")
                        .WithMany()
                        .HasForeignKey("BaseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.KupacKupuje", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany("KupacKupuje")
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany("KupacKupuje")
                        .HasForeignKey("KupacID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Popust", "Popust")
                        .WithMany("KupacKupuje")
                        .HasForeignKey("PopustID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.LoginInfo", b =>
                {
                    b.HasOne("eStore_web.EF_Models.AccountBase", "AccountBase")
                        .WithOne("LoginInfo")
                        .HasForeignKey("eStore_web.EF_Models.LoginInfo", "BaseID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Notifikacije", b =>
                {
                    b.HasOne("eStore_web.EF_Models.AccountBase", "UserFrom")
                        .WithMany()
                        .HasForeignKey("UserFromID");

                    b.HasOne("eStore_web.EF_Models.AccountBase", "UserTo")
                        .WithMany()
                        .HasForeignKey("UserToID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Novost", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Developer", "Developer")
                        .WithMany("Novosti")
                        .HasForeignKey("DeveloperID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany("Novost")
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.Popust", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany()
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.Prati", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany()
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany()
                        .HasForeignKey("KupacID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.PreuzimanjeIgre", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany("PreuzimanjeIgre")
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany("PreuzimanjeIgre")
                        .HasForeignKey("KupacID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.PrijavaPremium", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany()
                        .HasForeignKey("KupacId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.PrijavaRecenzije", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID");

                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany("PrijavaRecenzije")
                        .HasForeignKey("KupacID");

                    b.HasOne("eStore_web.EF_Models.Recenzija", "Recenzija")
                        .WithMany("PrijavaRecenzija")
                        .HasForeignKey("RecenzijaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.Recenzija", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany("Recenzija")
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany("Recenzija")
                        .HasForeignKey("KupacID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.Refund", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID");

                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany()
                        .HasForeignKey("IgraID");

                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany("Refund")
                        .HasForeignKey("KupacID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.Report", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Kupac", "ReportovaniKupac")
                        .WithMany()
                        .HasForeignKey("ReportovaniKupacId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eStore_web.EF_Models.Wallet", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithOne("Wallet")
                        .HasForeignKey("eStore_web.EF_Models.Wallet", "KupacID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.WalletHistory", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany()
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletID");
                });

            modelBuilder.Entity("eStore_web.EF_Models.WishList", b =>
                {
                    b.HasOne("eStore_web.EF_Models.Igra", "Igra")
                        .WithMany()
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eStore_web.EF_Models.Kupac", "Kupac")
                        .WithMany()
                        .HasForeignKey("KupacID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

using eStore_web.EF_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF
{
    public class eContext : DbContext
    {
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<EmailAddress> EmailAddress { get; set; }
        public DbSet<LoginInfo> LoginInfo { get; set; }
        public DbSet<KupacKupuje> KupacKupuje { get; set; }
        public DbSet<Igra> Igra { get; set; }
        public DbSet<GameGenre> GameGenre { get; set; }
        public DbSet<RatingCategorie> RatingCategorie { get; set; }
        public DbSet<Refund> Refund { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<PrijavaRecenzije> PrijavaRecenzije { get; set; }
        public DbSet<Novost> Novost { get; set; }
        public DbSet<PreuzimanjeIgre> PreuzimanjeIgre { get; set; }
        public DbSet<AccountBase> AccountBase { get; set; }
        public DbSet<Kupac> Kupac { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Popust> Popust { get; set; }
        public DbSet<IgricaImage> IgricaImage { get; set; }
        public DbSet<AccountImage> AccountImages { get; set; }
        public DbSet<Prati> Prati { get; set; }
        public DbSet<WalletHistory> WalletHistory { get; set; }
        public DbSet<PrijavaPremium> PrijavaPremium { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Notifikacije> Notifikacija { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=app.fit.ba,1431;Database=p1803;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=p1803;Password=ENxR8B!");
            //optionsBuilder.UseSqlServer("Server=localhost;Database=RS1_Seminarski;Trusted_Connection=False;MultipleActiveResultSets=true;User=sa;Password=testPassword_123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<AccountBase>()
                .HasOne(a => a.EmailAddress)
                .WithOne(b => b.AccountBase)
                .HasForeignKey<EmailAddress>(b => b.BaseID);

            modelBuilder.Entity<AccountBase>()
                .HasOne(a => a.LoginInfo)
                .WithOne(b => b.AccountBase)
                .HasForeignKey<LoginInfo>(b => b.BaseID);

            modelBuilder.Entity<Kupac>()
                .HasOne(a => a.Wallet)
                .WithOne(b => b.Kupac)
                .HasForeignKey<Wallet>(b => b.KupacID);

            modelBuilder.Entity<WishList>()
            .HasKey(c => new { c.KupacID, c.IgraID });

            modelBuilder.Entity<KupacKupuje>()
            .HasKey(c => new { c.KupacID, c.IgraID });

            modelBuilder.Entity<IgricaImage>(entity =>
            {
                entity.HasIndex(e => e.IgraID).IsUnique();
            });


            modelBuilder.Entity<AccountImage>(entity =>
            {
                entity.HasIndex(oi => oi.BaseID).IsUnique();
            });

        }

    }

}

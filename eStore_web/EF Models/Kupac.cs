using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Kupac
    {
        [Key]
        public int KupacID { get; set; }
        [Required,MinLength(5),MaxLength(25)]
        public string UserName { get; set; }
        [Required,DefaultValue("False")]
        public bool PretplacenNaPremium { get; set; }
        [Required,DefaultValue(10)]
        public int Reputacija { get; set; }
        [Required,DefaultValue(0)]
        public bool BanStatus { get; set; }
        
        [ForeignKey("AccountBase")]
        public int BaseID { get; set; }
        public AccountBase AccountBase { get; set; }

        [ForeignKey("Wallet")]
        public int? WalletID { get; set; }
        public Wallet Wallet { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; }
        [MaxLength(50)]
        public string Prezime { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DatumRodenja { get; set; }

        public ICollection<PrijavaRecenzije>PrijavaRecenzije { get; set; }
        public ICollection<Recenzija> Recenzija { get; set; }
        public ICollection<Refund> Refund { get; set; }
        public ICollection<PreuzimanjeIgre> PreuzimanjeIgre { get; set; }
        public ICollection<KupacKupuje> KupacKupuje { get; set; }
    }
}

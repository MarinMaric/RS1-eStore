using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Administrator
    {

        [Key]
        public int AdministratorID { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; }
        [MaxLength(50)]
        public string Prezime { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DatumRodenja { get; set; }

        [ForeignKey("AccountBase")]
        public int BaseID { get; set; }
        public AccountBase AccountBase { get; set; }
        
    }
}
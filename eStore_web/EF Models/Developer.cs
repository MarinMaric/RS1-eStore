using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Developer
    {
       [Key]
       public int DeveloperID { get; set; }

        [MaxLength(50)]
        public string NazivKompanije { get; set; }
        public string Opis { get; set; }

        public DateTime DatumUtemeljenja { get; set; }
        public string LokacijaAdresa { get; set; }
        public bool Banovan { get; set; }

        [ForeignKey("AccountBase")]
        public int BaseID { get; set; }
        public AccountBase AccountBase { get; set; }

        public ICollection<Novost> Novosti { get; set; }
        public ICollection<Igra> Igre { get; set; }
    }
}

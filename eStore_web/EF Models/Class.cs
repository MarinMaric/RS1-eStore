using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class AutentifikacijaToken
    {
        public int Id { get; set; }
        public string Vrijednost { get; set; }
        public int KorisnickiNalogId { get; set; }
        public LoginInfo KorisnickiNalog { get; set; }
        public DateTime VrijemeEvidentiranja { get; set; }
        public string IpAdresa { get; set; }
    }
}

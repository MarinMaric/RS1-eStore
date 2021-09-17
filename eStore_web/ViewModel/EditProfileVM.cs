using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class EditProfileVM
    {

        [Required, MinLength(5), MaxLength(25)]
        public string UserName { get; set; }
        [Required, MaxLength(30), EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DatumPrikaz { get; set; }
        public string Datum { get; set; }
        public int DrzavaValue { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ProfilSlika { get; set; }

        public int TipKorisnika { get; set; }
        public int ID { get; set; }
        public int BaseID { get; set; }

        //Kupac fields
        [MaxLength(50)]
        public string Ime { get; set; }
        [MaxLength(50)]
        public string Prezime { get; set; }

        //Developer fields
        [MaxLength(50)]
        public string NazivKompanije { get; set; }
        public string Opis { get; set; }
        public string LokacijaAdresa { get; set; }
    }
}

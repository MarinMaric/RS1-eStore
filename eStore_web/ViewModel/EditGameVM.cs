using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class EditGameVM
    {
        public int IgraID { get; set; }
        [Required, MaxLength(50)]
        public string Naziv { get; set; }
        [Required, MaxLength(1048)]
        public string Opis { get; set; }
        [Required, MaxLength(1048)]
        public string LinkIgre { get; set; }
        [Required, MaxLength(1048)]
        public string TrailerUrl { get; set; }
        [Required, Range(1, 999)]
        public float Cijena { get; set; }
        public byte[] SlikaPrikaz { get; set; }
        public IFormFile Slika { get; set; }

        public int ZanrValue { get; set; }
        public List<SelectListItem> Zanr { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class UploadGameVM
    {
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string TrailerUrl { get; set; }

        [Required]
        public int ZanrValue { get; set; }
        public List<SelectListItem> Zanr { get; set; }

        public int RatingValue { get; set; }
        public List<SelectListItem> Rating { get; set; }

        public DateTime DatumIzlaska { get; set; }
        [Required]
        public int Cijena { get; set; }
    }
}

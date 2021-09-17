using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class IgraDetaljiVM
    {
        public int igraId { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public string zanr { get; set; }
        public int zanrValue { get; set; }
        public string datumObjave { get; set; }
        public string linkIgre { get; set; }
        public string trailerUrl { get; set; }
        public float cijena { get; set; }
        public bool odobrena { get; set; }
        public bool premium { get; set; }
        public string slikaPrikaz { get; set; }
        public IFormFile slikaUpload { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class PopustVM
    {
        public int IgraID { get; set; }
        public string Naziv { get; set; }
        [Range(5,100, ErrorMessage ="Popust mora biti između 5 i 100")]
        public int Postotak { get; set; }
        [Remote("CheckDatumPocetka","Developer", ErrorMessage ="Datum nevaljan")]
        public DateTime DatumPocetka { get; set; }
        [Range(1,30, ErrorMessage ="Broj dana mora biti između 1 i 30")]
        public int BrojDana { get; set; }
    }
}

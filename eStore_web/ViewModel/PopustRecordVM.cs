using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class PopustRecordVM
    {
        public int Postotak { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumKraj { get; set; }
    }
}

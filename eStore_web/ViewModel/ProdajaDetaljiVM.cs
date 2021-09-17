using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class ProdajaDetaljiVM
    {
        public DateTime VrijemeKupovine { get; set; }
        public string Drzava { get; set; }
        public int Cijena { get; set; }
        public int Popust { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class SalesFilterVM
    {
        public string NazivIgre { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public int BrojKopijaOd { get; set; }
        public int BrojKopijaDo { get; set; }
        public string Drzava { get; set; }
    }
}

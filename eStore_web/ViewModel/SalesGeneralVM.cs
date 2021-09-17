using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class SalesGeneralVM
    {
        public List<ProdajaVM> prodaje { get; set; }
        public SalesFilterVM filter { get; set; }

    }
}

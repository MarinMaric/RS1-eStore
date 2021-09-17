using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempReport
{
    public class GeneralRow
    {
        public string NazivIgre { get; set; }
        public string Drzava { get; set; }
        public int BrojKopija { get; set; }

        public static List<GeneralRow> Get()
        {
            return new List<GeneralRow>();
        }
    }
}
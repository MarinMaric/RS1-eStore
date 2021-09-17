using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempReport
{
    public class ReportRow
    {
        public string IgraNaziv { get; set; }
        public DateTime VrijemeKupovine { get; set; }
        public string Drzava { get; set; }
        public int Cijena { get; set; }
        public int Popust { get; set; }

        public static List<ReportRow> Get()
        {
            return new List<ReportRow>();
        }
    }
}
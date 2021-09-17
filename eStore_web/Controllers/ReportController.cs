using AspNetCore.Reporting;
using eStore_web.Common;
using eStore_web.EF;
using eStore_web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TempReport;

namespace eStore_web.Controllers
{
    public class ReportController : Controller
    {
        eContext db = new eContext();

        public List<ReportRow> GetProdajeIgra(SalesFilterVM filter)
        {
            var query = db.KupacKupuje.Include(x=>x.Kupac).ThenInclude(x=>x.AccountBase)
                .ThenInclude(x=>x.Drzava).Include(x=>x.Igra).Include(x=>x.Popust).Select(x => new ReportRow
            {
                Cijena=(int)x.Cijena,
                Drzava=x.Kupac.AccountBase.Drzava.Naziv,
                IgraNaziv=x.Igra.Naziv,
                Popust=x.Popust==null?0:x.Popust.PopustProcent,
                VrijemeKupovine=x.VrijemeKupovine
            }).AsQueryable();

            if (!string.IsNullOrEmpty(filter.NazivIgre))
            {
                query = query.Where(x => x.IgraNaziv==filter.NazivIgre);
            }
            if (filter.DatumOd != DateTime.MinValue)
            {
                query = query.Where(x => DateTime.Compare(x.VrijemeKupovine, filter.DatumOd) >= 0);
            }
            if (filter.DatumDo != DateTime.MinValue)
            {
                query = query.Where(x => DateTime.Compare(x.VrijemeKupovine, filter.DatumDo) <= 0);
            }
            if (!string.IsNullOrEmpty(filter.Drzava))
            {
                query = query.Where(x => x.Drzava.Contains(filter.Drzava));
            }

            var podaci = query.OrderBy(x=> x.IgraNaziv).ThenBy(x=>x.Drzava).ToList();
            return podaci;
        }
        public IActionResult SpecificReport(string type, SalesFilterVM filter)
        {
            LocalReport _localReport = new LocalReport("Report/Report1.rdlc");
            List<ReportRow> podaci = GetProdajeIgra(filter);
            DataSet ds = new DataSet();
            _localReport.AddDataSource("DataSet1", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Naziv", filter.NazivIgre);
            parameters.Add("Drzava", filter.Drzava);
            parameters.Add("DatumOd", filter.DatumOd.ToString());
            parameters.Add("DatumDo", filter.DatumDo.ToString());

            if (type == "pdf")
            {
                ReportResult result = _localReport.Execute(RenderType.Pdf, parameters:parameters);
                return File(result.MainStream, "application/pdf");
            }
            else if(type=="excel")
            {
                ReportResult result = _localReport.Execute(RenderType.ExcelOpenXml);
                return File(result.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            return Ok();
        }

        public List<GeneralRow>  GetProdajeGeneral(SalesFilterVM filter)
        {
            List<GeneralRow> finalProdaje = new List<GeneralRow>();

            //Uzimam  sve prodaje za sve igre developera
            var list = db.Igra.Where(x => x.Developer.BaseID == Autentifikacija.GetLogiraniKorisnik(HttpContext).BaseID).ToList();
            int devId = (int)Autentifikacija.GetLogiraniKorisnik(HttpContext).BaseID;
            var prodaje = db.KupacKupuje.Include(x => x.Igra).ThenInclude(x => x.Developer)
                        .Include(x => x.Kupac).ThenInclude(x => x.AccountBase).ThenInclude(x => x.Drzava)
                        .Where(x => x.Igra.Developer.BaseID == devId).Select(x => new GeneralRow
                        {
                            NazivIgre = x.Igra.Naziv,
                            Drzava = x.Kupac.AccountBase.Drzava.Naziv,
                            BrojKopija = 0
                        }).AsQueryable();

            List<string> igre = prodaje.Select(x => x.NazivIgre).Distinct().ToList();

            //Za svaku igru dodajem pojedini zapis za sve ukupne prodaje u drzavi
            foreach(var i in igre)
            {
                List<string> drzave = prodaje.Where(x => x.NazivIgre ==i).Select(x => x.Drzava).Distinct().ToList();

                foreach (var d in drzave)
                {
                    GeneralRow r = new GeneralRow { 
                        NazivIgre=i,
                        Drzava=d,
                        BrojKopija=prodaje.Where(x => x.NazivIgre == i && x.Drzava == d).Count()
                    };
                    finalProdaje.Add(r);
                }
            }

            return finalProdaje;
        }   
        public IActionResult GeneralReport(string type, SalesFilterVM filter)
        {
            LocalReport _localReport = new LocalReport("Report/Report2.rdlc");
            List<GeneralRow> podaci = GetProdajeGeneral(filter);
            DataSet ds = new DataSet();
            _localReport.AddDataSource("DataSet2", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Naziv", filter.NazivIgre);
            parameters.Add("Drzava", filter.Drzava);
            parameters.Add("DatumOd", filter.DatumOd.ToString());
            parameters.Add("DatumDo", filter.DatumDo.ToString());

            if (type == "pdf")
            {
                ReportResult result = _localReport.Execute(RenderType.Pdf/*, parameters: parameters*/);
                return File(result.MainStream, "application/pdf");
            }
            else if (type == "excel")
            {
                ReportResult result = _localReport.Execute(RenderType.ExcelOpenXml);
                return File(result.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }

            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.Common;
using eStore_web.EF;
using eStore_web.EF_Models;
using eStore_web.Services;
using eStore_web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStore_web.Controllers
{
    public class GamesAngularController : Controller
    {
        eContext db = new eContext();


        private INotifikacije _notifikacije;

        public GamesAngularController(INotifikacije notifikacije)
        {
            _notifikacije = notifikacije;

        }

        [HttpGet]
        public IActionResult ViewGamesJson(int currentPage = 1, int itemsPerPage = 5, string filter=null)
        {
            var user = HttpContext.GetKorisnikOfAuthToken();
            if (user == null)
            {
                return Unauthorized();
            }
            List<IgraBrief> igre = db.Igra.Where(x => x.Deleted != 1 && string.IsNullOrEmpty(filter)?x.Naziv!=filter:x.Naziv.Contains(filter))
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .Select(x=>new IgraBrief { 
                igraId=x.IgraID,
                naziv=x.Naziv,
                cijena=x.Cijena
            }).ToList();
          
            return Ok(igre);
        }
        public IActionResult GameDetailsJson(int igraId)
        {
            IgraDetaljiVM igra = db.Igra.Include(x=>x.IgricaImage).Include(x=>x.GameGenre).Where(x =>x.IgraID==igraId && x.Deleted != 1).Select(x => new IgraDetaljiVM
            {
                igraId = x.IgraID,
                naziv = x.Naziv,
                cijena = x.Cijena,
                linkIgre = x.LinkIgre,
                trailerUrl = x.TrailerUrl,
                datumObjave=x.DatumObjave.ToString(),
                zanr=x.GameGenre.NazivZanra,
                zanrValue=(int)x.GameGenreID,
                odobrena=x.Odobrena,
                premium=x.PremiumStatus,
                opis=x.Opis,
                slikaPrikaz = x.IgricaImage!=null?ConvertImage(x.IgricaImage.Image):null
            }).FirstOrDefault();

            return Ok(igra);
        }
        string ConvertImage(byte[] img)
        {
            var base64 = Convert.ToBase64String(img);
            var imgsrc = string.Format("data:image/gif;base64,{0}", base64);

            return imgsrc;
        }
        [HttpPost]
        public IActionResult UploadGame(IgraDetaljiVM vm) {
            var user = HttpContext.GetKorisnikOfAuthToken();
            if (user == null ||user.TipKorisnika!=2)
            {
                return Unauthorized();
            }

            if (db.Igra.Any(x => x.Naziv == vm.naziv && x.Deleted!=1))
                return BadRequest();

            if (vm.igraId == 0)
            {
                Igra igraZahtjev = new Igra();
                int ID = GetDeveloperID((int)user.BaseID);
                igraZahtjev.Naziv = vm.naziv;
                igraZahtjev.Opis = vm.opis;
                igraZahtjev.DeveloperID = ID;
                igraZahtjev.LinkIgre = vm.linkIgre;
                igraZahtjev.DatumObjave = DateTime.Now;
                igraZahtjev.Odobrena = false;
                igraZahtjev.Cijena = vm.cijena;
                igraZahtjev.TrailerUrl = vm.trailerUrl;
                igraZahtjev.GameGenreID = int.Parse(vm.zanr);

                db.Igra.Add(igraZahtjev);
                db.SaveChanges();

                DodajSliku(igraZahtjev.IgraID, vm.slikaUpload);
                _notifikacije.posaljiNotifikacijeAdminu(ID, new NotifikacijaVM()
                {
                    Poruka = "Novi zahtjev za igricu!",
                    Url = "/Administrator/OdobriGet"
                });
            }
            else
            {
                Igra igraZahtjev = db.Igra.Find(vm.igraId);
                igraZahtjev.Naziv = vm.naziv;
                igraZahtjev.Opis = vm.opis;
                igraZahtjev.LinkIgre = vm.linkIgre;
                igraZahtjev.Cijena = vm.cijena;
                igraZahtjev.TrailerUrl = vm.trailerUrl;
                if(!string.IsNullOrEmpty(vm.zanr))
                    igraZahtjev.GameGenreID = int.Parse(vm.zanr);

                db.SaveChanges();

                if(vm.slikaUpload!=null)
                    DodajSliku(igraZahtjev.IgraID, vm.slikaUpload);
            }

            return Ok(true);
        }

        public IActionResult GetZanroviJson()
        {
            var zanrovi = db.GameGenre.Select(x => new ZanrDropdownVM { zanrId = x.GameGenreID, naziv = x.NazivZanra }).ToList();
            return Ok(zanrovi);
        }
        int GetDeveloperID(int BaseID)
        {
            Developer dev = db.Developer.FirstOrDefault(x => x.BaseID == BaseID);
            if (dev != null)
                return dev.DeveloperID;
            else return 0;
        }
        public bool DodajSliku(int IgraID, IFormFile Image)
        {
            FileManagment FileManagment = new FileManagment();
            return FileManagment.DodajSliku(IgraID, 0, Image);
        }
        public IActionResult DeleteGame(int igraID)
        {
            var igra = db.Igra.Find(igraID);
            igra.Deleted = 1;
            db.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult CollectionSize(string filter=null)
        {
            var user = HttpContext.GetKorisnikOfAuthToken();
            if (user == null)
            {
                return Unauthorized();
            }
            int size;

            if (string.IsNullOrEmpty(filter))
            {
                size = db.Igra.Where(x => x.Deleted != 1).Count();
            }
            else
            {
                size = db.Igra.Where(x => x.Deleted != 1 && x.Naziv.Contains(filter)).Count();
            }
            return Ok(size);
        }
    }
}

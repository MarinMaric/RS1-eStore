using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStore_web.Common;
using eStore_web.Services;
using eStore_web.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStore_web.Controllers
{
    [Autorizacija(Kupac: false, Developer: true, Administrator: false, Svi: false)]
    public class DeveloperController: Controller
    {
        eContext db = new eContext();
        

        private INotifikacije _notifikacije;

        public DeveloperController(INotifikacije notifikacije)
        {
            _notifikacije = notifikacije;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadGame()
        {
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            ViewData["zanrovi"] = db.GameGenre.ToList();
            ViewData["dev"] = LoginInfo.BaseID; 
            ViewData["developeri"] = db.Developer.ToList();

            UploadGameVM vm = new UploadGameVM
            {
                Zanr = db.GameGenre.Select(x => new SelectListItem { Text = x.NazivZanra, Value = x.GameGenreID.ToString() }).ToList(),
                Rating = db.RatingCategorie.Select(x => new SelectListItem { Text = x.NazivKategorije, Value = x.RatingCategorieID.ToString() }).ToList()
            };

            return View("UploadGame",vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestPublish(int devID,UploadGameVM upload)
        {
            Igra igraZahtjev = new Igra();
            int ID = db.Developer.First(x => x.BaseID == devID).DeveloperID;
            igraZahtjev.Naziv = upload.Naziv;
            igraZahtjev.GameGenreID = upload.ZanrValue;
            igraZahtjev.Opis = upload.Opis;
            igraZahtjev.DeveloperID = ID;
            igraZahtjev.LinkIgre = upload.Link;
            igraZahtjev.DatumObjave = DateTime.Now;
            igraZahtjev.Odobrena = false;
            igraZahtjev.Cijena = upload.Cijena;
            igraZahtjev.TrailerUrl = upload.TrailerUrl;

            db.Igra.Add(igraZahtjev);
            db.SaveChanges();
            _notifikacije.posaljiNotifikacijeAdminu((int)Autentifikacija.GetLogiraniKorisnik(HttpContext).BaseID, new NotifikacijaVM()
            {
                Poruka = "Novi zahtjev za igricu!",
                Url = "/Administrator/OdobriGet"
            });
            return View("Index");
        }
        
        public IActionResult ViewGames()
        {
            ViewData["listaIgara"] = db.Igra.Where(x=>x.Deleted!=1).ToList();
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            List<Developer>developeri=db.Developer.ToList();
            foreach(Developer d in developeri)
            {
                if (d.BaseID == LoginInfo.BaseID)
                {
                    ViewData["dev"] = d.DeveloperID;
                }
            }
            return View();
        }

        public IActionResult EditGame(int igraID)
        {
            EditGameVM vm = db.Igra.Where(x=>x.IgraID==igraID).Select(x => new EditGameVM { 
                IgraID=x.IgraID,
                Naziv=x.Naziv,
                Opis=x.Opis,
                Cijena=x.Cijena,
                ZanrValue=(int)x.GameGenreID,
                SlikaPrikaz=x.IgricaImage.Image,
                LinkIgre=x.LinkIgre,
            }).FirstOrDefault();
            vm.Zanr = db.GameGenre.Select(x => new SelectListItem
            {
                Text = x.NazivZanra,
                Value = x.GameGenreID.ToString()
            }).ToList();
            
            return View(vm);
        }
        [ValidateAntiForgeryToken]
        public IActionResult SaveChanges(EditGameVM edit)
        {
            string success = "", error = "";
            Igra igra = db.Igra.Find(edit.IgraID);

            igra.Naziv = edit.Naziv;
            igra.Opis = edit.Opis;
            igra.Cijena = edit.Cijena;
            igra.GameGenreID = edit.ZanrValue;
            igra.LinkIgre = edit.LinkIgre;
            if (edit.Slika != null)
            {
                if (DodajSliku(igra.IgraID, edit.Slika))
                {
                    success += "<br>- profilna slika ";
                }
                else
                {
                    error += "<br>- Greska pri mjenjanju profilne slike";
                }
            }
            db.SaveChanges();
            db.Dispose();

            return RedirectToAction("ViewGames");
        }

        public IActionResult ViewGameDetails(int igraID, int notId)
        {
            if (notId != 0)
            {
                db.Notifikacija.Find(notId).otvorena = true;
                db.SaveChanges();
            }
            ViewData["zanrovi"] = db.GameGenre.ToList();
            List<Igra> listaIgara = db.Igra.Include(x=>x.IgricaImage).Include(x=>x.GameGenre).ToList();
            foreach (Igra i in listaIgara)
            {
                if (i.IgraID == igraID)
                {
                    ViewData["igra"] = i;
                    break;
                }
            }
            
            return View("ViewGameDetails");
        }
        public IActionResult DeleteGame(int igraID)
        {
            db.Igra.Find(igraID).Deleted = 1;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("ViewGames");
        }

        public bool DodajSliku(int IgraID, IFormFile Image)
        {
            FileManagment FileManagment = new FileManagment();
            return FileManagment.DodajSliku(IgraID, 0, Image);
        }

        public IActionResult PutOnSale(int igraID)
        {
            PopustVM vm = new PopustVM
            {
                IgraID = igraID,
                Naziv=db.Igra.Find(igraID).Naziv,
                DatumPocetka = DateTime.Now
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDiscount(PopustVM vm)
        {
            Popust popust = new Popust
            {
                IgraID = vm.IgraID,
                PocetakPopusta = vm.DatumPocetka,
                PopustProcent = vm.Postotak,
                KrajPopusta = vm.DatumPocetka.AddDays(vm.BrojDana),
                Aktivan=true
            };

            db.Popust.Add(popust);
            db.SaveChanges();

            return RedirectToAction("ViewGameDetails", new { igraID=vm.IgraID });
        }
        public bool CheckDatumPocetka(DateTime datum)
        {
            if (DateTime.Compare(datum, DateTime.Now) != 0)
                return true;
            else return false;
        }

        public IActionResult DiscountHistory(int igraID, DateTime pocetakOd, DateTime pocetakDo, DateTime krajOd, DateTime krajDo)
        {
            var popusti = db.Popust.Where(x => x.IgraID == igraID).AsQueryable();
            var test = popusti.ToList();
            if (pocetakOd != DateTime.MinValue)
            {
                popusti = popusti.Where(x => DateTime.Compare(x.PocetakPopusta, pocetakOd) >= 0).AsQueryable();
            }
            if (pocetakDo != DateTime.MinValue)
            {
                popusti = popusti.Where(x => DateTime.Compare(x.PocetakPopusta, pocetakDo) <= 0).AsQueryable();
            }
            if (krajOd != DateTime.MinValue)
            {
                popusti = popusti.Where(x => DateTime.Compare(x.KrajPopusta, krajOd) >= 0).AsQueryable();
            }
            if (krajDo != DateTime.MinValue)
            {
                popusti = popusti.Where(x => DateTime.Compare(x.PocetakPopusta, krajDo) <= 0).AsQueryable();
            }

            var vm =popusti.Select(x=>new PopustRecordVM { 
                DatumPocetka=x.PocetakPopusta,
                DatumKraj=x.KrajPopusta,
                Postotak=x.PopustProcent
            }).ToList();
            return PartialView("DiscountHistory", vm);
        }
        public IActionResult PovuciPopust(int popustID)
        {
            Popust p = db.Popust.Find(popustID);
            p.Aktivan = false;
            db.SaveChanges();
            return RedirectToAction("DiscountHistory",p.IgraID);
        }

        public IActionResult Sales()
        {
            return View();
        }
        public IActionResult SalesInfo(int igraID, SalesFilterVM vm)
        {
            var prodaje = db.KupacKupuje.Include(x=>x.Kupac).ThenInclude(x=>x.AccountBase)
                .ThenInclude(x=>x.Drzava).Include(x=>x.Popust).Where(x=>x.IgraID==igraID)
                .AsQueryable();

            if (vm.DatumOd != DateTime.MinValue)
            {
                prodaje = prodaje.Where(x => DateTime.Compare(x.VrijemeKupovine, vm.DatumOd) >= 0);
            }
            if (vm.DatumDo != DateTime.MinValue)
            {
                prodaje = prodaje.Where(x => DateTime.Compare(x.VrijemeKupovine, vm.DatumDo) <= 0);
            }
            if (!string.IsNullOrEmpty(vm.Drzava))
            {
                prodaje = prodaje.Where(x =>x.Kupac.AccountBase.Drzava.Naziv==vm.Drzava);
            }

            var list = prodaje.ToList().Select(x => new ProdajaDetaljiVM
            {
                Drzava = x.Kupac.AccountBase.Drzava.Naziv,
                Cijena = (int)x.Cijena,
                Popust = x.Popust!=null?x.Popust.PopustProcent:0,
                VrijemeKupovine = x.VrijemeKupovine
            }).ToList();

            TempData["igraID"] = igraID;
            TempData["vm"] = vm;
            ViewData["naziv"] = db.Igra.Find(igraID).Naziv;
            return View(list);
        }
        public IActionResult SalesGeneral(SalesFilterVM vm)
        {
            var prodajeVM = db.Igra.Include(x=>x.Developer).Where(x => x.Developer.BaseID == Autentifikacija.GetLogiraniKorisnik(HttpContext).BaseID)
                .Select(x => new ProdajaVM {
                    IgraID=x.IgraID,
                    NazivIgre = x.Naziv,
                    BrojKopija = 0
                })
                .ToList();

            for (int i = 0; i < prodajeVM.Count(); i++)
            {
                var query = db.KupacKupuje.Where(x => x.Igra.Naziv == prodajeVM.ElementAt(i).NazivIgre).AsQueryable();
                if (!string.IsNullOrEmpty(vm.NazivIgre))
                {
                    query = query.Where(x => x.Igra.Naziv.Contains(vm.NazivIgre));
                }
                if (vm.DatumOd != DateTime.MinValue)
                {
                    query = query.Where(x => DateTime.Compare(x.VrijemeKupovine, vm.DatumOd) >= 0);
                }
                if (vm.DatumDo != DateTime.MinValue)
                {
                    query = query.Where(x => DateTime.Compare(x.VrijemeKupovine, vm.DatumDo) <= 0);
                }

                int brKopija = query.ToList().Count();
                prodajeVM[i].BrojKopija = brKopija;
            }

            var query2 = prodajeVM.AsQueryable();
            query2 = query2.Where(x => x.BrojKopija > 0);
            prodajeVM = query2.OrderBy(x=>x.NazivIgre).ToList();

            TempData["vm"] = vm;

            return PartialView(prodajeVM);
        }
    }
}

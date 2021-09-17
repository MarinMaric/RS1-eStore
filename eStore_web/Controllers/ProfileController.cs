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
using eStore_web.ViewModel;
using eStore_web.Common;
using System.Web;

namespace eStore_web.Controllers
{
    public class ProfileController : Controller
    {
        eContext db = new eContext();
        
        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult Index(int ID)
        {
            #region IsUserLoggedIn
            int IsSame = 0;
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            if (LoginInfo != null)
            {
                if (LoginInfo.TipKorisnika == 1)
                {
                    Kupac Kupac = db.Kupac.Where(kup => kup.BaseID == LoginInfo.BaseID).FirstOrDefault();
                    if (Kupac != null)
                    {
                        if (Kupac.KupacID == ID)
                            IsSame = 1;
                    }
                }else if (LoginInfo.TipKorisnika == 2)
                {
                    Developer developer = db.Developer.Where(dev => dev.BaseID == LoginInfo.BaseID).FirstOrDefault();
                    if (developer != null)
                    {
                        if (developer.DeveloperID == ID)
                            IsSame = 1;
                    }
                }
                
            }
            ViewBag.IsSame = IsSame;
            #endregion IsUserLoggedIn

            if (LoginInfo.TipKorisnika == 1)
            {
                var k = db.Kupac.Where(kupac => kupac.KupacID == ID)
            .Include(o => o.AccountBase)
            .ThenInclude(oi => oi.Image)
            .Include(o => o.AccountBase)
            .ThenInclude(li => li.LoginInfo)
            .FirstOrDefault();
                if (k == null)
                {
                    return View("ProfileNotFound");
                }
                ViewBag.profile = k;
            }
            else if(LoginInfo.TipKorisnika==2)
            {
                var d = db.Developer.Where(dev => dev.DeveloperID == ID)
                .Include(o => o.AccountBase)
                .ThenInclude(oi => oi.Image)
                .Include(o => o.AccountBase)
                .ThenInclude(li => li.LoginInfo)
                .FirstOrDefault();
                if (d == null)
                {
                    return View("ProfileNotFound");
                }
                ViewBag.profile = d;
            }
            
            return View("profile");
        }


        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult vcProfile(int KupacID, string VC,int IsSame)
        {
            return ViewComponent(VC, new { k = KupacID, IsSame });
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public void RemoveWishList(int KupacID, int IgraID)
        {

            Kupac kupac = db.Kupac.Where(k => k.KupacID == KupacID).FirstOrDefault();
            WishList wish = db.WishList.Where(wl => wl.Kupac.KupacID == kupac.KupacID && wl.IgraID == IgraID).FirstOrDefault();
            if (kupac == null)
                throw Exception();
            if (wish != null)
            {
                db.WishList.Remove(wish);
                db.SaveChanges();
                db.Dispose();
            }


        }

        private Exception Exception()
        {
            throw new NotImplementedException();
        }

        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public void RemoveRecenzija(int KupacID, int IgraID)
        {
            Kupac kupac = db.Kupac.Where(k => k.KupacID == KupacID).FirstOrDefault();
            Recenzija recenzija = db.Recenzija.Where(wl => wl.Kupac.KupacID == kupac.KupacID && wl.IgraID == IgraID).FirstOrDefault();
            if (recenzija != null)
            {
                db.Recenzija.Remove(recenzija);
                db.SaveChanges();
                db.Dispose();
            }
        }



        [Autorizacija(Kupac: true, Developer: true, Administrator: false, Svi: false)]
        public IActionResult EditProfile()
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            AccountImage ooo = db.AccountImages.FirstOrDefault();
            List<Drzava> drzave = db.Drzava.ToList();
            ViewBag.drzave = drzave;

            TempData.Peek("success-key");
            TempData.Peek("error-key");
            EditProfileVM vm;

            if (Log.TipKorisnika == 1)
            {
                Kupac kupac = db.Kupac.Where(k => k.BaseID == Log.BaseID)
                   .Include(o => o.AccountBase).ThenInclude(e => e.EmailAddress)
                    .Include(o => o.AccountBase).ThenInclude(l => l.LoginInfo)
                    .Include(o => o.AccountBase).ThenInclude(oi => oi.Image)
                    .Include(o => o.AccountBase).ThenInclude(d => d.Drzava)
                     .FirstOrDefault();
                ViewBag.profile = kupac;

                vm = new EditProfileVM
                {
                    DatumPrikaz = kupac.DatumRodenja,
                    DrzavaValue=kupac.AccountBase.DrzavaID,
                    Email=kupac.AccountBase.EmailAddress.Email,
                    Ime=kupac.Ime,
                    Prezime=kupac.Prezime,
                    UserName=kupac.UserName,
                    TipKorisnika=1,
                    ID=kupac.KupacID,
                    BaseID=kupac.BaseID,
                    Password=kupac.AccountBase.LoginInfo.Password,
                    Image=kupac.AccountBase.Image.Image
                };
            }
            else {
                Developer developer = db.Developer.Where(x => x.BaseID == Log.BaseID)
                    .Include(o => o.AccountBase).ThenInclude(e => e.EmailAddress)
                    .Include(o => o.AccountBase).ThenInclude(l => l.LoginInfo)
                    .Include(o => o.AccountBase).ThenInclude(oi => oi.Image)
                    .Include(o => o.AccountBase).ThenInclude(d => d.Drzava)
                    .FirstOrDefault();
                ViewBag.profile = developer;

                vm = new EditProfileVM
                {
                    DatumPrikaz=developer.DatumUtemeljenja,
                    DrzavaValue=developer.AccountBase.DrzavaID,
                    Email=developer.AccountBase.EmailAddress.Email,
                    NazivKompanije=developer.NazivKompanije,
                    LokacijaAdresa=developer.LokacijaAdresa,
                    UserName=developer.AccountBase.LoginInfo.AccountName,
                    TipKorisnika=2,
                    ID=developer.DeveloperID,
                    BaseID=developer.BaseID,
                    Password=developer.AccountBase.LoginInfo.Password,
                    Opis=developer.Opis
                };

                if (developer.AccountBase.Image != null)
                {
                    vm.Image = developer.AccountBase.Image.Image;
                }
            }
            return View(vm);
        }


        [Autorizacija(Kupac: true, Developer: true, Administrator: false, Svi: false)]
        public IActionResult EditProfileSet(EditProfileVM edit)
        {
            string success = null;
            string error = null;
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            if (Log.TipKorisnika == 1)
            {
                var kupac = db.Kupac.Where(k => k.BaseID == Log.BaseID)
                .Include(o => o.AccountBase)
                  .ThenInclude(l => l.LoginInfo)
                .Include(o => o.AccountBase)
                  .ThenInclude(e => e.EmailAddress)
                .Include(o => o.AccountBase)
                    .ThenInclude(d => d.Drzava)
                 .Include(o => o.AccountBase)
                  .ThenInclude(oi => oi.Image)
                 .FirstOrDefault();

                if (kupac != null)
                {

                    if (kupac.Ime != edit.Ime)
                    {
                        kupac.Ime = edit.Ime;
                        success += "<br>- Ime";
                    }
                    if (kupac.Prezime != edit.Prezime)
                    {
                        kupac.Prezime = edit.Prezime;
                        success += "<br>- Prezime";
                    }
                    if (kupac.DatumRodenja != DateTime.Parse(edit.Datum))
                    {
                        kupac.DatumRodenja = DateTime.Parse(edit.Datum);
                        success += "<br>- Datum rodenja";
                    }
                    if (kupac.AccountBase.EmailAddress == null)
                        kupac.AccountBase.EmailAddress = new EmailAddress();
                    if (kupac.AccountBase.EmailAddress.Email != edit.Email)
                    {

                        EmailAddress EmailAddress = db.EmailAddress.Where(ea => ea.Email == edit.Email).FirstOrDefault();
                        if (EmailAddress != null)
                        {
                            error += "<br>-Email adresa <b>" + edit.Email + "</b> vec postoji";
                        }
                        else
                        {
                            kupac.AccountBase.EmailAddress.Email = edit.Email;
                            success += "<br>- Email adresu";
                        }
                        //Sta ako ima vec ta email adresa ??????
                    }
                    if (kupac.AccountBase.LoginInfo == null)
                        kupac.AccountBase.LoginInfo = new LoginInfo();
                    if (kupac.AccountBase.LoginInfo.AccountName != edit.UserName)
                    {
                        LoginInfo LoginInfo = db.LoginInfo.Where(li => li.AccountName == edit.UserName && li.LoginInfoID != kupac.AccountBase.LoginInfoID).FirstOrDefault();
                        if (LoginInfo != null)
                        {
                            error += "<br>-Account name <b>" + edit.UserName + "</b> vec postoji";
                        }
                        else
                        {
                            kupac.AccountBase.LoginInfo.AccountName = edit.UserName;
                            success += "<br>- Account name";
                        }
                    }
                    if (kupac.UserName != edit.UserName)
                    {
                        kupac.UserName = edit.UserName;
                        success += "<br>- Korisnicko ime";
                    }
                    if (edit.ProfilSlika != null)
                    {
                        if (DodajSliku(kupac.BaseID, edit.ProfilSlika))
                        {
                            success += "<br>- profilna slika ";
                        }
                        else
                        {
                            error += "<br>- Greska pri mjenjanju profilne slike";
                        }
                    }
                }
            }
            else
            {
                var developer = db.Developer.Where(d => d.BaseID == Log.BaseID)
                .Include(o => o.AccountBase)
                  .ThenInclude(l => l.LoginInfo)
                .Include(o => o.AccountBase)
                  .ThenInclude(e => e.EmailAddress)
                .Include(o => o.AccountBase)
                    .ThenInclude(d => d.Drzava)
                 .Include(o => o.AccountBase)
                  .ThenInclude(oi => oi.Image)
                 .FirstOrDefault();

                if (developer != null)
                {
                    if (developer.NazivKompanije != edit.NazivKompanije)
                    {
                        developer.NazivKompanije = edit.NazivKompanije;
                        success += "<br>- Ime";
                    }
                    if (developer.DatumUtemeljenja != DateTime.Parse(edit.Datum))
                    {
                        developer.DatumUtemeljenja = DateTime.Parse(edit.Datum);
                        success += "<br>- Datum rodenja";
                    }
                    if (developer.LokacijaAdresa != edit.LokacijaAdresa)
                    {
                        developer.LokacijaAdresa = edit.LokacijaAdresa;
                        success += "<br>- Adresa firme";
                    }
                    if (developer.AccountBase.EmailAddress == null)
                        developer.AccountBase.EmailAddress = new EmailAddress();
                    if (developer.AccountBase.EmailAddress.Email != edit.Email)
                    {

                        EmailAddress EmailAddress = db.EmailAddress.Where(ea => ea.Email == edit.Email).FirstOrDefault();
                        if (EmailAddress != null)
                        {
                            error += "<br>-Email adresa <b>" + edit.Email + "</b> vec postoji";
                        }
                        else
                        {
                            developer.AccountBase.EmailAddress.Email = edit.Email;
                            success += "<br>- Email adresu";
                        }
                        //Sta ako ima vec ta email adresa ??????
                    }
                    if (developer.AccountBase.LoginInfo == null)
                        developer.AccountBase.LoginInfo = new LoginInfo();
                    if (developer.AccountBase.LoginInfo.AccountName != edit.UserName)
                    {
                        LoginInfo LoginInfo = db.LoginInfo.Where(li => li.AccountName == edit.UserName && li.LoginInfoID != developer.AccountBase.LoginInfoID).FirstOrDefault();
                        if (LoginInfo != null)
                        {
                            error += "<br>-Account name <b>" + edit.UserName + "</b> vec postoji";
                        }
                        else
                        {
                            developer.AccountBase.LoginInfo.AccountName = edit.UserName;
                            success += "<br>- Account name";
                        }
                    }
                    if(edit.Opis!=null && edit.Opis != developer.Opis)
                    {
                        developer.Opis = edit.Opis;
                        success += "<br>- Opis";
                    }
                    if (edit.ProfilSlika != null)
                    {
                        if (DodajSliku(developer.BaseID, edit.ProfilSlika))
                        {
                            success += "<br>- profilna slika ";
                        }
                        else
                        {
                            error += "<br>- Greska pri mjenjanju profilne slike";
                        }
                    }
                }
            }
            
            
            db.SaveChanges();
            db.Dispose();

            TempData["success-key"] = success;
            TempData["error-key"] = error;
            return RedirectToAction("EditProfile",new { edit.ID });
        }


        [Autorizacija(Kupac: true, Developer: true, Administrator: false, Svi: false)]
        public IActionResult ChangePassword(int ID)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            if (Log.TipKorisnika == 1)
            {
                ChangePasswordVM vm = db.Kupac.Where(k => k.BaseID == Log.BaseID)
                .Include(k => k.AccountBase)
                  .ThenInclude(o => o.LoginInfo)
                  .Select(x=>new ChangePasswordVM { 
                    ID=x.KupacID
                  }).FirstOrDefault();

                return View("ChangePassword", vm);
            }
            else
            {
                ChangePasswordVM developer = db.Developer.Where(d => d.BaseID == Log.BaseID)
                .Include(d => d.AccountBase)
                  .ThenInclude(o => o.LoginInfo)
                  .Select(x => new ChangePasswordVM
                  {
                      ID = x.DeveloperID
                  }).FirstOrDefault();

                return View("ChangePassword", developer);
            }
        }


        [Autorizacija(Kupac: true, Developer: true, Administrator: false, Svi: false)]
        public IActionResult ChangePasswordSet(int ID,string StariPassword,string NoviPassword1)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            if (Log.TipKorisnika == 1)
            {
                Kupac Kupac = db.Kupac.Where(k => k.KupacID == ID)
                .Include(o => o.AccountBase)
                  .ThenInclude(l => l.LoginInfo)
                .Include(o => o.AccountBase)
                  .ThenInclude(e => e.EmailAddress)
                .Include(o => o.AccountBase)
                    .ThenInclude(d => d.Drzava)
                    .FirstOrDefault();
                if (Kupac != null)
                {

                    if (Kupac.AccountBase.LoginInfo.Password == StariPassword)
                    {
                        Kupac.AccountBase.LoginInfo.Password = NoviPassword1;
                        db.SaveChanges();
                        db.Dispose();
                        TempData["success-key"] = "<br>Password uspjesno promjenjen<br>";
                        return View("EditProfile", Kupac);
                    }

                    TempData["error-key"] = "<br>Stari Password nije validan<br>";

                    return View("ChangePassword", Kupac);
                }
                else
                {
                    TempData["error-key"] = "<br>Greska u sistemu,pokusajte kasnije<br>";
                    return RedirectToAction("/Kupac/Index");
                }
            }
            else
            {
                Developer developer = db.Developer.Where(d=>d.DeveloperID == ID)
               .Include(o => o.AccountBase)
                 .ThenInclude(l => l.LoginInfo)
               .Include(o => o.AccountBase)
                 .ThenInclude(e => e.EmailAddress)
               .Include(o => o.AccountBase)
                   .ThenInclude(d => d.Drzava)
                   .FirstOrDefault();
                if (developer != null)
                {

                    if (developer.AccountBase.LoginInfo.Password == StariPassword)
                    {
                        developer.AccountBase.LoginInfo.Password = NoviPassword1;
                        db.SaveChanges();
                        db.Dispose();
                        TempData["success-key"] = "<br>Password uspjesno promjenjen<br>";
                        return View("EditProfile", developer);
                    }

                    TempData["error-key"] = "<br>Stari Password nije validan<br>";
                    return View("ChangePassword", developer);
                }
                else
                {
                    TempData["error-key"] = "<br>Greska u sistemu,pokusajte kasnije<br>";
                    return RedirectToAction("/Developer/Index");
                }
            }
        }

        public bool IsEmailValid(int BaseId, string Email)
        {
            EmailAddress EmailAddress = db.EmailAddress.Where(ea => ea.Email == Email && ea.BaseID!= BaseId).FirstOrDefault();
            if (EmailAddress != null)
                return false;
            return true;
        }
        public bool IsAccountNameValid(int BaseId,string AccountName)
        {
            LoginInfo LoginInfo = db.LoginInfo.Where(li => li.AccountName == AccountName && li.BaseID!= BaseId).FirstOrDefault();
            if (LoginInfo != null)
                return false;
            return true;
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool DodajSliku(int OsobaID=0, IFormFile Image = null)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            

            if (Log.BaseID == 0 || Image == null)
            {
                return false;
            }
            else
            {
                FileManagment FileManagment = new FileManagment();
                return FileManagment.DodajSliku(0, OsobaID, Image);
            }

        }



        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult WalletHistory(int KupacID)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac kupac = db.Kupac.Where(k => k.BaseID == Log.BaseID)
                .Include(k=>k.Wallet)
                .FirstOrDefault();

            List<WalletHistory> wh = db.WalletHistory.Where(w => w.WalletID == kupac.Wallet.WalletID)
                .Include(w=>w.Igra)
                 .ToList();
            ViewBag.w = kupac.WalletID;
            ViewBag.wh = wh;
            ViewBag.k = kupac;


                return View();
        }

        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool RequestRefund(int KupacID,int IgraID,string text)
        {
            Refund refund = db.Refund.Where(k => k.KupacID == KupacID && k.IgraID==IgraID).FirstOrDefault();

            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac kupac = db.Kupac.Where(k => k.BaseID == Log.BaseID).FirstOrDefault();
            
            if (refund != null)
                return false;
            refund = new Refund() {
                KupacID = kupac.KupacID,
                IgraID=IgraID,
                RazlogRefunda=text,
                VrijemeZahtijeva=DateTime.Now,
                VrijemeKupovine=DateTime.Now,
                VrijemeOdgovora=DateTime.Now
            };

            db.Refund.Add(refund);
            db.SaveChanges();
            db.Dispose();
            return true;

           
        }

    } 
}

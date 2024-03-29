﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.EF;
using Microsoft.AspNetCore.Mvc;
using eStore_web.Common;
using Microsoft.AspNetCore.Http;
using eStore_web.EF_Models;
using Microsoft.EntityFrameworkCore;
using eStore_web.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eStore_web.Controllers
{
    public class AutentifikacijaController : Controller
    {
        eContext db = new eContext();
       
      
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterSelect()
        {
            return View();
        }
        public IActionResult Register(int tip)
        {
            ViewData["tipKorisnika"] = tip;
            RegisterVM registerVM = new RegisterVM {
                Drzava = db.Drzava.Select(x => new SelectListItem { 
                    Value=x.DrzavaID.ToString(),
                    Text=x.Naziv
                }).ToList()
            };
            return View(registerVM);
        }
        public IActionResult RegisterSet(string Email, string AccountName, int TipRacuna, string NoviPassword1, int DrzavaId, string Sjediste)
        {
            if (TipRacuna == 1)
            {
                Kupac Kupac = new Kupac()
                {
                    UserName = AccountName,
                    PretplacenNaPremium=true//privremeno su svi pretplaceni na premium
                };
                Kupac.AccountBase = new AccountBase()
                {
                    Drzava = db.Drzava.Find(DrzavaId),
                    DrzavaID = DrzavaId,
                    EmailAddress = new EmailAddress()
                    {
                        Email = Email
                    }
                };
                Kupac.AccountBase.LoginInfo = new LoginInfo()
                {
                    AccountName = AccountName,
                    Password = NoviPassword1,
                    TipKorisnika = 1
                };
                Kupac.Wallet = new Wallet()
                {
                    balance = 950//privremeno svi imaju nesto walleta..

                };
                db.Kupac.Add(Kupac);
                db.SaveChanges();

                return RedirectToAction("LoginSet", new { AccountName, password=NoviPassword1 });
            }
            if (TipRacuna == 2)
            {
                Developer Developer = new Developer();

                Developer.AccountBase = new AccountBase {
                    Drzava = db.Drzava.Find(DrzavaId),
                    DrzavaID = DrzavaId,
                    EmailAddress = new EmailAddress { Email = Email }
                };
                Developer.AccountBase.LoginInfo = new LoginInfo()
                {
                    AccountName = AccountName,
                    Password = NoviPassword1,
                    TipKorisnika = 2
                };
                if (!string.IsNullOrEmpty(Sjediste))
                {
                    Developer.LokacijaAdresa = Sjediste;
                }
                Developer.NazivKompanije = AccountName;
                db.Developer.Add(Developer);
                db.SaveChanges();

                return RedirectToAction("LoginSet", new { AccountName, NoviPassword1 });
            }

            TempData["error-key"] = "<br>Greska pri registraciji";
            return View("Register");
        }
        public IActionResult Login()
        {

            return View("Login");
        }
        public  IActionResult LoginSet(string AccountName, string password)
        {
            LoginInfo Log = db.LoginInfo.Where(li => li.AccountName == AccountName && li.Password == password)
                .FirstOrDefault();

            

            if (Log != null)
            {
                HttpContext.Session.SetObjectAsJson("LoggedUser", Log);
                HttpContext.Session.SetString("LoggedName", Log.AccountName);

                if (Log.TipKorisnika==1)
                    return RedirectToAction("Index", "Kupac");
                if (Log.TipKorisnika == 2)
                    return RedirectToAction("ViewGames", "Developer");
                if (Log.TipKorisnika == 3)
                    return RedirectToAction("Index", "Administrator");
                else return null;
            }
            else
            {
                TempData["error-key"] = "<br>account name ili password pogresan";
                return View("Login");
            }
        }
       
        public IActionResult LogOut()
        {
            HttpContext.Session.SetObjectAsJson("LoggedUser",null);


            return RedirectToAction("Index", "Kupac");
        }
    }
}

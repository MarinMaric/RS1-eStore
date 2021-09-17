using eStore_web.Common;
using eStore_web.EF;
using eStore_web.EF_Models;
using eStore_web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.Controllers
{
    public class AutentifikacijaAngularController : Controller
    {
        eContext db = new eContext();

        [HttpPost]
        public IActionResult Login([FromBody]LoginAngularVM login)
        {
            var user = db.LoginInfo.Where(x => x.AccountName == login.username && x.Password == login.password).FirstOrDefault();
            if(user == null || !db.Developer.Any(x => x.BaseID == user.BaseID))
            {
                return Unauthorized();
            }
            string tokenString = TokenGenerator.Generate(50);

            db.Add(new AutentifikacijaToken
            {
                KorisnickiNalogId = user.LoginInfoID,
                VrijemeEvidentiranja = DateTime.Now,
                Vrijednost = tokenString
            });
            db.SaveChanges();
            return Ok(new AutentifikcijaLoginResultVM
            {
                tokenString = tokenString,
                username = user.AccountName
            });
        }
    }
}

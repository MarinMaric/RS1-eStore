using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eStore_web.Common
{
    public static class Autentifikacija
    {
        private static eContext db = new eContext();


        public static LoginInfo GetLogiraniKorisnik(this HttpContext context)
        {
           
            LoginInfo Log=context.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            return Log;
           
        }
        public static T GetLogiraniKorisnikAll<T>(this HttpContext context)
        {

            LoginInfo Log = context.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            if (Log != null)
            {
                if (Log.TipKorisnika == 1)
                {
                    Kupac Kupac = db.Kupac.Where(k => k.BaseID == Log.BaseID)
                        .Include(o => o.AccountBase)
                        .ThenInclude(e => e.EmailAddress)
                        .Include(o => o.AccountBase)
                        .ThenInclude(li => li.LoginInfo)
                        .Include(w=>w.Wallet)
                        .Include(o=>o.AccountBase)
                        .ThenInclude(oi=>oi.Image)
                        .FirstOrDefault();
                    return (T)Convert.ChangeType(Kupac, typeof(T));

                }
                if (Log.TipKorisnika == 2)
                {
                    Developer Developer = db.Developer.Where(d => d.BaseID == Log.BaseID)
                        .Include(d => d.AccountBase)
                        .ThenInclude(d => d.LoginInfo)
                        .FirstOrDefault();
                    return (T)Convert.ChangeType(Developer, typeof(T));
                }
                if (Log.TipKorisnika == 3)
                {
                    Administrator Administrator = db.Administrator.Where(a => a.BaseID == Log.BaseID)
                        .Include(o => o.AccountBase)
                        .ThenInclude(e => e.EmailAddress)
                        .Include(o => o.AccountBase)
                        .ThenInclude(li => li.LoginInfo)
                        .FirstOrDefault();
                    return (T)Convert.ChangeType(Administrator, typeof(T));
                }




            }
            return (T)Convert.ChangeType(Log, typeof(T));

        }
    }
}

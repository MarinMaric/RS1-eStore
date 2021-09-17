using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.Common
{
    public static class MyAuthTokenExtension
    {

        public static LoginInfo GetKorisnikOfAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            return GetKorisnikOfAuthToken(token);
        }

        public static LoginInfo GetKorisnikOfAuthToken(string token)
        {
            eContext db = new eContext();

            LoginInfo korisnickiNalog = db.AutentifikacijaToken.Where(x => token != null && x.Vrijednost == token).Select(s => s.KorisnickiNalog).SingleOrDefault();
            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["MojAutentifikacijaToken"];
            return token;
        }
    }
}

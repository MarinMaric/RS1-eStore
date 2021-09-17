using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.Common
{
    public class FileManagment
    {


        public bool ProvjeriPostojanje(int IgraID = 0, int BaseId = 0, IFormFile Image = null,eContext db=null)
        {
            
            if(Image!=null)
            {
                if(BaseId!=0 && IgraID==0)
                {
                    if (db.AccountImages.Where(oi => oi.BaseID == BaseId).FirstOrDefault() != null)
                        return true;
                    return false;

                }
                if(BaseId==0 && IgraID!=0)
                {
                    if (db.IgricaImage.Where(ii => ii.IgraID == IgraID).FirstOrDefault() != null)
                        return true;
                    return false;


                }
            }


            return false;
        }
        //Uradjeno samo za osobu treba doradit za kupca
        public bool DodajSliku(int IgraID = 0, int BaseId = 0, IFormFile Image=null, eContext db=null)
        {
            if(db==null)
            {
                db = new eContext();
            }
            if (Image != null)
            {
                if (Image.Length > 0)
                {
                    using (var fs1 = Image.OpenReadStream())

                        if (BaseId != 0 && IgraID == 0)
                        {

                            byte[] p1 = null;


                            using (var ms1 = new MemoryStream())
                            {
                                AccountBase accountBase = db.AccountBase.Where(o => o.AccountBaseID == BaseId).FirstOrDefault();
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                                if (!ProvjeriPostojanje(0, BaseId, Image, db))
                                {
                                    AccountImage AccountImage = new AccountImage
                                    {
                                        BaseID = BaseId,
                                        Image = p1
                                    };
                                    db.AccountImages.Add(AccountImage);
                                    accountBase.Image = AccountImage;
                                }
                                else
                                {
                                    accountBase.Image.Image = p1;
                                }




                                db.SaveChanges();
                                
                                return true;
                            }
                        }


                        else
                        {

                            if (BaseId == 0 && IgraID != 0)
                            {
                                byte[] p1 = null;
                                using (var ms1 = new MemoryStream())
                                {
                                    Igra Igra = db.Igra.Where(i => i.IgraID == IgraID).FirstOrDefault();
                                    fs1.CopyTo(ms1);
                                    p1 = ms1.ToArray();
                                    if (!ProvjeriPostojanje(IgraID, 0, Image, db))
                                    {
                                        IgricaImage i1 = new IgricaImage
                                        {
                                            IgraID = IgraID,
                                            Image = p1
                                        };
                                        Igra.IgricaImage = i1;
                                        db.IgricaImage.Add(i1);
                                    }
                                    else
                                    {
                                        Igra.IgricaImage.Image = p1;
                                    }

                                    db.SaveChanges();
                                    db.Dispose();
                                    return true;
                                }
                            }

                        }
                }
            }

            return false;
        }

    }
}

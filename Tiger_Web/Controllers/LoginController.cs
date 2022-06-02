using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiger_Web.Models;

namespace Tiger_Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
    
        public ActionResult Login()
        {            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Giris(Kullanici kul)
        {
            ResponseObject ro = new ResponseObject();
            try
            {               
                DataModel dm = new DataModel();            
                Kullanici us = new Kullanici();
                 us = dm.Kullanici.Where(q => q.KullaniciAdi == kul.KullaniciAdi && q.Sifre == kul.Sifre && q.AktifMi == true).FirstOrDefault();
                if (us.AktifMi==true)
                {
                    ro.Sonuc = true;
                    ro.Cevap = "Ok";
                    Sessions.Bilgi = new MySession();
                    Sessions.Bilgi.Login = true;
                    Sessions.Bilgi.Ad = us.Ad;
                    Sessions.Bilgi.Soyad = us.Soyad;
                    Sessions.Bilgi.KullaniciNo = us.No;
                }
                else
                {
                    ro.Cevap = "Yanlış Giriş";
                }
            }
            catch (Exception ex)
            {
                ro.Cevap = "HATA : " + ex.ToString();
            }

            return Json(ro);
        }
    }
}
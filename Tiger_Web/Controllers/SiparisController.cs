using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiger_Web.Functions;
using Tiger_Web.Models;

namespace Tiger_Web.Controllers
{
    public class SiparisController : Controller
    {
        // GET: Siparis
        public ActionResult Siparis()
        {
            return View();
        }
        public PartialViewResult GenelPartial()
        {
            string Key = "";
            long No = 0;
            DataModel dm = new DataModel();
            if (RouteData.Values["id"] != null)
            {
                Key = RouteData.Values["id"].ToString();
                No = Key.ToInt32();
            }
            sFilter res = new sFilter()
            {
                Key = Key,
                No = No,
            };


            if (Request.QueryString["partial"] == "_SiparisEdit")
            {
                Siparis x = new Siparis();
                if (res.No > 0)
                {
                    x = dm.Siparis.Where(q => q.No == res.No).FirstOrDefault();
                }
                else
                {
                    x.No = 0;
                    foreach (var item in dm.SiparisDetay.Where(q => q.SiparisNo == null && q.KullaniciNo == Sessions.Bilgi.KullaniciNo).ToList())
                    {
                        dm.SiparisDetay.Remove(item);
                    }
                    dm.SaveChanges();
                }
                return PartialView("partial/" + Request.QueryString["partial"], x);
            }
            else if (Request.QueryString["partial"] == "_sDetayEdit")
            {
                long uNo = Request.QueryString["uNo"].ToInt32();
                SiparisDetay x = new SiparisDetay();
                if (uNo == 0)
                {
                    x.SiparisNo = res.No;
                    x.No = 0;
                    x.Miktar = 1;
                    x.BirimFiyat = 1;
                }
                else
                {
                    x = dm.SiparisDetay.Where(q => q.No == uNo).FirstOrDefault();
                }
                return PartialView("partial/" + Request.QueryString["partial"], x);
            }

            else if (Request.QueryString["partial"] == "_sDetayList")
            {
                Siparis x = new Siparis();
                if (Key.ToInt32() == 0)
                {
                    x.No = 0;
                }
                else
                {
                    x.No = Key.ToInt32();
                }
                return PartialView("partial/" + Request.QueryString["partial"], x);
            }        
            else
                return PartialView("partial/" + Request.QueryString["partial"], res);
        }

        #region Siparis
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SiparisEdit(Siparis po)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            Siparis s = new Siparis();
            if (po.No != 0)
            {
                s = dm.Siparis.Where(sp => sp.No == po.No).FirstOrDefault();
            }
            s.CariNo = po.CariNo;
            s.SiparisTarihi = po.SiparisTarihi;
            s.AktifMi = true;
            s.KullaniciNo = Sessions.Bilgi.KullaniciNo;
            if (po.No == 0)
            {
                dm.Siparis.Add(s);
            }
            try
            {
                foreach (var item in dm.SiparisDetay.Where(q => q.SiparisNo == null && q.KullaniciNo == Sessions.Bilgi.KullaniciNo).ToList())
                {
                    item.Siparis = s;
                }
                dm.SaveChanges();
                if (po.No == 0)
                {
                    s.Kod = DateTime.Now.ToString("yyyy") + s.No.ToString("00000");
                }  
                dm.SaveChanges();
                ro.Sonuc = true;
                ro.Cevap = "Ok";
            }
            catch (Exception ex)
            {
                ro.Cevap = ex.Message;
            }
            return Json(ro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SiparisDelete(long No)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            var s = dm.Siparis.Where(sp => sp.No == No).FirstOrDefault();
            if (s != null)
            {
                foreach (var item in s.SiparisDetay.ToList())
                {
                    dm.SiparisDetay.Remove(item);
                }
                dm.Siparis.Remove(s);
            }
            try
            {
                dm.SaveChanges();
                ro.Sonuc = true;
                ro.Cevap = "Siparis Silindi.";
            }
            catch (Exception ex)
            {
                ro.Cevap = ex.Message;
            }
            return Json(ro);
        }
        #endregion
        #region SiparisDetay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult sDetayEdit(SiparisDetay po)
        {

            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            SiparisDetay sd = new SiparisDetay();
            if (po.No != 0)
            {
                sd = dm.SiparisDetay.Where(q => q.No == po.No).FirstOrDefault();
            }
            if (po.SiparisNo > 0)
            {
                sd.SiparisNo = po.SiparisNo;
            }
            sd.BirimFiyat = po.BirimFiyat;
            sd.Miktar = po.Miktar;
            sd.KullaniciNo = Sessions.Bilgi.KullaniciNo;
            sd.UrunNo = po.UrunNo;
            if (po.No == 0)
            {
                dm.SiparisDetay.Add(sd);
            }
            try
            {
                dm.SaveChanges();
                ro.Sonuc = true;
                ro.Cevap = "Ok";
            }
            catch (Exception ex)
            {
                ro.Cevap = ex.Message;
            }
            return Json(ro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult sDetayDelete(string No)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            var sd = dm.SiparisDetay.AsEnumerable().Where(s => s.No == No.ToInt32()).FirstOrDefault();
            if (sd != null)
                dm.SiparisDetay.Remove(sd);
            try
            {
                dm.SaveChanges();
                ro.Sonuc = true;
                ro.Cevap = "Silindi";
            }
            catch (Exception ex)
            {
                ro.Cevap = ex.Message;
            }
            return Json(ro);
        }
        #endregion

    }
}
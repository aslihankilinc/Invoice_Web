using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiger_Web.Functions;
using Tiger_Web.Models;

namespace Tiger_Web.Controllers
{
    public class FaturaController : Controller
    {
        public ActionResult Fatura()
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


            if (Request.QueryString["partial"] == "_FaturaEdit")
            {
                Fatura x = new Fatura();
                if (res.No > 0)
                {
                    x = dm.Fatura.Where(q => q.No == res.No).FirstOrDefault();
                }
                else
                {
                    x.No = 0;
                    foreach (var item in dm.FaturaDetay.Where(q => q.FaturaNo == null && q.KullaniciNo == Sessions.Bilgi.KullaniciNo).ToList())
                    {
                        dm.FaturaDetay.Remove(item);
                    }
                    dm.SaveChanges();
                }
                return PartialView("partial/" + Request.QueryString["partial"], x);
            }
            else if (Request.QueryString["partial"] == "_fDetayEdit")
            {
                long uNo = Request.QueryString["uNo"].ToInt32();
                FaturaDetay x = new FaturaDetay();
                if (uNo == 0)
                {
                    x.FaturaNo = res.No;
                    x.No = 0;
                    x.Miktar = 1;
                    x.BirimFiyat = 1;
                }
                else
                {
                    x = dm.FaturaDetay.Where(q => q.No == uNo).FirstOrDefault();
                }
                return PartialView("partial/" + Request.QueryString["partial"], x);
            }

            else if (Request.QueryString["partial"] == "_fDetayList")
            {
                Fatura x = new Fatura();
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
            else if (Request.QueryString["partial"] == "_fSiparisList") 
             {
                Siparis_Fatura x = new Siparis_Fatura();
                x.FaturaNo = res.No;
                x.Fatura = dm.Fatura.Where(s=>s.No==res.No).FirstOrDefault();
                return PartialView("partial/" + Request.QueryString["partial"], x);
            }
            else
                return PartialView("partial/" + Request.QueryString["partial"], res);
        }
        #region Fatura
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult FaturaEdit(Fatura po)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            Fatura f = new Fatura();

            if (po.No != 0)
            {
                f = dm.Fatura.Where(s => s.No == po.No).FirstOrDefault();

            }
            f.CariNo = po.CariNo;
            f.Tarih = DateTime.Now;
            f.FaturaTarihi = po.Tarih;
            f.AktifMi = true;
            f.KullaniciNo = Sessions.Bilgi.KullaniciNo;
            if (po.No == 0)
            {
                dm.Fatura.Add(f);
            }
            try
            {

                foreach (var item in dm.FaturaDetay.Where(q => q.FaturaNo == null && q.KullaniciNo == Sessions.Bilgi.KullaniciNo).ToList())
                {
                    item.Fatura = f;
                }
                dm.SaveChanges();
                if (po.No == 0)
                {
                    f.FisNumarasi = DateTime.Now.ToString("yyyy") + f.No.ToString("000000000");
                }
                f.NetTutar = 0;
                f.ToplamKdvTutari = 0;
                f.ToplamTutar = 0;
                foreach (var item in dm.FaturaDetay.Where(q => q.FaturaNo == f.No).ToList())
                {
                    f.NetTutar += (item.Miktar * item.BirimFiyat);
                    f.ToplamKdvTutari += ((item.Miktar * item.BirimFiyat) * item.Urun.Kdv / 100);
                    f.ToplamTutar += ((item.Miktar * item.BirimFiyat) * (100 + item.Urun.Kdv) / 100);
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
        public JsonResult FaturaDelete(long No)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            var f = dm.Fatura.Where(s => s.No == No).FirstOrDefault();
            if (f != null)
            {
                foreach (var item in f.FaturaDetay.ToList())
                {
                    dm.FaturaDetay.Remove(item);
                }
                dm.Fatura.Remove(f);
            }
            try
            {
                dm.SaveChanges();
                ro.Sonuc = true;
                ro.Cevap = "Fatura Silindi.";
            }
            catch (Exception ex)
            {
                ro.Cevap = ex.Message;
            }
            return Json(ro);
        }
        #endregion       
        #region Detay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult fDetayEdit(FaturaDetay po)
        {

            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            FaturaDetay fd = new FaturaDetay();
            if (po.No != 0)
            {
                fd = dm.FaturaDetay.Where(q => q.No == po.No).FirstOrDefault();
            }
            if (po.FaturaNo > 0)
            {
                fd.FaturaNo = po.FaturaNo;
            }
            fd.BirimFiyat = po.BirimFiyat;
            fd.Miktar = po.Miktar;
            fd.KullaniciNo = Sessions.Bilgi.KullaniciNo;
            fd.UrunNo = po.UrunNo;
            if (po.No == 0)
            {
                dm.FaturaDetay.Add(fd);
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
        public JsonResult fDetayDelete(string No)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            var fd = dm.FaturaDetay.AsEnumerable().Where(s => s.No == No.ToInt32()).FirstOrDefault();
            if (fd != null)
                dm.FaturaDetay.Remove(fd);
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
        #region Fatura_Siparis
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult sfCon(Siparis_Fatura po)
        {

            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            Siparis_Fatura sf = new Siparis_Fatura();
            sf.FaturaNo = po.FaturaNo;
            sf.SiparisNo = po.SiparisNo;
            var sd = dm.SiparisDetay.Where(s => s.SiparisNo == po.SiparisNo).ToList();
            try
            {
                if (po.No == 0)
                {
                    dm.Siparis_Fatura.Add(sf);
                    dm.SaveChanges();
                    foreach (var item in sd)
                    {
                        dm.FaturaDetay.Add(new FaturaDetay
                        {
                            FaturaNo = po.FaturaNo,
                            UrunNo = item.UrunNo,
                            BirimFiyat = item.BirimFiyat,
                            Miktar = item.Miktar,
                            KullaniciNo = Sessions.Bilgi.KullaniciNo,
                        });
                    }
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
        #endregion
    }
}

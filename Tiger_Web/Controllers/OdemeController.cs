using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiger_Web.Functions;
using Tiger_Web.Models;

namespace Tiger_Web.Controllers
{
    public class OdemeController : Controller
    {
        // GET: Odeme
        public ActionResult Odeme()
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
            if (Request.QueryString["partial"] == "_OdemeEdit")
            {
                Odeme x = new Odeme();
                if (res.No == 0)
                    x.No = 0;
                else
                    x = dm.Odeme.Where(s => s.No == res.No).FirstOrDefault();
                return PartialView("partial/" + Request.QueryString["partial"], x);
            }

            else
                return PartialView("partial/" + Request.QueryString["partial"], res);
        }

        //OdemeEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OdemeEdit(Odeme po)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            Odeme o = new Odeme();
            Fatura f = new Fatura();
            Fatura_Odeme fo = new Fatura_Odeme();
            decimal kalanTutar = 0, okTutar = 0;
            okTutar = po.Tutar;
            try
            {
                if (po.No != 0)
                {
                    o = dm.Odeme.Where(s => s.No == po.No).FirstOrDefault();
                    foreach (var item in dm.Fatura_Odeme.Where(s => s.OdemeNo >= po.No && s.Fatura.CariNo == po.CariNo).ToList())
                    {
                        f = dm.Fatura.Where(s => s.No == item.FaturaNo).FirstOrDefault();
                        f.AktifMi = true;
                        dm.Fatura_Odeme.Remove(item);
                    }
                    dm.SaveChanges();
                }
                o.CariNo = po.CariNo;
                o.Tutar = po.Tutar;
                o.KullaniciNo = Sessions.Bilgi.KullaniciNo;
                o.Tarih = DateTime.Now;
                if (po.No == 0)
                {
                    dm.Odeme.Add(o);
                    dm.SaveChanges();
                }

                foreach (var item in dm.Fatura.Where(s => s.AktifMi == true && s.CariNo == po.CariNo).OrderBy(s => s.No).ToList())
                {
                    kalanTutar = (item.ToplamTutar - okTutar).ToDecimal();
                    okTutar = kalanTutar * (-1);
                    if (kalanTutar > 0)
                    {
                        okTutar = 0;
                    }
                    else if (kalanTutar == 0)
                    {
                        item.AktifMi = false;
                    }
                    else
                    {
                        item.AktifMi = false;
                        kalanTutar = 0;
                    }
                    fo.FaturaNo = item.No;
                    fo.OdemeNo = o.No;
                    fo.KalanTutar = kalanTutar.ToDecimal();
                    fo.OdenenTutar = (item.ToplamTutar - kalanTutar).ToDecimal();
                    fo.Tarih = DateTime.Now;
                    dm.Fatura_Odeme.Add(fo);
                    dm.SaveChanges();
                    if (okTutar == 0) break;
                }

                //dm.SaveChanges();
                ro.Sonuc = true;
                ro.Cevap = "Ok";
            }
            catch (Exception ex)
            {
                ro.Cevap = ex.Message;
            }
            return Json(ro);
        }
        //OdemeDelete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OdemeDelete(int No)
        {
            DataModel dm = new DataModel();
            ResponseObject ro = new ResponseObject();
            Fatura f = new Fatura();
            Fatura_Odeme fod = new Fatura_Odeme();
            var o = dm.Odeme.Where(s => s.No == No).FirstOrDefault();
            if (o != null)
            {
                var fo = dm.Fatura_Odeme.Where(s => s.OdemeNo >= No && s.Fatura.CariNo == o.CariNo).ToList();
                //if (fo != null)
                //{
                //    foreach (var item in fo)
                //    {
                //        f = dm.Fatura.Where(s => s.No == item.FaturaNo).FirstOrDefault();
                //        f.AktifMi = true;
                //        dm.Fatura_Odeme.Remove(item);
                //    }
                //    //dm.SaveChanges();
                //    var newO = dm.Odeme.Where(s => s.No > No && s.CariNo == o.CariNo).ToList();
                //    decimal kalanTutar = 0, okTutar = 0;
                //    foreach (var no in newO)
                //    {
                //        okTutar = no.Tutar;
                //        foreach (var t in dm.Fatura.Where(s => s.AktifMi == true && s.CariNo == o.CariNo))
                //        {
                //            kalanTutar = (t.ToplamTutar - okTutar).ToDecimal();
                //            okTutar = kalanTutar * (-1);
                //            if (kalanTutar > 0)
                //            {
                //                okTutar = 0;
                //            }
                //            else if (kalanTutar == 0)
                //            {
                //                t.AktifMi = false;
                //            }
                //            else
                //            {
                //                t.AktifMi = false;
                //                kalanTutar = 0;
                //            }
                //            fod.FaturaNo = t.No;
                //            fod.OdemeNo = no.No;
                //            fod.KalanTutar = kalanTutar.ToDecimal();
                //            fod.OdenenTutar = (f.ToplamTutar - kalanTutar).ToDecimal();
                //            fod.Tarih = DateTime.Now;
                //            dm.Fatura_Odeme.Add(fod);
                //            dm.SaveChanges();
                //            if (okTutar == 0) break;
                //        }
                //    }

                //}
                dm.Odeme.Remove(o);
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
    }
}
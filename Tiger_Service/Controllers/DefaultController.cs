using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using Tiger_Web;
using Tiger_Service.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.JsonResult;
using Tiger_Web.Functions;

namespace Tiger_Service.Controllers
{
    public class DefaultController : ApiController
    {

        [HttpGet]
        public object getFatura()
        {
            try
            {
                DataModel dm = new DataModel();
                List<mFatura> list = new List<mFatura>();
                foreach (var item in dm.Fatura.ToList())
                {
                    list.Add(new mFatura
                    {
                        No = item.No,
                        FaturaTarihi = Convert.ToDateTime(item.FaturaTarihi),
                        FisNumarasi = item.FisNumarasi,
                        Cari = item.Cari.Unvan,
                        NetTutar = Convert.ToDecimal(item.NetTutar),
                    });
                }
                string lis = JsonConvert.SerializeObject(list);
                return JsonConvert.DeserializeObject(lis, typeof(object));
            }
            catch (Exception ex)
            {
                return "[{\"Hata\": \"" + ex.ToString() + "\"}]";
            }

        }
        [HttpGet]
        public object getFaturaDetay([FromUri] int No)
        {

            try
            {
                DataModel dm = new DataModel();

                List<mFaturaDetay> list = new List<mFaturaDetay>();
                foreach (var item in dm.FaturaDetay.Where(s => s.FaturaNo == No).ToList())
                {
                    list.Add(new mFaturaDetay
                    {
                        No = item.No,
                        BirimFiyat = Convert.ToDecimal(item.BirimFiyat),
                        Miktar = Convert.ToDecimal(item.Miktar),
                        UrunAd = item.Urun.Ad,

                    });
                }
                string lis = JsonConvert.SerializeObject(list);
                return JsonConvert.DeserializeObject(lis, typeof(object));
            }
            catch (Exception ex)
            {
                return "[{\"Hata\": \"" + ex.ToString() + "\"}]";
            }
        }
        [HttpGet]
        public object setFaturaDetay([FromUri] int FaturaNo, int UrunNo, double BirimFiyat, double Miktar)
        {
            mResult result = new mResult();
            try
            {
                DataModel dm = new DataModel();
                FaturaDetay f = new FaturaDetay();
                f.FaturaNo = FaturaNo;
                f.UrunNo = UrunNo;
                f.BirimFiyat = Convert.ToDecimal(BirimFiyat);
                f.Miktar = Convert.ToDecimal(Miktar);
                dm.FaturaDetay.Add(f);
                dm.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Result = "Basarisiz";
            }
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result), typeof(object));
        }
       
        [HttpPost]
        public object postFaturaDetay([FromBody] mFaturaDetay s)
        {
            mResult result = new mResult();
            try
            {
                DataModel dm = new DataModel();
                FaturaDetay f = new FaturaDetay();
                f.FaturaNo = s.FaturaNo;
                f.UrunNo = s.UrunNo;
                f.BirimFiyat = Convert.ToDecimal(s.BirimFiyat);
                f.Miktar = Convert.ToDecimal(s.Miktar);
                dm.FaturaDetay.Add(f);
                dm.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Result = "Basarisiz";
            }
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result), typeof(object));
        }

        [HttpGet]
        public object getUrun()
        {
            try
            {
                DataModel dm = new DataModel();
                List<mUrun> list = new List<mUrun>();
                foreach (var item in dm.Urun.ToList())
                {
                    list.Add(new mUrun
                    {
                        No = item.No,
                        Ad = item.Ad,
                        Kdv = item.Kdv.ToDouble(),
                        Birim = item.Birim.Ad,
                    });
                }
                string lis = JsonConvert.SerializeObject(list);
                return JsonConvert.DeserializeObject(lis, typeof(object));
            }
            catch (Exception ex)
            {
                return "[{\"Hata\": \"" + ex.ToString() + "\"}]";
            }
        }

    }
}

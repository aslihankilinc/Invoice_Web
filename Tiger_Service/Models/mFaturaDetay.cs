using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tiger_Service.Models
{
  
    public class mFaturaDetay
    {
        public long No { get; set; }
        public long FaturaNo { get; set; }
        public long UrunNo { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public int KullaniciNo { get; set; }
        public string UrunAd { get;  set; }
    }
}
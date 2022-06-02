using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiger_Service.Models
{
    public class sFatura
    {
        public long No { get; set; }

        public string FisNumarasi { get; set; }

        public string Cari { get; set; }

        public DateTime? FaturaTarihi { get; set; }

        public int CariNo { get; set; }

        public decimal NetTutar { get; set; }

        public decimal ToplamKdvTutari { get; set; }

        public decimal ToplamTutar { get; set; }

        public int KullaniciNo { get; set; }

        public DateTime Tarih { get; set; }

        public bool AktifMi { get; set; }

    }

}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tiger_Win
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fatura
    {
        public long No { get; set; }
        public string FisNumarasi { get; set; }
        public Nullable<System.DateTime> FaturaTarihi { get; set; }
        public Nullable<int> CariNo { get; set; }
        public Nullable<decimal> NetTutar { get; set; }
        public Nullable<decimal> ToplamKdvTutari { get; set; }
        public Nullable<decimal> ToplamTutar { get; set; }
        public Nullable<int> KullaniciNo { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<bool> AktifMi { get; set; }
    }
}
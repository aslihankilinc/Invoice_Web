//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tiger_Web
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiparisDetay
    {
        public long No { get; set; }
        public Nullable<long> SiparisNo { get; set; }
        public long UrunNo { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public int KullaniciNo { get; set; }
    
        public virtual Kullanici Kullanici { get; set; }
        public virtual Siparis Siparis { get; set; }
        public virtual Urun Urun { get; set; }
    }
}
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
    
    public partial class Odeme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Odeme()
        {
            this.Fatura_Odeme = new HashSet<Fatura_Odeme>();
        }
    
        public int No { get; set; }
        public int CariNo { get; set; }
        public decimal Tutar { get; set; }
        public int KullaniciNo { get; set; }
        public System.DateTime Tarih { get; set; }
    
        public virtual Cari Cari { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fatura_Odeme> Fatura_Odeme { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}

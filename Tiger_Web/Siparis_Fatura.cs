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
    
    public partial class Siparis_Fatura
    {
        public int No { get; set; }
        public long FaturaNo { get; set; }
        public long SiparisNo { get; set; }
    
        public virtual Fatura Fatura { get; set; }
        public virtual Siparis Siparis { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiger_Web.Models
{
    public class sModel
    {
    }
    public class ResponseObject
    {
        public string Cevap { get; set; } = "Hata";
        public bool Sonuc { get; set; } = false;
    }
    [Serializable]
    public class sFilter
    {
        public string Key { get; set; }
        public long No { get; set; }
    }
}
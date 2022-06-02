using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiger_Service.Models
{
    public class mUrun
    {
        public long No { get; set; }
        public string Ad { get; set; }
        public double Kdv { get; set; }
        public string Birim { get; set; }
        public int BirimNo { get; set; }
    }
}
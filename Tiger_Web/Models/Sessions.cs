using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiger_Web
{
    [Serializable]
    public class MySession
    {
        public bool Login { get; set; }
        public int KullaniciNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }

    }
    public class Sessions
    {
        public static MySession Bilgi
        {
            get { return HttpContext.Current.Session["abc2455"] as MySession; }
            set { HttpContext.Current.Session["abc2455"] = value; }
        }

    }
}
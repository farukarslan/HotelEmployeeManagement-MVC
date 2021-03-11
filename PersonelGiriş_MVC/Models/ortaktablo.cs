using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelGiriş_MVC.Models
{
    public class ortaktablo
    {

        public string p_isim { get; set; }
        public string p_soyisim { get; set; }
        public string p_girissaati { get; set; }
        public string p_cikissaati { get; set; }
        public List<ortaktablo> toplamtablo { get; set; }
    }
}

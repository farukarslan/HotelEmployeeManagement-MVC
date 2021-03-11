using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonelGiriş_MVC.Models
{
    public class saat
    {
        public int saatid { get; set; }
        public string girissaati { get; set; }
        public string cikissaati { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime? kayittarihi { get; set; }
        //her giriş/çıkış saati yanlızca bir ait olabilir.
        public virtual personel personel { get; set; }
    }
}
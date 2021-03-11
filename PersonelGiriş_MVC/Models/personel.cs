using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PersonelGiriş_MVC.Models
{
    public class personel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string isim { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string soyisim { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string tcno { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        [DataType(DataType.DateTime, ErrorMessage = "Lütfen doğum tarihinizi doğru bir şekilde giriniz.")]
        public int dogumtarihi { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string dogumyeri { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string cinsiyet { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string kangrubu { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string adres { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen telefon numaranızı doğru bir şekilde giriniz.")]
        public string telno { get; set; }
        public string resimismi { get; set; }
        public virtual List<saat> saatler { get; set; }

    }
}
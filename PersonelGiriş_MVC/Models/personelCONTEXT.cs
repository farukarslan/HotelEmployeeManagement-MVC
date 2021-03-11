using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PersonelGiriş_MVC.Models
{
    public class personelCONTEXT : DbContext
    {
        public personelCONTEXT() : base("sqlim")
        {

        }

        public DbSet<personel> personeller { get; set; }
        public DbSet<saat> saatler { get; set; }

    }
}
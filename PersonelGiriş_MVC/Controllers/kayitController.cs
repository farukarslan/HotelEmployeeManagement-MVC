using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelGiriş_MVC.Models;
using System.IO;
using PagedList;

namespace PersonelGiriş_MVC.Controllers
{
    public class kayitController : Controller
    {
        // GET: kayit
        public ActionResult Index()
        {
            return View();
        }
        //---------------------- PERSONEL KAYIT GİR---------------------//
        public ActionResult yenipersonelgir()
        {
            return View();
        }
        personelCONTEXT db = new personelCONTEXT();
        [HttpPost]
        public ActionResult yenipersonelgir(personel yenipersonel, HttpPostedFileBase gelendosya)
        {

            if (ModelState.IsValid && gelendosya != null)
            {
                string dosyaismial = gelendosya.FileName;
                if (dosyaismial.EndsWith("jpg") || dosyaismial.EndsWith("png"))
                {
                    yenipersonel.resimismi = dosyaismial;
                    db.personeller.Add(yenipersonel);
                    db.SaveChanges();
                    gelendosya.SaveAs(Server.MapPath("~/img/" + dosyaismial));
                    ViewBag.mesaj = "Kayıt Başarılı";
                    return RedirectToAction("yenipersonelList");
                }
                else
                {
                    ViewBag.mesaj = "Lütfen uygun dosya formatı seçiniz!";
                    return View();
                }
            }
            else
            {
                ViewBag.mesaj = "Kayıt Başarısız";
                return View(yenipersonel);
            }
        }

        //----------------------PERSONEL BİLGİ GÜNCELLE---------------------//
        public ActionResult personelguncel(int id)
        {

            var guncellenecek = db.personeller.Find(id);
            return View(guncellenecek);
        }
        [HttpPost]
        public ActionResult personelguncel(personel yenipersonel, HttpPostedFileBase gelendosya, int id)
        {
            if (ModelState.IsValid)
            {


                int idyegit = yenipersonel.id;
                var vericek = db.personeller.Find(idyegit);
                vericek.isim = yenipersonel.isim;
                vericek.id = yenipersonel.id;
                vericek.soyisim = yenipersonel.soyisim;
                vericek.tcno = yenipersonel.tcno;
                vericek.dogumtarihi = yenipersonel.dogumtarihi;
                vericek.dogumyeri = yenipersonel.dogumyeri;
                vericek.cinsiyet = yenipersonel.cinsiyet;
                vericek.kangrubu = yenipersonel.kangrubu;
                vericek.adres = yenipersonel.adres;
                vericek.telno = yenipersonel.telno;
                vericek.resimismi = yenipersonel.resimismi;

                var silenecek_kayit = (from sill in db.personeller
                                       where sill.id == id
                                       select sill).FirstOrDefault();

                string SilResName = silenecek_kayit.resimismi;
                System.IO.File.Delete(Server.MapPath("~/img/" + SilResName));

                string dosyaismial = gelendosya.FileName;
                vericek.resimismi = dosyaismial;

                db.SaveChanges();

                gelendosya.SaveAs(Server.MapPath("~/img/" + dosyaismial));
                ViewBag.mesaj = "Güncelleme Başarılı";
                return RedirectToAction("yenipersonelList");
            }
            else
            {
                ViewBag.mesaj = "Güncelleme Başarısız";
                return View();
            }

        }
        //----------------------PERSONEL LİSTELE---------------------//
        public ActionResult yenipersonelList(int? page)
        {
            personelCONTEXT db = new personelCONTEXT();
            var liste = db.personeller.ToList();
            return (View(liste.ToPagedList(pageNumber: page ?? 1, pageSize: 5)));
        }
        [HttpPost]
        public ActionResult yenipersonelList(int? id, int? page)
        {

            personelCONTEXT db = new personelCONTEXT();
            if (id == 1)
            {
                var veri = (from kayitlisteleri in db.personeller
                            orderby kayitlisteleri.isim
                            select kayitlisteleri).ToList();
                return (View(veri.ToPagedList(pageNumber: page ?? 1, pageSize: 5)));

            }
            else if (id == 2)
            {
                var veri = (from kayitlisteleri in db.personeller
                            orderby kayitlisteleri.soyisim
                            select kayitlisteleri).ToList();
                return (View(veri.ToPagedList(pageNumber: page ?? 1, pageSize: 5)));
            }
            else if (id == 3)
            {
                var veri = (from kayitlisteleri in db.personeller
                            orderby kayitlisteleri.dogumtarihi
                            select kayitlisteleri).ToList();
                return (View(veri.ToPagedList(pageNumber: page ?? 1, pageSize: 5)));
            }
            else
            {
                var veri = db.personeller.ToList();
                return (View(veri.ToPagedList(pageNumber: page ?? 1, pageSize: 5)));

            }
        }
        //----------------------PERSONEL DETAYLI LİSTELE---------------------//
        public ActionResult detaylıliste()
        {
            personelCONTEXT db = new personelCONTEXT();
            var liste = db.personeller.ToList();
            return View(liste);
        }
        //----------------------PERSONEL ARAMA---------------------//
        public ActionResult arama()
        {
            return View();
        }
        [HttpPost]
        public ActionResult arama(string aranan)
        {
            personelCONTEXT db = new personelCONTEXT();
            var tumveriler = (from kayit in db.personeller
                              where kayit.isim.Contains(aranan)
                              select kayit).ToList();
            return View(tumveriler);
        }
        //----------------------PERSONEL SİLME---------------------//
        public ActionResult sil(int id)
        {
            personelCONTEXT db = new personelCONTEXT();

            var silinecek = db.personeller.Find(id);
            db.personeller.Remove(silinecek);

            var secilenSaat = (from kayit in db.saatler
                               where kayit.personel.id == id
                               select kayit).FirstOrDefault();
            if (secilenSaat != null)
            {
                var silinecekSaat = db.saatler.Find(secilenSaat.saatid);
                db.saatler.Remove(silinecekSaat);
            }


            System.IO.File.Delete(Server.MapPath("~/img/" + silinecek.resimismi));
            db.SaveChanges();
            return RedirectToAction("yenipersonelList");
        }
        //----------------------------------------GİRİŞ/ÇIKIŞ SAATİ KAYDET------------------------------------------//
        [HttpGet]
        public ActionResult saatgir(int id)
        {
            ViewBag.veri = id;
            return View();
        }
        [HttpPost]
        public ActionResult saatgir(int id, string girissaati, string cikissaati, string gizlidosya)
        {
            ViewBag.veri = id;
            saat yenisaat = new saat();

            var personelid = (from per in db.personeller
                              where per.id == id
                              select per).FirstOrDefault();

            yenisaat.girissaati = girissaati;
            yenisaat.cikissaati = cikissaati;
            yenisaat.personel = personelid;
            yenisaat.kayittarihi = Convert.ToDateTime(gizlidosya);


            db.saatler.Add(yenisaat);
            db.SaveChanges();

            return RedirectToAction("saatList2");
        }
        public ActionResult saatgir2()
        {
            var liste = db.personeller.ToList();
            return View(liste);
        }
        //-------------------------GİRİŞ/ÇIKIŞ SAAT LİSTE------------------------------------//
        public ActionResult saatList2()
        {
            var saatKayitlari = (from kayit in db.saatler
                                 select kayit.kayittarihi).ToList().Distinct();
            ViewBag.saat = new SelectList(saatKayitlari, "saatid");
            return View();
        }

        [HttpPost]
        public ActionResult saatList2(string dropdanveri)
        {
            var saatKayitlari = (from kayit in db.saatler
                                 select kayit.kayittarihi).ToList().Distinct();
            ViewBag.saat = new SelectList(saatKayitlari, "saatid");

            var kayitTarih = Convert.ToDateTime(dropdanveri);
            var secim = (from saat_veri in db.saatler
                         join per_veri in db.personeller on saat_veri.personel.id equals per_veri.id
                         where saat_veri.kayittarihi == kayitTarih
                         select new ortaktablo { p_isim = per_veri.isim, p_soyisim = per_veri.soyisim, p_girissaati = saat_veri.girissaati, p_cikissaati = saat_veri.cikissaati });
            var Model = new ortaktablo { toplamtablo = secim.ToList() };
            return View(Model);
        }
    }
}
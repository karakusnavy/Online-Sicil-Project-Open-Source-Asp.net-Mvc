using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineSicil.Models;

namespace OnlineSicil.Controllers
{
    public class SicilController : Controller
    {       

        private bool SicilKayitGuvenlik(string adsoyad, string telefon, string eposta, string aciklama)
        {
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();

            foreach (var item in db.SicilKayitlari)
            {
                if ((item.adsoyad == adsoyad || item.firmaunvani == adsoyad) && item.telefon == telefon && item.eposta == eposta && item.aciklama == aciklama)
                {
                    return false;
                }
            }
            return true;
        }

        [Route("sicil-bildir")]
        [HttpGet]
        public ActionResult SicilBildir()
        {
            ViewBag.Baslik = "Sicil Bildir";
            return View();
        }
        // SİCİL BİLDİR GET
        [Authorize]
        [Route("isci-sicil-bildir")]
        [HttpGet]
        public ActionResult IsciSicilBildir()
        {
            ViewBag.Baslik = "İşçi Sicil Bildir";
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            ViewBag.Sehirler = db.City.ToList();

            return View();
        }
        [Authorize]
        [Route("isveren-sicil-bildir")]
        [HttpGet]
        public ActionResult IsverenSicilBildir()
        {
            ViewBag.Baslik = "İşveren Sicil Bildir";
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            ViewBag.Sehirler = db.City.ToList();

            return View();
        }
        //-------------------


        private bool DosyaTipi(string ContentType)
        {
            return ContentType.Equals("image/png") || ContentType.Equals("image/gif") || ContentType.Equals("image/jpg") || ContentType.Equals("image/jpeg");
        }
        private bool DosyaBoyutu(int ContentLenght)
        {
            return ((ContentLenght / 1024) / 1024) < 1;
        }

        //-- Sicil Bildir Post Bölümü
        
        [Authorize]
        [Route("isci-sicil-bildir")]
        [HttpPost]
        public ActionResult IsciSicilBildir(SicilKayitlari sicilkayitlari,HttpPostedFileBase temsiliresim,HttpPostedFileBase kanit1,HttpPostedFileBase kanit2, HttpPostedFileBase kanit3,HttpPostedFileBase kanit4,HttpPostedFileBase kanit5)
        {
            if (SicilKayitGuvenlik(sicilkayitlari.adsoyad, sicilkayitlari.telefon, sicilkayitlari.eposta, sicilkayitlari.aciklama)==false)
            {
                return RedirectToAction("Hata","HomeController");
            }
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            string temsili="null", kanitone="null", kanittwo="null", kanitthree="null", kanitfour="null", kanitfive="null";
            // Resimlere random isim verme.

            Random rnd = new Random();


            if (temsiliresim != null)
            {
                if (!DosyaTipi(temsiliresim.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else if (!DosyaBoyutu(temsiliresim.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else
                {
                    // Temsili Resim
                    var fileName = rnd.Next(0,999999999).ToString()+Path.GetFileName(temsiliresim.FileName);
                    temsili = fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName);
                    temsiliresim.SaveAs(path);
                    //----
                }
            }
            if (kanit1 != null)
            {
                if (!DosyaTipi(kanit1.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else if (!DosyaBoyutu(kanit1.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else
                {
                    // Kanıt 1 Resim
                    var fileName2 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit1.FileName);
                    kanitone = fileName2;
                    var path2 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName2);
                    kanit1.SaveAs(path2);
                    //----
                }
            }
            if (kanit2 != null)
            {
                if (!DosyaTipi(kanit2.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else if (!DosyaBoyutu(kanit2.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else
                {
                    // Kanıt 2 Resim
                    var fileName3 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit2.FileName);
                    kanittwo = fileName3;
                    var path3 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName3);
                    kanit2.SaveAs(path3);
                    //----
                }
            }
            if (kanit3 != null)
            {
                if (!DosyaTipi(kanit3.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else if (!DosyaBoyutu(kanit3.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else
                {
                    // Kanıt 3 Resim
                    var fileName4 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit3.FileName);
                    kanitthree = fileName4;
                    var path4 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName4);
                    kanit3.SaveAs(path4);
                    //----
                }
            }
            if (kanit4 != null)
            {
                if (!DosyaTipi(kanit4.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else if (!DosyaBoyutu(kanit4.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else
                {
                    // Kanıt 4 Resim
                    var fileName5 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit4.FileName);
                    kanitfour = fileName5;
                    var path5 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName5);
                    kanit4.SaveAs(path5);
                    //----
                }
            }
            if (kanit5 != null)
            {
                if (!DosyaTipi(kanit5.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else if (!DosyaBoyutu(kanit5.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                else
                {
                    // Kanıt 5 Resim
                    var fileName6 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit5.FileName);
                    kanitfive = fileName6;
                    var path6 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName6);
                    kanit5.SaveAs(path6);
                    //----
                }
            }


            try
            {
                if (sicilkayitlari.adsoyad == null || sicilkayitlari.aciklama == null || sicilkayitlari.telefon == null || sicilkayitlari.adsoyad.Length < 5 || sicilkayitlari.aciklama.Length <= 30 || sicilkayitlari.telefon.Length < 10)
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklerken İsim Soyisim, Açıklama ve Telefon alanlarının doğru doldurulması gerekmektedir.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsciSicilBildir");
                }
                sicilkayitlari.ekleyenkadi = HttpContext.User.Identity.Name.ToString();
                sicilkayitlari.sicilbilgi = "isci";

                if (temsiliresim != null)
                    sicilkayitlari.resim = temsili;
                if (kanit1 != null)
                    sicilkayitlari.kanit1 = kanitone;
                if (kanit2 != null)
                    sicilkayitlari.kanit2 = kanittwo;
                if (kanit3 != null)
                    sicilkayitlari.kanit3 = kanitthree;
                if (kanit4 != null)
                    sicilkayitlari.kanit4 = kanitfour;
                if (kanit5 != null)
                    sicilkayitlari.kanit5 = kanitfive;

                sicilkayitlari.eklenme_tarihi = DateTime.Now.ToString();
                db.SicilKayitlari.Add(sicilkayitlari);
                db.SaveChanges();
                return View("Basarili");
            }
            catch (Exception)
            {

               
            }

                    
                
            
           


            
                ViewBag.Hata = "Hata";
                ViewBag.Sebep = "Kritik hata oluştu. Hata kodu: #S4M3D";
                ViewBag.Sehirler = db.City.ToList();
                return View("IsciSicilBildir");
            
        }
        [Route("isveren-sicil-bildir")]
        [HttpPost]
        [Authorize]
        public ActionResult IsverenSicilBildir(SicilKayitlari sicilkayitlari, HttpPostedFileBase temsiliresim, HttpPostedFileBase kanit1, HttpPostedFileBase kanit2, HttpPostedFileBase kanit3, HttpPostedFileBase kanit4, HttpPostedFileBase kanit5)
        {
            if (SicilKayitGuvenlik(sicilkayitlari.firmaunvani, sicilkayitlari.telefon, sicilkayitlari.eposta, sicilkayitlari.aciklama) == false)
            {
                return RedirectToAction("Hata", "HomeController");
            }
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();

            string temsili = "null", kanitone = "null", kanittwo = "null", kanitthree = "null", kanitfour = "null", kanitfive = "null";
            // Resimlere random isim verme.

            Random rnd = new Random();


            if (temsiliresim != null)
            {
                if (!DosyaTipi(temsiliresim.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else if (!DosyaBoyutu(temsiliresim.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else
                {
                    // Temsili Resim
                    var fileName = rnd.Next(0, 999999999).ToString() + Path.GetFileName(temsiliresim.FileName);
                    temsili = fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName);
                    temsiliresim.SaveAs(path);
                    //----
                }
            }
            if (kanit1 != null)
            {
                if (!DosyaTipi(kanit1.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else if (!DosyaBoyutu(kanit1.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else
                {
                    // Kanıt 1 Resim
                    var fileName2 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit1.FileName);
                    kanitone = fileName2;
                    var path2 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName2);
                    kanit1.SaveAs(path2);
                    //----
                }
            }
            if (kanit2 != null)
            {
                if (!DosyaTipi(kanit2.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else if (!DosyaBoyutu(kanit2.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else
                {
                    // Kanıt 2 Resim
                    var fileName3 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit2.FileName);
                    kanittwo = fileName3;
                    var path3 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName3);
                    kanit2.SaveAs(path3);
                    //----
                }
            }
            if (kanit3 != null)
            {
                if (!DosyaTipi(kanit3.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else if (!DosyaBoyutu(kanit3.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else
                {
                    // Kanıt 3 Resim
                    var fileName4 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit3.FileName);
                    kanitthree = fileName4;
                    var path4 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName4);
                    kanit3.SaveAs(path4);
                    //----
                }
            }
            if (kanit4 != null)
            {
                if (!DosyaTipi(kanit4.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else if (!DosyaBoyutu(kanit4.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else
                {
                    // Kanıt 4 Resim
                    var fileName5 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit4.FileName);
                    kanitfour = fileName5;
                    var path5 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName5);
                    kanit4.SaveAs(path5);
                    //----
                }
            }
            if (kanit5 != null)
            {
                if (!DosyaTipi(kanit5.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya tiplerinde hata meydana geldi. Lütfen sadece JPG, JPEG, PNG veya GIF formatlarını kullanın.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else if (!DosyaBoyutu(kanit5.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Sicil kaydı eklenirken dosya boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    ViewBag.Sehirler = db.City.ToList();
                    return View("IsverenSicilBildir");
                }
                else
                {
                    // Kanıt 5 Resim
                    var fileName6 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(kanit5.FileName);
                    kanitfive = fileName6;
                    var path6 = Path.Combine(Server.MapPath("~/Content/sicil-photos"), fileName6);
                    kanit5.SaveAs(path6);
                    //----
                }
            }

            if (sicilkayitlari.adsoyad == null || sicilkayitlari.aciklama == null || sicilkayitlari.telefon == null || sicilkayitlari.adsoyad.Length < 5 || sicilkayitlari.aciklama.Length <= 30 || sicilkayitlari.telefon.Length < 10)
            {
                ViewBag.Hata = "Hata";
                ViewBag.Sebep = "Sicil kaydı eklerken Yetkili İsim Soyisim, Açıklama ve Telefon alanlarının doğru doldurulması gerekmektedir.";
                ViewBag.Sehirler = db.City.ToList();
                return View("IsverenSicilBildir");
            }

            try
            {
                sicilkayitlari.ekleyenkadi = HttpContext.User.Identity.Name.ToString();
                sicilkayitlari.sicilbilgi = "isveren";

                if (temsiliresim != null)
                    sicilkayitlari.resim = temsili;
                if (kanit1 != null)
                    sicilkayitlari.kanit1 = kanitone;
                if (kanit2 != null)
                    sicilkayitlari.kanit2 = kanittwo;
                if (kanit3 != null)
                    sicilkayitlari.kanit3 = kanitthree;
                if (kanit4 != null)
                    sicilkayitlari.kanit4 = kanitfour;
                if (kanit5 != null)
                    sicilkayitlari.kanit5 = kanitfive;
                sicilkayitlari.eklenme_tarihi = DateTime.Now.ToString();
                sicilkayitlari.goruntuleme = "0";
                db.SicilKayitlari.Add(sicilkayitlari);
                db.SaveChanges();
                return View("Basarili");
            }
            catch (Exception)
            {              
            }

            ViewBag.Hata = "Hata";
            ViewBag.Sebep = "Kritik hata oluştu. Hata kodu: #S4M3D-K4R4K75";
            ViewBag.Sehirler = db.City.ToList();
            return View("IsverenSicilBildir");
        }
        void GoruntulemeArtir(string sicilno)
        {
            OnlineSicilDBEntities2 sicildb = new OnlineSicilDBEntities2();
            var guncellenecekveri = sicildb.SicilKayitlari.Find(int.Parse(sicilno));
            if (guncellenecekveri != null)
            {
                if (guncellenecekveri.goruntuleme != null && guncellenecekveri.goruntuleme.Length>=0)
                {
                    string suanlik = guncellenecekveri.goruntuleme;
                    int suan = int.Parse(suanlik);
                    int artmis_hali = suan = suan + 1;
                    guncellenecekveri.goruntuleme = artmis_hali.ToString();                   
                }
                else
                {
                    guncellenecekveri.goruntuleme = "1";
                }
                sicildb.SaveChanges();
                

            }
        }
        //---------------------------
        
        [Route("sicil/isci/{SicilNo}")]
        public ActionResult IsciSicil(string SicilNo)
        {
            
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
          
                foreach (var item in db.SicilKayitlari)
                {
                    if (item.id.ToString() == SicilNo && item.sicilbilgi == "isci")
                    {
                        ViewBag.Id = item.id;
                        ViewBag.Ekleyen = item.ekleyenkadi;
                        ViewBag.AdSoyad = item.adsoyad;
                        ViewBag.Eposta = item.eposta;
                    ViewBag.Resim = item.resim;
                    ViewBag.Dolandirici = item.dolandirici;
                    ViewBag.Goruntuleme = item.goruntuleme;
                    ViewBag.KimlikOnay = item.kimlikonay;
                    ViewBag.Kanit1 = item.kanit1;
                    ViewBag.Kanit2 = item.kanit2;
                    ViewBag.UyeTarihi = item.eklenme_tarihi;
                    ViewBag.Kanit3 = item.kanit3;
                    ViewBag.Kanit4 = item.kanit4;
                    ViewBag.Kanit5 = item.kanit5;
                    ViewBag.Telefon = item.telefon;
                        SehirBul(item.sehir);
                        ViewBag.Sehir = plakam;
                        ViewBag.DogumTarihi = item.dogumtarihi;
                        ViewBag.Aciklama = item.aciklama;
                    GoruntulemeArtir(SicilNo);
                    
                    ViewBag.Baslik = item.adsoyad+" Sicil Kaydı";
                        return View();
                    }
                }


            return RedirectToAction("HataSayfasi","Home"); //aslında bulamadı burayı değiştir
        }
        [Route("sicil/isveren/{SicilNo}")]
      
        public ActionResult IsverenSicil(string SicilNo)
        {
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();

            foreach (var item in db.SicilKayitlari)
            {
                if (item.id.ToString() == SicilNo && item.sicilbilgi == "isveren")
                {
                    ViewBag.Ekleyen = item.ekleyenkadi;
                    ViewBag.FirmaUnvani = item.firmaunvani;
                    ViewBag.YetkiliAdSoyad = item.adsoyad;
                    ViewBag.Eposta = item.eposta;
                    ViewBag.Telefon = item.telefon;
                    ViewBag.Resim = item.resim;
                    ViewBag.Kanit1 = item.kanit1;
                    ViewBag.Dolandirici = item.dolandirici;
                    ViewBag.Goruntuleme = item.goruntuleme;
                    ViewBag.KimlikOnay = item.kimlikonay;
                    ViewBag.Kanit2 = item.kanit2;
                    ViewBag.Kanit3 = item.kanit3;
                    ViewBag.Kanit4 = item.kanit4;
                    ViewBag.UyeTarihi = item.eklenme_tarihi;
                    ViewBag.Kanit5 = item.kanit5;
                    SehirBul(item.sehir);
                    ViewBag.Sehir = plakam;
                    ViewBag.Adres = item.firmaadresi;
                    ViewBag.SiteAdresi = item.firmasitesi;
                    ViewBag.Aciklama = item.aciklama;
                    ViewBag.Id = item.id;
                    GoruntulemeArtir(SicilNo);
                    ViewBag.Baslik = item.firmaunvani + " Sicil Kaydı";
                    return View();
                }
            }


            return RedirectToAction("HataSayfasi", "Home"); //aslında bulamadı burayı değiştir
        }
        string plakam;
        void SehirBul(string plaka)
        {
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();

            foreach (var item in db.City)
            {
                if (plaka == item.code)
                {
                    plakam = item.name;
                }
            }
        }
        [Route("sicil-ara/{Tur}/{AnahtarKelime}")]
  
        public ActionResult SicilAra(string Tur, string AnahtarKelime)
        {
            ViewBag.Tur = Tur;
            ViewBag.AnahtarKelime = AnahtarKelime;
            ViewBag.Baslik = AnahtarKelime+" Arama Sonuçları";
            
            return View();
        }
        
        [Authorize]
        [HttpPost]
        [Route("yorum-yap")]
        public ActionResult YorumYap(Yorumlar yorumlar)
        {
            
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            foreach (var item in db.SicilKayitlari)
            {
                if (item.id == yorumlar.sicilid)
                {
                    if (item.sicilbilgi == "isci")
                    {
                        OnlineSicilDBEntities2 db2 = new OnlineSicilDBEntities2();
                        yorumlar.yorumyapanid = HttpContext.User.Identity.Name.ToString();
                        db2.Yorumlar.Add(yorumlar);
                        db2.SaveChanges();
                        return RedirectPermanent("/sicil/isci/"+ yorumlar.sicilid);
                    }
                    if (item.sicilbilgi == "isveren")
                    {
                        OnlineSicilDBEntities2 db2 = new OnlineSicilDBEntities2();
                        yorumlar.yorumyapanid = HttpContext.User.Identity.Name.ToString();
                        db2.Yorumlar.Add(yorumlar);
                        db2.SaveChanges();
                        return RedirectPermanent("/sicil/isveren/" + yorumlar.sicilid);
                    }
                }
            }
            return RedirectToAction("HataSayfasi", "Home");
        }
        [Authorize]
        [HttpGet]
        [Route("yorum-sil/{Id}")]
        public ActionResult YorumSil(int id)
        {
            string ekleyenkadi;
            string sicilid;
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            foreach (var item in db.Yorumlar)
            {
                if (item.id == id)
                {
                    ekleyenkadi = item.yorumyapanid;
                    sicilid = item.sicilid.ToString();
                    if (ekleyenkadi == HttpContext.User.Identity.Name.ToString())
                    {
                        OnlineSicilDBEntities2 db2 = new OnlineSicilDBEntities2();
                        foreach (var items in db2.SicilKayitlari)
                        {
                            if (items.id == item.sicilid)
                            {
                                if (items.sicilbilgi == "isci")
                                {
                                    OnlineSicilDBEntities2 sil = new OnlineSicilDBEntities2();
                                    var model = sil.Yorumlar.Find(id);
                                    sil.Yorumlar.Remove(model);
                                    sil.SaveChanges();
                                    return RedirectPermanent("/sicil/isci/" + sicilid);
                                }
                                if (items.sicilbilgi == "isveren")
                                {
                                    OnlineSicilDBEntities2 sil2 = new OnlineSicilDBEntities2();
                                    var model2 = sil2.Yorumlar.Find(id);
                                    sil2.Yorumlar.Remove(model2);
                                    sil2.SaveChanges();
                                    return RedirectPermanent("/sicil/isveren/" + sicilid);
                                }
                            }
                        }
                        
                    }
                }
            }
            return RedirectToAction("HataSayfasi", "Home");
        }
    }
}
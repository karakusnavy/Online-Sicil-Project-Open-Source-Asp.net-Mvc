using OnlineSicil.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineSicil.Controllers
{
    public class ProfileController : Controller
    {    
        OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
        // 
        void GoruntulemeArtir(string id,string hangisi)
        {
            OnlineSicilDBEntities2 iddb = new OnlineSicilDBEntities2();
            if (hangisi == "isci")
            {
                var guncellenecekveri = iddb.Isciler.Find(int.Parse(id));
                if (guncellenecekveri != null)
                {
                    if (guncellenecekveri.goruntuleme != null && guncellenecekveri.goruntuleme.Length >= 0)
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
                    iddb.SaveChanges();

                }
            }
            else
            {
                var guncellenecekveri2 = iddb.Isverenler.Find(int.Parse(id));
                if (guncellenecekveri2 != null)
                {
                    if (guncellenecekveri2.goruntuleme != null && guncellenecekveri2.goruntuleme.Length >= 0)
                    {
                        string suanlik = guncellenecekveri2.goruntuleme;
                        int suan = int.Parse(suanlik);
                        int artmis_hali = suan = suan + 1;
                        guncellenecekveri2.goruntuleme = artmis_hali.ToString();
                    }
                    else
                    {
                        guncellenecekveri2.goruntuleme = "1";
                    }
                    iddb.SaveChanges();
                }
            }
        }
        [Route("profil/{kullaniciAdi}")]
        public ActionResult Index(string kullaniciAdi)
        {
            ViewBag.Baslik = kullaniciAdi+" Kullanıcı Profili";
            foreach (var item in db.Isciler)
            {
                if (item.kadi == kullaniciAdi)
                {
                    ViewBag.AdSoyad = item.adsoyad;
                    ViewBag.EPosta = item.eposta;
                    ViewBag.DogumTarihi = item.dogumtarihi;
                    ViewBag.Telefon = item.telefon;
                    ViewBag.Kadi = item.kadi;
                    ViewBag.UyeTarihi = item.uyelik_tarihi;
                    ViewBag.Goruntuleme = item.goruntuleme;
                    ViewBag.Dolandirici = item.dolandirici;
                    ViewBag.KimlikOnay = item.kimlikonay;
                    SehirBul(item.sehir);
                    ViewBag.Sehir = plakam;
                    ViewBag.Resim = item.fotograf;
                    GoruntulemeArtir(item.id.ToString(), "isci");
                    return View("Profile");
                }
            }
            foreach (var item in db.Isverenler)
            {
                if (item.kadi == kullaniciAdi)
                {
                    ViewBag.AdSoyad = item.yetkiliadsoyad;
                    ViewBag.EPosta = item.eposta;
                    ViewBag.Adres = item.adres;
                    ViewBag.Firma = item.firmadi;
                    ViewBag.Telefon = item.telefon;
                    ViewBag.Goruntuleme = item.goruntuleme;
                    ViewBag.Dolandirici = item.dolandirici;
                    ViewBag.KimlikOnay = item.kimlikonay;
                    ViewBag.UyeTarihi = item.uyelik_tarihi;
                    ViewBag.Yetkili = item.yetkiliadsoyad;
                    ViewBag.Kadi = item.kadi;
                    ViewBag.Site = item.siteadresi;
                    ViewBag.Resim = item.firmalogo;
                    SehirBul(item.sehir);
                    ViewBag.Sehir = plakam;
                    GoruntulemeArtir(item.id.ToString(), "isveren");
                    return View("Profile");
                }
            }
            return RedirectToAction("HataSayfasi", "Home");
        }
        string plakam;
        void SehirBul(string plaka)
        {
            foreach (var item in db.City)
            {
                if (plaka == item.code)
                {
                    plakam = item.name;
                }
            }
        }
        [Authorize]
        [Route("profil/duzenle/{kullaniciAdi}")]
        [HttpGet]
        public ActionResult Duzenle(string kullaniciAdi)
        {
            ViewBag.Baslik = kullaniciAdi+" Profil Düzenle";
            foreach (var item in db.Isciler)
            {
                if (item.kadi==kullaniciAdi)
                {
                    return RedirectToAction("IsciDuzenleGet","Profile");
                }
            }
            foreach (var item in db.Isverenler)
            {
                if (item.kadi == kullaniciAdi)
                {
                    return RedirectToAction("IsverenDuzenleGet", "Profile");
                }
            }
            return RedirectToAction("HataSayfasi", "Home");
        }
        // Profil Düzenleme Get Bölümü
        [Authorize]
        [Route("profil/isci/duzenle")]
        [HttpGet]
        public ActionResult IsciDuzenleGet()
        {
            
            foreach (var item in db.Isciler)
            {
                if (item.kadi == HttpContext.User.Identity.Name.ToString())
                {
                    ViewBag.AdSoyad = item.adsoyad;
                    ViewBag.Resim = item.fotograf;
                    ViewBag.EPosta = item.eposta;
                    ViewBag.DogumTarihi = item.dogumtarihi;
                    ViewBag.Telefon = item.telefon;
                    ViewBag.Sifre = item.sifre;
                    ViewBag.Kadi = item.kadi;
                    ViewBag.Id = item.id;
                    ViewBag.Sehirler = db.City.ToList();
                    ViewBag.SehirId = item.sehir;
                    SehirBul(item.sehir);
                    ViewBag.Sehir = plakam;
                    if (babacan==1)
                    {
                        ViewBag.Hata = "1";
                    }

                    return View("IsciDuzenle");
                }
            }
            return RedirectToAction("HataSayfasi", "Home");
        }
        [Authorize]
        [Route("profil/isveren/duzenle")]
        [HttpGet]
        public ActionResult IsverenDuzenleGet()
        {
            foreach (var item in db.Isverenler)
            {
                if (item.kadi == HttpContext.User.Identity.Name.ToString())
                {
                    ViewBag.Firma = item.firmadi;
                    ViewBag.EPosta = item.eposta;
                    ViewBag.Adres = item.adres;
                    ViewBag.Resim = item.firmalogo;
                    ViewBag.Yetkili = item.yetkiliadsoyad;
                    ViewBag.Telefon = item.telefon;
                    ViewBag.Sifre = item.sifre;
                    ViewBag.Site = item.siteadresi;
                    ViewBag.Kadi = item.kadi;
                    ViewBag.Id = item.id;
                    ViewBag.Sehirler = db.City.ToList();
                    ViewBag.SehirId = item.sehir;
                    SehirBul(item.sehir);
                    ViewBag.Sehir = plakam;
                    if (babacan == 1)
                    {
                        ViewBag.Hata = "1";
                    }

                    return View("IsverenDuzenle");
                }
            }
            return RedirectToAction("HataSayfasi", "Home");
        }

        private bool DosyaTipi(string ContentType)
        {
            return ContentType.Equals("image/png") || ContentType.Equals("image/gif") || ContentType.Equals("image/jpg") || ContentType.Equals("image/jpeg");
        }
        private bool DosyaBoyutu(int ContentLenght)
        {
            return ((ContentLenght / 1024) / 1024) < 1;
        }
        string resimadresiisci;
        int babacan = 0;
        //--
        [Authorize]
        [Route("profil/isci/duzenle")]
        [HttpPost]
        public ActionResult IsciDuzenle(Isciler isciler,HttpPostedFileBase profilresmi)
        {
          
            if (profilresmi != null)
            {
                if (!DosyaTipi(profilresmi.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Güncelleme yapılırken hata oluştu. Resim tipi PNG, JPG, JPEG veya GIF formatlarında olmalıdır.";
                    return RedirectToAction("IsciDuzenleGet", "Profile");
                }
                else if (!DosyaBoyutu(profilresmi.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Resim boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    return RedirectToAction("IsciDuzenleGet", "Profile");
                }
                else
                {
                    Random rnd = new Random();
                    var fileName2 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(profilresmi.FileName);
                    resimadresiisci = fileName2;
                    var path2 = Path.Combine(Server.MapPath("~/Content/user-photos"), fileName2);
                    profilresmi.SaveAs(path2);
                   
                }
            }

            if (isciler.adsoyad.Length <= 4 ||isciler.telefon.Length<=8|| isciler.sifre.Length <= 3)
            {
                return RedirectToAction("IsciDuzenleGet", "Profile");
            }

                    string idsi;
                    idsi = isciler.id.ToString();
                    var guncellenecekveri = db.Isciler.Find(int.Parse(idsi));
                    guncellenecekveri.adsoyad = isciler.adsoyad;
                    guncellenecekveri.sifre = isciler.sifre;
                    guncellenecekveri.telefon = isciler.telefon;
                    guncellenecekveri.dogumtarihi = isciler.dogumtarihi;
                    guncellenecekveri.sehir = isciler.sehir;

            if (profilresmi!=null)
            { 
                guncellenecekveri.fotograf = resimadresiisci;
            }

            db.SaveChanges();
                    babacan++;
                    return RedirectToAction("IsciDuzenleGet","Profile");
            
            
      
        }
        [Authorize]
        [Route("profil/isveren/duzenle")]
        [HttpPost]
        public ActionResult IsverenDuzenle(Isverenler isverenler, HttpPostedFileBase profilresmi)
        {
            if (profilresmi != null)
            {
                if (!DosyaTipi(profilresmi.ContentType))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Güncelleme yapılırken hata oluştu. Resim tipi PNG, JPG, JPEG veya GIF formatlarında olmalıdır.";
                    return RedirectToAction("IsverenDuzenleGet", "Profile");
                }
                else if (!DosyaBoyutu(profilresmi.ContentLength))
                {
                    ViewBag.Hata = "Hata";
                    ViewBag.Sebep = "Resim boyutlarında hata meydana geldi. Lütfen boyutları düşürünüz.";
                    return RedirectToAction("IsverenDuzenleGet", "Profile");
                }
                else
                {
                    Random rnd = new Random();
                    var fileName2 = rnd.Next(0, 999999999).ToString() + Path.GetFileName(profilresmi.FileName);
                    resimadresiisci = fileName2;
                    var path2 = Path.Combine(Server.MapPath("~/Content/user-photos"), fileName2);
                    profilresmi.SaveAs(path2);

                }
            }

            if (isverenler.yetkiliadsoyad.Length <= 4 || isverenler.telefon.Length <= 8 || isverenler.sifre.Length <= 3)
            {
                return RedirectToAction("IsverenDuzenleGet", "Profile");
            }

            string idsi;
            idsi = isverenler.id.ToString();
            var guncellenecekveri = db.Isverenler.Find(int.Parse(idsi));
            guncellenecekveri.firmadi = isverenler.firmadi;
            guncellenecekveri.sifre = isverenler.sifre;
            guncellenecekveri.sehir = isverenler.sehir;
            guncellenecekveri.siteadresi = isverenler.siteadresi;
            guncellenecekveri.telefon = isverenler.telefon;
            guncellenecekveri.yetkiliadsoyad = isverenler.yetkiliadsoyad;
            guncellenecekveri.adres = isverenler.adres;
            if (profilresmi != null)
            {
                guncellenecekveri.firmalogo = resimadresiisci;
            }
            db.SaveChanges();
            babacan++;
            return RedirectToAction("IsverenDuzenleGet", "Profile");
        }
        [Authorize]
        [Route("profil/sicil-kaydi-sil/{id}")]
        public ActionResult SicilSil(int id)
        {
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            var model = db.SicilKayitlari.Find(id);
            if (model != null && model.ekleyenkadi == HttpContext.User.Identity.Name.ToString())
            {
                db.SicilKayitlari.Remove(model);
                db.SaveChanges();
                return View("Basarili");
            }
            return RedirectToAction("HataSayfasi", "Home");
        }
    }
}
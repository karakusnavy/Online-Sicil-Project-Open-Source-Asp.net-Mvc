using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineSicil.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace OnlineSicil.Controllers
{
    public class HomeController : Controller
    {
        OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
        [Route("")]
        public ActionResult Index()
        {
            return View("Index");
        }
        [Route("login")]
        public ActionResult Login()
        {
            ViewBag.Baslik = "Giriş Yap";
            return View("Login");
        }
        [Route("register")]
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Baslik = "Kayıt Ol";
            return View("Register");
        }

        // İşçi ve İşveren Kayıt Bölümü

        [Route("isci")]
        [HttpGet]
        public ActionResult IsciKaydiGet()
        {
            ViewBag.Baslik = "İşçi Kaydı Yap";
            ViewBag.Sehirler = db.City.ToList();
            return View("register_isci");
        }
        [Route("isveren")]
        [HttpGet]
        public ActionResult IsverenKaydiGet()
        {
            ViewBag.Baslik = "İşveren Kaydı Yap";
            ViewBag.Sehirler = db.City.ToList();
            return View("register_isveren");
        }

        //post bölümü


        [Route("isci")]
        [HttpPost]
        public ActionResult IsciKaydi(Isciler isciler)
        {
            //---- İşçinin Bilgileri İşveren Birisiyle Eşleşiyor mu?
            ViewBag.Sehirler = db.City.ToList();
            var model = db.Isverenler.ToList();
            foreach (var item in model)
            {
                if (item.eposta == isciler.eposta)
                {
                    ViewBag.Hata = "Bu Eposta Adresi Bir İşveren Tarafından Kullanılıyor";
                    return View("Register_Isci");
                }
                if (item.telefon == isciler.telefon)
                {
                    ViewBag.Hata = "Bu Telefon Numarası Bir İşveren Tarafından Kullanılıyor";
                    return View("Register_Isci");
                }
                if (item.kadi == isciler.kadi)
                {
                    ViewBag.Hata = "Bu Kullanıcı Adı Kullanılıyor";
                    return View("Register_Isci");
                }
            }
            //-----------------------------------------------------
            //---- İşçinin Bilgileri Bir İşçi Tarafından Kullanılıyor mu?
            var model2 = db.Isciler.ToList();
            foreach (var item in model2)
            {
                if (item.eposta == isciler.eposta)
                {
                    ViewBag.Hata = "Bu Eposta Adresi Bir İşçi Tarafından Kullanılıyor.";
                    return View("Register_Isci");
                }
                if (item.telefon == isciler.telefon)
                {
                    ViewBag.Hata = "Bu Telefon Numarası Bir İşçi Tarafından Kullanılıyor";
                    return View("Register_Isci");
                }
                if (item.kadi == isciler.kadi)
                {
                    ViewBag.Hata = "Bu Kullanıcı Adı Kullanılıyor";
                    return View("Register_Isci");
                }
            }
            //-------------------------------------------------------

            if (isciler.eposta.Length < 6 && isciler.telefon.Length < 9)
            {
                ViewBag.Hata = "Eposta veya şifre bilgileri yanlış girildi.";
                return View("Register_Isci");
            }
            
            try
            {
                Random rnd = new Random();
                isciler.bakiye = 0;
                string kod = "aylavyu-5" + rnd.Next(0, 99898998).ToString() + "a" + "546" + rnd.Next(0, 99).ToString() + "a";
                isciler.onay = kod;
                isciler.onay = "onaylandı"; //burayı kaldır
                isciler.uyelik_tarihi = DateTime.Now.ToString();
                db.Isciler.Add(isciler);
                db.SaveChanges();
                MailGonder(kod, isciler.eposta);
                return View("Onayla");
            }
            catch (Exception)
            {
                ViewBag.Hata = "Sistemde kritik hata mevcut. Hata Kodu samet-tozillamadafaka#46565";
                return View("Register_Isci");
            }
            

           
        }
        [Route("isveren")]
        [HttpPost]
        public ActionResult IsverenKaydi(Isverenler isverenler)
        {
            //---- İşçinin Bilgileri İşveren Birisiyle Eşleşiyor mu?
            var model = db.Isverenler.ToList();
            foreach (var item in model)
            {
                if (item.eposta == isverenler.eposta)
                {
                    ViewBag.Hata = "Bu Eposta Adresi Bir İşveren Tarafından Kullanılıyor.";
                    return View("Register_Isveren");
                }
                if (item.telefon == isverenler.telefon)
                {
                    ViewBag.Hata = "Bu Telefon Numarası Bir İşveren Tarafından Kullanılıyor";
                    return View("Register_Isveren");
                }
            }
            //-----------------------------------------------------
            //---- İşçinin Bilgileri Bir İşçi Tarafından Kullanılıyor mu?
            var model2 = db.Isverenler.ToList();
            foreach (var item in model2)
            {
                if (item.eposta == isverenler.eposta)
                {
                    ViewBag.Hata = "Bu Eposta Adresi Bir İşçi Tarafından Kullanılıyor.";
                    return View("Register_Isveren");
                }
                if (item.telefon == isverenler.telefon)
                {
                    ViewBag.Hata = "Bu Telefon Numarası Bir İşçi Tarafından Kullanılıyor";
                    return View("Register_Isveren");
                }
            }
            //-------------------------------------------------------

            if (isverenler.eposta.Length < 6 && isverenler.telefon.Length < 9)
            {
                ViewBag.Hata = "Eposta veya şifre bilgileri yanlış girildi.";
                return View("Register_Isveren");
            }




            try
            {
                Random rnd = new Random();
                isverenler.bakiye = 0;
                string kod = "aylavyu-5" + rnd.Next(0, 99898998).ToString() + "a" + "546" + rnd.Next(0, 99).ToString() + "a";
                isverenler.onay = kod;
                isverenler.onay = "onaylandı"; //burayı kaldır
                isverenler.uyelik_tarihi = DateTime.Now.ToString();
                db.Isverenler.Add(isverenler);
                db.SaveChanges();
                MailGonder(kod, isverenler.eposta);
                return View("Onayla");
            }
            catch (Exception)
            {
                ViewBag.Hata = "Sistemde kritik hata mevcut. Hata Kodu samet-tozillamadafaka#64695";
                return View("Register_Isveren");

            }

        }
        // Kayıt Bitiş
        //----

        void MailGonder(string kod, string mail)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("serakoder@gmail.com");
            msg.To.Add(new MailAddress(mail));
            msg.Subject = "Başvuru";
            msg.Body = "Mail adresinizi onaylayın: "+mail+"/"+kod;
            SmtpClient mySmtpClient = new SmtpClient();
            System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential("serakoder@gmail.com", "samet235*");
            mySmtpClient.Host = "smtp.gmail.com"; // host adresi ben default olarak gmail paylaşıyorum.
            mySmtpClient.Port = 587;          // smtp port no
            mySmtpClient.EnableSsl = true;
            mySmtpClient.UseDefaultCredentials = false;
            mySmtpClient.Credentials = myCredential;
            mySmtpClient.Send(msg);
            msg.Dispose();
        }

        //----
        [Route("onayla")]
        public ActionResult Onayla()
        {
            ViewBag.Baslik = "Onayla";
            return View("Onayla");
        }

        // Login Sayfaları

        [Route("login/isci")]
        [HttpGet]
        public ActionResult LoginIsci()
        {
            ViewBag.Baslik = "İşçi Girişi Yap";
            return View("Login_Isci");
        }
        [Route("login/isveren")]
        [HttpGet]
        public ActionResult LoginIsveren()
        {
            ViewBag.Baslik = "İşveren Girişi Yap";
            return View("Login_Isveren");
        }

        // Post Login Sayfaları

        [Route("login/isci")]
        [HttpPost]
        public ActionResult LoginIsci(Isciler isciler)
        {
            foreach (var item in db.Isciler)
            {
                if (isciler.eposta == item.eposta && isciler.sifre==item.sifre)
                {
                    if (item.onay!="onaylandı")
                    {
                        ViewBag.Hata = "E-Posta Adresinizi Doğrulamamışsınız. Lütfen E-posta adresinize giderek belirtilen linke tıklayın.";
                        return View("Login_Isci");
                    }
                    FormsAuthentication.SetAuthCookie(item.kadi, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Hata = "Bilgiler Yanlış";
            return View("Login_Isci");
        }
        [Route("login/isveren")]
        [HttpPost]
        public ActionResult LoginIsveren(Isverenler isverenler)
        {
            foreach (var item in db.Isverenler)
            {
                if (isverenler.eposta == item.eposta && isverenler.sifre == item.sifre)
                {
                    if (item.onay != "onaylandı")
                    {
                        ViewBag.Hata = "E-Posta Adresinizi Doğrulamamışsınız. Lütfen E-posta adresinize giderek belirtilen linke tıklayın.";
                        return View("Login_Isveren");
                    }
                    FormsAuthentication.SetAuthCookie(item.kadi, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Hata = "Bilgiler Yanlış";
            return View("Login_Isveren");
        }
        [Route("quit")]
        [Authorize]
        public ActionResult Quit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
        [Route("hata")]
        public ActionResult HataSayfasi()
        {
            ViewBag.Baslik = "Hata";
            return View("Hata");
        }
        [Route("hakkimizda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Baslik = "Hakkımızda";
            return View();
        }
        [Route("iletisim")]
        [HttpGet]
        public ActionResult Iletisim()
        {
            ViewBag.Baslik = "İletişim";
            return View();
        }
        [Route("iletisim")]
        [HttpPost]
        public ActionResult Iletisim(iletisim iletisim)
        {
            try
            {
                if (iletisim.adsoyad.Length <= 3 || iletisim.telefon.Length <= 7 || iletisim.eposta.Length <= 5 || iletisim.konu.Length <= 15)
                {
                    ViewBag.Hata = "Hata ! Lütfen bilgileri düzgün bir şekilde doldurunuz. ";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Hata = "Kritik Hata ! Bilgileri doldurun, eğer halen hata alıyorsanız mail adreslerimiz ile iletişime geçin.";
                return View();

            }
           
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            db.iletisim.Add(iletisim);
            db.SaveChanges();
            ViewBag.Hata = "Başarılı bir şekilde gönderim yaptınız ! En kısa zamanda verdiğiniz iletişim adreslerinden birine dönüş yapılacaktır. ";
            return View();
        }
        void MailOnaylaBaba(string id,string hayirdir)
        {
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            if (hayirdir=="isci")
            {
                var guncellenecekveri = db.Isciler.Find(int.Parse(id));
                guncellenecekveri.onay = "onaylandı";
                db.SaveChanges();
            }
            else
            {
                var guncellenecekveri = db.Isverenler.Find(int.Parse(id));
                guncellenecekveri.onay = "onaylandı";
                db.SaveChanges();
            }
        }
        [Route("mail-onay/{mail}/{kod}")]
        public ActionResult MailOnay(string mail, string kod)
        {
            OnlineSicilDBEntities2 db = new OnlineSicilDBEntities2();
            foreach (var item in db.Isciler)
            {
                if (item.onay == kod && item.eposta == mail)
                {
                    MailOnaylaBaba(item.id.ToString(),"isci");
                    return View("MailOnay");
                }
            }
            foreach (var item in db.Isverenler)
            {
                if (item.onay == kod && item.eposta == mail)
                {
                    MailOnaylaBaba(item.id.ToString(),"isveren");
                    return View("MailOnay");
                }
            }
            return View("Hata");
        }
        [Route("gizlilikpolitikasi")]
        public ActionResult GizlilikPolitikasi()
        {
            ViewBag.Baslik = "Gizlilik Politikası";
            return View();
        }
        [Route("kullanicisozlesmesi")]
        public ActionResult KullaniciSozlesmesi()
        {
            ViewBag.Baslik = "Kullanıcı Sözleşmesi";
            return View();
        }
        [Route("sss")]
        public ActionResult Sss()
        {
            ViewBag.Baslik = "Sıkça Sorulan Sorular";
            return View();
        }
      
    }
}
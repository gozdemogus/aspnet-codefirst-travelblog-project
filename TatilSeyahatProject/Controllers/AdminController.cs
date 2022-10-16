using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TatilSeyahatProject.Models.Siniflar;

namespace TatilSeyahatProject.Controllers
{
    public class AdminController : Controller
    {

        Context c = new Context();
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            var degerler = c.Blogs.ToList();
            return View(degerler);
        }


        //burası yeni blog ekleme ekranı, form doldurma kısmı
        [HttpGet]
        public ActionResult YeniBlog()
        {
            return View();
        }

        //burası formu doldurduktan sonra kayıt islemi yapıyor +
        //ardından da seni indexe yönlendiriyor yani oradan
        //eklenmis halini görmeni saglıyor, kendi direkt olarak bi
        //ekran göstermiyor 
        [HttpPost]
        public ActionResult YeniBlog(Blog p)
        {
            c.Blogs.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BlogSil(int id)
        {
            var b = c.Blogs.Find(id);
            c.Blogs.Remove(b);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //update tıklandıgında id bilgisiyle ilgili blogu bluyoruz
        //ardından o veriyle birlikte güncelleme view'a yönlendiriyor.
        //oradaki form cift taraflı calısıyor
        public ActionResult BlogGetir(int id)
        {
            var bl = c.Blogs.Find(id);
            return View("BlogGetir", bl);
        }

        public ActionResult BlogGuncelle(Blog b)
        {
            var blg = c.Blogs.Find(b.ID);
            blg.Tarih = b.Tarih;
            blg.Aciklama = b.Aciklama;
            blg.Baslik = b.Baslik;
            blg.BlogImage = b.BlogImage;
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult YorumListesi()
        {
            var yorumlar = c.Yorumlars.ToList();
            return View(yorumlar);
        }

        public ActionResult YorumSil(int id)
        {
            var b = c.Yorumlars.Find(id);
            c.Yorumlars.Remove(b);
            c.SaveChanges();
            return RedirectToAction("YorumListesi");
        }

        public ActionResult YorumGetir(int id)
        {
            var yrm = c.Yorumlars.Find(id);
            return View("YorumGetir", yrm);
        }

        public ActionResult YorumGuncelle(Yorumlar y)
        {
            var yrm = c.Yorumlars.Find(y.ID);
            yrm.KullaniciAdi = y.KullaniciAdi;
            yrm.Mail = y.Mail;
            yrm.Yorum = y.Yorum;
            c.SaveChanges();

            return RedirectToAction("YorumListesi");
        }



    }
}
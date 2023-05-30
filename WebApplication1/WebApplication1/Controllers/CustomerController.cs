using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Text;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public static String GetMD5_2(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++) 
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }


        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(FormCollection bilgi)
        {
            string tc = bilgi["tc"].ToString();
            string sif = GetMD5_2(bilgi["sif"].ToString());
            var count = db.Tb1_Musteriler.Where(x => x.TcKimlik == tc && x.Sifre == sif).Count();
            if (count==0)
            {
                ViewData["sonuc"] = "Hata! Kayıtlı değilsiniz yada bilgileriniz yanlış.";
            }
            else
            {
                Session["session_giris"] = true;
                Session["session_tc"] = tc;
              //  ViewData["sonuc"] = "Tebrikler...";
                return RedirectToAction("Profil", "Customer");
            }
            return View();
        }
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit([Bind(Include ="TcKimlik,AdSoyad,DoğumTarihi,Cinsiyet,Telefon,Sifre")] Tb1_Musteriler muteri)
        {
            db.Tb1_Musteriler.Add(muteri);
            int result = db.SaveChanges();
            if(result>0)
            {
                ViewData["sonuc"] = "Tebrikler ! Kaydınız Gerçekleşti ";
            }
            else
            {
                ViewData["sonuc"] = "Hata ! Kaydınız Gerçekleştirilemedi...";
            }
            return View();
        }   
            [HttpGet]
           public ActionResult Profil()
        {
            if(Session["session_giris"] != null)
            {
                string tc = Session["session_tc"].ToString();
                Tb1_Musteriler musteri = db.Tb1_Musteriler.Find(tc);
                if(musteri == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(musteri);
                }
            }
            else
            {
                return RedirectToAction("Giris");
            } 
        }
            [HttpPost]
            public ActionResult Profil(FormCollection bilgi)
            {
             if (ModelState.IsValid)
                {
                    String tc = Session["session_tc"].ToString();
                    Tb1_Musteriler musteri = db.Tb1_Musteriler.Find(tc);

                    musteri.AdSoyad = bilgi["AdSoyad"].ToString();
                    musteri.DoğumTarihi = Convert.ToDateTime(bilgi["DoğumTarihi"]);
                    musteri.Cinsiyet = bilgi["Cisiyet"].ToString();
                    musteri.Telefon = bilgi["Telefon"].ToString();

                    db.Entry(musteri).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Profil");
                }
                 return View();



               
            }
            
       
        

    }
}
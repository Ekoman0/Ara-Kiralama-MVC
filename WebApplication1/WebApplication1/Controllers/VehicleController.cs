using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VehicleController : Controller
    {
        Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();
       //GET: Vehicle
        public ActionResult Index()
        {
            return View(db.Tb1_Araclar.ToList());
        }
        public ActionResult Details(int? id)
        {
            Tb1_Araclar arac_bilgileri = db.Tb1_Araclar.Find(id);
            return View(arac_bilgileri);
        }

        [HttpGet]
        public ActionResult Rezervation(int? id)
        {
            Tb1_Araclar arac_bilgileri = db.Tb1_Araclar.Find(id);
            ViewData["Marka"] = arac_bilgileri.Marka;
            ViewData["Model"] = arac_bilgileri.Model;
            ViewData["Fiyat"] = arac_bilgileri.Fiyat;
            return View();
        }
        [HttpPost]
        public ActionResult Rezervation([Bind(Include = "RezervationId,AracId,TcKimlik,AdSoyad,AlmaTarihi,TeslimTarihi,Ucret")]Tb1_Rezervasyonlar rezervasyon)     
        {
            if(ModelState.IsValid)
            {
                db.Tb1_Rezervasyonlar.Add(rezervasyon);
                db.SaveChanges();
            }
            ViewBag.Message = "Tebrikler Rezervasyon İşleminiz Tamamlanmıştır.";
            return View();
        }
    }
}   
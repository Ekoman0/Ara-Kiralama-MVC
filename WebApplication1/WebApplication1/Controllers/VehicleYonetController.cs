using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers
{
    public class VehicleYonetController : Controller
    {
        private Db_AracKiralamaEntities db = new Db_AracKiralamaEntities();

        // GET: VehicleYonet
        public ActionResult Index()
        {
            return View(db.Tb1_Araclar.ToList());
        }

        // GET: VehicleYonet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb1_Araclar tb1_Araclar = db.Tb1_Araclar.Find(id);
            if (tb1_Araclar == null)
            {
                return HttpNotFound();
            }
            return View(tb1_Araclar);
        }

        // GET: VehicleYonet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleYonet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AracId,Marka,Model,ModelYili,Yakit,Vites,Fiyat")] Tb1_Araclar tb1_Araclar)
        {
            if (ModelState.IsValid)
            {
                db.Tb1_Araclar.Add(tb1_Araclar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb1_Araclar);
        }

        // GET: VehicleYonet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb1_Araclar tb1_Araclar = db.Tb1_Araclar.Find(id);
            if (tb1_Araclar == null)
            {
                return HttpNotFound();
            }
            return View(tb1_Araclar);
        }

        // POST: VehicleYonet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AracId,Marka,Model,ModelYili,Yakit,Vites,Fiyat")] Tb1_Araclar tb1_Araclar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb1_Araclar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb1_Araclar);
        }

        // GET: VehicleYonet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb1_Araclar tb1_Araclar = db.Tb1_Araclar.Find(id);
            if (tb1_Araclar == null)
            {
                return HttpNotFound();
            }
            return View(tb1_Araclar);
        }

        // POST: VehicleYonet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb1_Araclar tb1_Araclar = db.Tb1_Araclar.Find(id);
            db.Tb1_Araclar.Remove(tb1_Araclar);
            db.SaveChanges();
            //Araç Resmi Siliniyor
            String ImageFileName = id.ToString() + ".jpg";
            String FolderPath = Path.Combine(Server.MapPath("~/ VehicleImages"), ImageFileName);
            if(System.IO.File.Exists(FolderPath))
            {
                System.IO.File.Delete(FolderPath);
            }
            //resim Silindi
            return RedirectToAction("Index");
        }





        [HttpGet]
        public ActionResult SaveImages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveImages(String hiddenId, HttpPostedFileBase UploadedImage)
        {
            if(UploadedImage.ContentLength > 0)
            {
               String ImageFileName = hiddenId + "jpg";
                String FolderPath = Path.Combine(Server.MapPath("~/VehicleImages"), ImageFileName);
                UploadedImage.SaveAs(FolderPath);
            }
            ViewBag.Message = hiddenId + ".jpg İsimli Resim Başarıyla Yüklendi";
            return View();
        }   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

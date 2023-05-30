using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class RezervationController : Controller
    {
        // GET: Rezervation
        public ActionResult Index()
        {
            if (Session["session_giris"] != null)
            {
               return View();
            }
            else
            {
                return RedirectToAction("Giris", "Customer");
            }
            
        }
    }
}
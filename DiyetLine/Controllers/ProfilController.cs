using DiyetLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DiyetLine.Controllers
{

    public class ProfilController : Controller
    {
        // GET: Profil
        diyetlineEntities db = new diyetlineEntities();

        [_SessionControl]
        public ActionResult Profil()
        {
            var email = HttpContext.User.Identity.Name;

            var query1 = db.Table_IsletmeSahibi.Where(x => x.Restorant_Email == email).Select(x => x.Rol_id).FirstOrDefault();
            var query2 = db.Table_Kullanicilar.Where(x => x.Email == email).Select(x => x.Rol_id).FirstOrDefault();
            if (query1 == 3)
            {
              
                var query3 = db.Table_IsletmeSahibi.Where(x => x.Restorant_Email == email).Select(x => x.Sq_id).FirstOrDefault();
                ViewBag.id = query3;
                return View("IsletmeProfil");
            }
            else if (query2 == 2)
            {
                return View();
            }
            else
                return View("Hata");
        }
    

        public ActionResult IsletmeProfil(int IsletmeId)
        {
            List<il> illerList = db.il.ToList();
            ViewBag.illerList = new SelectList(illerList, "Sqid", "ad");
            Models.ViewModel isletmesahibi = IsletmeGetir(IsletmeId);
                return View(isletmesahibi);           
        }

        public static Models.ViewModel IsletmeGetir(int id)
        {
            diyetlineEntities db = new diyetlineEntities();
            Models.ViewModel gelenisletme = new Models.ViewModel();
            var isletme=db.Table_IsletmeSahibi.Where(i => i.Sq_id == id).FirstOrDefault();
             gelenisletme.isletmesahibi=isletme;
            return gelenisletme;
        }
        [HttpPost]
        public ActionResult IsletmeProfil(Models.ViewModel form)
        {
            if (ModelState.IsValid)
            {
                Table_IsletmeSahibi isletmesahibi = new Table_IsletmeSahibi();
                isletmesahibi = form.isletmesahibi;
                
                db.Entry(isletmesahibi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profil");
            }
            return View(form);
        }
    }
    }
   

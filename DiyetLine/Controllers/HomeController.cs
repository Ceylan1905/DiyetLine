using DiyetLine.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DiyetLine.Controllers
{
    public class HomeController : Controller
    {
        diyetlineEntities db = new diyetlineEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UyeOl()
        {
            return View();
        }
      
     
        public ActionResult IsletmeSahibi_UyeOl()
        {
            List<il> illerList = db.il.ToList();
            ViewBag.illerList = new SelectList(illerList, "Sqid", "ad");
            return View();
        }

        public JsonResult GetIlcelerList (int IlId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ilce> IlceList = db.ilce.Where(x => x.il_id == IlId).ToList();
            return Json(IlceList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IsletmeSahibi_UyeOl(ViewModel isletme)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var ctx = new diyetlineEntities())
                    {
                        ctx.Table_IsletmeSahibi.Add(new Table_IsletmeSahibi()
                        {
                            Sq_id = isletme.isletmesahibi.Sq_id,
                            Restorant_Sahibi = isletme.isletmesahibi.Restorant_Sahibi,
                            Restorant_Adi = isletme.isletmesahibi.Restorant_Adi,
                            Restorant_Email = isletme.isletmesahibi.Restorant_Email,
                            Restorant_SahibiTel = isletme.isletmesahibi.Restorant_SahibiTel,
                            Restorant_Tel = isletme.isletmesahibi.Restorant_Tel,
                            SubeSorumlusu_Tel = isletme.isletmesahibi.SubeSorumlusu,
                            SubeSorumlusu = isletme.isletmesahibi.SubeSorumlusu,
                            Adres = isletme.isletmesahibi.Adres,
                            TCNo = isletme.isletmesahibi.TCNo != null ? isletme.isletmesahibi.TCNo.ToString() : String.Empty,
                            VergiNo = isletme.isletmesahibi.VergiNo != null ? isletme.isletmesahibi.VergiNo.ToString() : String.Empty,
                            il_id = isletme.IlId,
                            ilce_id = isletme.IlceId,
                            Sifre=isletme.isletmesahibi.Sifre,
                            Rol_id = isletme.isletmesahibi.Rol_id = 3,

                        });

                        ctx.SaveChanges();
                        ViewBag.success = "Kayıt işleminiz başarıyla gerçekleştirildi.";
                    }

                }
                catch
                {
                    ViewBag.fail = "Kayıt işleminiz gerçekleştirilemedi.";
                }
                
            }

           return View(isletme);
            }
      
        [HttpPost]
        public ActionResult UyeOl(Table_Kullanicilar user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56488/api/Kullanıcılar/create");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Table_Kullanicilar>("create", user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
          
            return View(user );
        }
        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return RedirectToAction("Index");

        }
        [AllowAnonymous]
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
      
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(KullaniciIsletmeViewModel model, string returnurl)
        {
            if (ModelState.IsValid)
            {
                diyetlineEntities db = new diyetlineEntities();
               
                    //Aşağıdaki if komutu gönderilen mail ve şifre doğrultusunda kullanıcı kontrolu yapar. Eğer kullanıcı var ise login olur.
                    var result1 = db.Table_IsletmeSahibi.FirstOrDefault(x => x.Restorant_Email == model.kullanici.Email && x.Sifre == model.kullanici.Sifre);
                var result2 = db.Table_Kullanicilar.FirstOrDefault(x => x.Email == model.kullanici.Email && x.Sifre == model.kullanici.Sifre);
                if (result1 != null)
                    {
                    model.isletme_sahibi.Restorant_Email=model.kullanici.Email;
                    model.isletme_sahibi.Rol_id=model.kullanici.Rol_id = 3;
                        FormsAuthentication.SetAuthCookie(model.isletme_sahibi.Restorant_Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                else if(result2 != null)
                {
                    FormsAuthentication.SetAuthCookie(model.kullanici.Email, true);
                    return RedirectToAction("Index", "Home");
                }

                    else
                    {
                        ModelState.AddModelError("", "EMail veya şifre hatalı!");
                    }
                }
           
            return View(model);
        }
      

       
    }
}
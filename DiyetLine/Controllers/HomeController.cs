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
        DiyetlineEntities db = new DiyetlineEntities();
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

            using (var ctx = new DiyetlineEntities())
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
                    Rol_id = isletme.isletmesahibi.Rol_id = 3,

                });

                ctx.SaveChanges();
                return View(isletme);
            }

           
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
            return Redirect("Index");
      
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Table_Kullanicilar model, string returnurl)
        {
            if (ModelState.IsValid)
            {
                DiyetlineEntities db = new DiyetlineEntities();

                //Aşağıdaki if komutu gönderilen mail ve şifre doğrultusunda kullanıcı kontrolu yapar. Eğer kullanıcı var ise login olur.
                var result = db.Table_Kullanicilar.FirstOrDefault(x => x.Email == model.Email && x.Sifre == model.Sifre);
                if(result!=null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
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
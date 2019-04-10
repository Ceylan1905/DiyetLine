using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DiyetLine.Models;
using Newtonsoft.Json;
namespace DiyetLine.Controllers
{
    //Kübra bağlantı denemesi
    [RoutePrefix("api/Kullanıcılar")]
    [Authorize]
    public class KullanıcılarController : ApiController
    {
        private diyetlineEntities db = new diyetlineEntities();
        public KullanıcılarController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
       
        // GET: api/Kullanıcılar
        [HttpGet]
        [Route("findall")]
        public IQueryable<Table_Kullanicilar> GetTable_Kullanıcılar()
        {
            return db.Table_Kullanicilar;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("findisletme")]
        public IQueryable<Table_IsletmeSahibi> GetTable_Isletme()
        {
            return db.Table_IsletmeSahibi;
        }

        // GET: api/Kullanıcılar/5
        [HttpGet]
        [Route("find/{id}")]
       
        [ResponseType(typeof(Table_Kullanicilar))]
        public HttpResponseMessage GetTable_Kullanıcılar(int id)
        {
            try
            {
            var response= new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new
                    StringContent(JsonConvert.SerializeObject(db.Table_Kullanicilar.SingleOrDefault(x => x.Sq_id == id)));
                response.Content.Headers.ContentType = new
                    System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

        // DELETE: api/Kullanıcılar/5
        [HttpDelete]
        [Route("delete/{id}")]
        [ResponseType(typeof(Table_Kullanicilar))]
        public HttpResponseMessage DeleteTable_Kullanıcılar(int id)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                db.Table_Kullanicilar.Remove(db.Table_Kullanicilar.SingleOrDefault(x => x.Sq_id == id));
                db.SaveChanges();
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

        // POST: api/Kullanıcılar
        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        [ResponseType(typeof(Table_Kullanicilar))]
        public IHttpActionResult PostTable_Kullanıcılar(Table_Kullanicilar table_Kullanıcılar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new diyetlineEntities())
            {
                ctx.Table_Kullanicilar.Add(new Table_Kullanicilar()
                {
                    Sq_id=table_Kullanıcılar.Sq_id,
                    Ad = table_Kullanıcılar.Ad,
                    Soyad = table_Kullanıcılar.Soyad,
                    Email = table_Kullanıcılar.Email,
                    Sifre=table_Kullanıcılar.Sifre,
                    Rol_id=table_Kullanıcılar.Rol_id=2,
                   
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        
        
        
        // PUT: api/Kullanıcılar/5
        [HttpPut]
        [Route("update")]
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutTable_Kullanıcılar( Table_Kullanicilar table_Kullanıcılar)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var currentuser = db.Table_Kullanicilar.SingleOrDefault(x => x.Sq_id == table_Kullanıcılar.Sq_id);
                currentuser.Sq_id = table_Kullanıcılar.Sq_id;
                currentuser.Ad = table_Kullanıcılar.Ad;
                currentuser.Soyad = table_Kullanıcılar.Soyad;
                currentuser.Email = table_Kullanıcılar.Email;
                currentuser.Sifre = table_Kullanıcılar.Sifre;
                currentuser.Rol_id = table_Kullanıcılar.Rol_id;
                db.SaveChanges();
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

       

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_KullanıcılarExists(int id)
        {
            return db.Table_Kullanicilar.Count(e => e.Sq_id == id) > 0;
        }
    }
}
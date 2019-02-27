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

namespace DiyetLine.Controllers
{
    [RoutePrefix("api/Roller")]
    public class RollerController : ApiController
    {
        private diyetlineEntities db = new diyetlineEntities();
        public RollerController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Roller
        [HttpGet]
        [Route("findall")]
        public IQueryable<Table_Roller> GetTable_Roller()
        {
            return db.Table_Roller;
        }

        // GET: api/Roller/5
        [HttpGet]
        [Route("find/{id}")]
        [ResponseType(typeof(Table_Roller))]
        public IHttpActionResult GetTable_Roller(int id)
        {
            Table_Roller table_Roller = db.Table_Roller.Find(id);
            if (table_Roller == null)
            {
                return NotFound();
            }

            return Ok(table_Roller);
        }

        // PUT: api/Roller/5
        [HttpPut]
        [Route("update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTable_Roller(int id, Table_Roller table_Roller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table_Roller.Sq_id)
            {
                return BadRequest();
            }

            db.Entry(table_Roller).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Table_RollerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Roller
        [HttpPost]
        [Route("create")]
        [ResponseType(typeof(Table_Roller))]
        public IHttpActionResult PostTable_Roller(Table_Roller table_Roller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Table_Roller.Add(table_Roller);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = table_Roller.Sq_id }, table_Roller);
        }

        // DELETE: api/Roller/5
        [HttpDelete]
        [Route("delete")]
        [ResponseType(typeof(Table_Roller))]
        public IHttpActionResult DeleteTable_Roller(int id)
        {
            Table_Roller table_Roller = db.Table_Roller.Find(id);
            if (table_Roller == null)
            {
                return NotFound();
            }

            db.Table_Roller.Remove(table_Roller);
            db.SaveChanges();

            return Ok(table_Roller);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_RollerExists(int id)
        {
            return db.Table_Roller.Count(e => e.Sq_id == id) > 0;
        }
    }
}
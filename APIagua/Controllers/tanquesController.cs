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
using APIagua.Models;

namespace APIagua.Controllers
{
    public class tanquesController : ApiController
    {
        private aguaEntities db = new aguaEntities();

        // GET: api/tanques
        public IQueryable<tanque> Gettanques()
        {
            return db.tanques;
        }

        // GET: api/tanques/5
        [ResponseType(typeof(tanque))]
        public IHttpActionResult Gettanque(int id)
        {
            tanque tanque = db.tanques.Find(id);
            if (tanque == null)
            {
                return NotFound();
            }

            return Ok(tanque);
        }

        // PUT: api/tanques/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttanque(int id, tanque tanque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tanque.id_tanque)
            {
                return BadRequest();
            }

            db.Entry(tanque).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tanqueExists(id))
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

        // POST: api/tanques
        [ResponseType(typeof(tanque))]
        public IHttpActionResult Posttanque(tanque tanque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tanques.Add(tanque);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tanque.id_tanque }, tanque);
        }

        // DELETE: api/tanques/5
        [ResponseType(typeof(tanque))]
        public IHttpActionResult Deletetanque(int id)
        {
            tanque tanque = db.tanques.Find(id);
            if (tanque == null)
            {
                return NotFound();
            }

            db.tanques.Remove(tanque);
            db.SaveChanges();

            return Ok(tanque);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tanqueExists(int id)
        {
            return db.tanques.Count(e => e.id_tanque == id) > 0;
        }
    }
}
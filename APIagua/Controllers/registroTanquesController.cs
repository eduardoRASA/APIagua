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
    public class registroTanquesController : ApiController
    {
        private aguaEntities db = new aguaEntities();

        // GET: api/registroTanques
        public IQueryable<registroTanque> GetregistroTanques()
        {
            return db.registroTanques;
        }

        // GET: api/registroTanques/5
        [ResponseType(typeof(registroTanque))]
        public IHttpActionResult GetregistroTanque(int id)
        {
            registroTanque registroTanque = db.registroTanques.Find(id);
            if (registroTanque == null)
            {
                return NotFound();
            }

            return Ok(registroTanque);
        }

        // PUT: api/registroTanques/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutregistroTanque(int id, registroTanque registroTanque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registroTanque.id)
            {
                return BadRequest();
            }

            db.Entry(registroTanque).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!registroTanqueExists(id))
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

        // POST: api/registroTanques
        [ResponseType(typeof(registroTanque))]
        public IHttpActionResult PostregistroTanque(registroTanque registroTanque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.registroTanques.Add(registroTanque);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registroTanque.id }, registroTanque);
        }

        // DELETE: api/registroTanques/5
        [ResponseType(typeof(registroTanque))]
        public IHttpActionResult DeleteregistroTanque(int id)
        {
            registroTanque registroTanque = db.registroTanques.Find(id);
            if (registroTanque == null)
            {
                return NotFound();
            }

            db.registroTanques.Remove(registroTanque);
            db.SaveChanges();

            return Ok(registroTanque);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool registroTanqueExists(int id)
        {
            return db.registroTanques.Count(e => e.id == id) > 0;
        }
    }
}
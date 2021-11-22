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
    public class registroPresasController : ApiController
    {
        private aguaEntities db = new aguaEntities();

        // GET: api/registroPresas
        public IQueryable<registroPresa> GetregistroPresas()
        {
            return db.registroPresas;
        }
        // GET: api/registroPresas/5
        [ResponseType(typeof(registroPresa))]
        public List<registroPresa> GetregistroPresa(int id)
        {
            var registrosXpresa = db.registroPresas.Where(i => i.id_presa == id).ToList();
            if (registrosXpresa == null)
            {
                return null;
            }

            return registrosXpresa;
        }

        // PUT: api/registroPresas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutregistroPresa(int id, registroPresa registroPresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registroPresa.id)
            {
                return BadRequest();
            }

            db.Entry(registroPresa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!registroPresaExists(id))
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

        // POST: api/registroPresas
        [ResponseType(typeof(registroPresa))]
        public IHttpActionResult PostregistroPresa(registroPresa registroPresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.registroPresas.Add(registroPresa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registroPresa.id }, registroPresa);
        }

        // DELETE: api/registroPresas/5
        [ResponseType(typeof(registroPresa))]
        public IHttpActionResult DeleteregistroPresa(int id)
        {
            registroPresa registroPresa = db.registroPresas.Find(id);
            if (registroPresa == null)
            {
                return NotFound();
            }

            db.registroPresas.Remove(registroPresa);
            db.SaveChanges();

            return Ok(registroPresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool registroPresaExists(int id)
        {
            return db.registroPresas.Count(e => e.id == id) > 0;
        }
    }
}
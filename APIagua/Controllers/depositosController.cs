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
    public class depositosController : ApiController
    {
        private aguaEntities db = new aguaEntities();

        // GET: api/depositoes
        public IList<deposito> Getdepositos()
        {
            var depositosList = db.depositos
                             .SqlQuery("SELECT * FROM depositos")
                             .ToList<deposito>();
            return depositosList;
        }

        // GET: api/depositoes/5
        [ResponseType(typeof(deposito))]
        public IHttpActionResult Getdeposito(int id)
        {
            deposito deposito = db.depositos.Find(id);
            if (deposito == null)
            {
                return NotFound();
            }

            return Ok(deposito);
        }

        // PUT: api/depositoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdeposito(int id, deposito deposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deposito.id_deposito)
            {
                return BadRequest();
            }

            db.Entry(deposito).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!depositoExists(id))
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

        // POST: api/depositoes
        [ResponseType(typeof(deposito))]
        public IHttpActionResult Postdeposito(deposito deposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.depositos.Add(deposito);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deposito.id_deposito }, deposito);
        }

        // DELETE: api/depositoes/5
        [ResponseType(typeof(deposito))]
        public IHttpActionResult Deletedeposito(int id)
        {
            deposito deposito = db.depositos.Find(id);
            if (deposito == null)
            {
                return NotFound();
            }

            db.depositos.Remove(deposito);
            db.SaveChanges();

            return Ok(deposito);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool depositoExists(int id)
        {
            return db.depositos.Count(e => e.id_deposito == id) > 0;
        }
    }
}
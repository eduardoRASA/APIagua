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
    public class registroDepositoesController : ApiController
    {
        private aguaEntities db = new aguaEntities();

        // GET: api/registroDepositoes
        public IQueryable<registroDeposito> GetregistroDepositoes()
        {
            return db.registroDepositoes;
        }

        // GET: api/registroDepositoes/5
        [ResponseType(typeof(registroDeposito))]
        public IHttpActionResult GetregistroDeposito(int id)
        {
            registroDeposito registroDeposito = db.registroDepositoes.Find(id);
            if (registroDeposito == null)
            {
                return NotFound();
            }

            return Ok(registroDeposito);
        }

        // PUT: api/registroDepositoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutregistroDeposito(int id, registroDeposito registroDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registroDeposito.id)
            {
                return BadRequest();
            }

            db.Entry(registroDeposito).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!registroDepositoExists(id))
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

        // POST: api/registroDepositoes
        [ResponseType(typeof(registroDeposito))]
        public IHttpActionResult PostregistroDeposito(registroDeposito registroDeposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.registroDepositoes.Add(registroDeposito);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registroDeposito.id }, registroDeposito);
        }

        // DELETE: api/registroDepositoes/5
        [ResponseType(typeof(registroDeposito))]
        public IHttpActionResult DeleteregistroDeposito(int id)
        {
            registroDeposito registroDeposito = db.registroDepositoes.Find(id);
            if (registroDeposito == null)
            {
                return NotFound();
            }

            db.registroDepositoes.Remove(registroDeposito);
            db.SaveChanges();

            return Ok(registroDeposito);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool registroDepositoExists(int id)
        {
            return db.registroDepositoes.Count(e => e.id == id) > 0;
        }
    }
}
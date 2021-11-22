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
    public class presasController : ApiController
    {
        private aguaEntities db = new aguaEntities();

        // GET: api/presas
        public IList<presa> Getpresas()
        {
            var presasList = db.presas
                             .SqlQuery("SELECT * FROM presas")
                             .ToList<presa>();
           return presasList;
        }

        // GET: api/presas/5
        [ResponseType(typeof(presa))]
        public IHttpActionResult Getpresa(int id)
        {
            presa presa = db.presas.Find(id);
            if (presa == null)
            {
                return NotFound();
            }

            return Ok(presa);
        }

        // PUT: api/presas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpresa(int id, presa presa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != presa.id_presa)
            {
                return BadRequest();
            }

            db.Entry(presa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!presaExists(id))
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

        // POST: api/presas
        [ResponseType(typeof(presa))]
        public IHttpActionResult Postpresa(presa presa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.presas.Add(presa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = presa.id_presa }, presa);
        }

        // DELETE: api/presas/5
        [ResponseType(typeof(presa))]
        public IHttpActionResult Deletepresa(int id)
        {
            presa presa = db.presas.Find(id);
            if (presa == null)
            {
                return NotFound();
            }

            db.presas.Remove(presa);
            db.SaveChanges();

            return Ok(presa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool presaExists(int id)
        {
            return db.presas.Count(e => e.id_presa == id) > 0;
        }
    }
}
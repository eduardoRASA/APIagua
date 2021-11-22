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
    public class estadosController : ApiController
    {
        private aguaEntities db = new aguaEntities();

        // GET: api/estados
        public IQueryable<estado> Getestadoes()
        {
            return db.estadoes;
        }

        // GET: api/estados/5
        [ResponseType(typeof(estado))]
        public IHttpActionResult Getestado(int id)
        {
            estado estado = db.estadoes.Find(id);
            if (estado == null)
            {
                return NotFound();
            }

            return Ok(estado);
        }

        // PUT: api/estados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putestado(int id, estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estado.id_estado)
            {
                return BadRequest();
            }

            db.Entry(estado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!estadoExists(id))
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

        // POST: api/estados
        [ResponseType(typeof(estado))]
        public IHttpActionResult Postestado(estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.estadoes.Add(estado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estado.id_estado }, estado);
        }

        // DELETE: api/estados/5
        [ResponseType(typeof(estado))]
        public IHttpActionResult Deleteestado(int id)
        {
            estado estado = db.estadoes.Find(id);
            if (estado == null)
            {
                return NotFound();
            }

            db.estadoes.Remove(estado);
            db.SaveChanges();

            return Ok(estado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool estadoExists(int id)
        {
            return db.estadoes.Count(e => e.id_estado == id) > 0;
        }
    }
}
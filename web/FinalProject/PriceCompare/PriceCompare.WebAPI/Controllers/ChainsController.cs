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
using PriceCompare.WebApi.Models;

namespace PriceCompare.WebApi.Controllers
{
    public class ChainsController : ApiController
    {
        private PricesDBEntities db = new PricesDBEntities();

        // GET: api/Chains
        public IQueryable<Chain> GetChains()
        {
            return db.Chains;
        }

        // GET: api/Chains/5
        [ResponseType(typeof(Chain))]
        public IHttpActionResult GetChain(int id)
        {
            Chain chain = db.Chains.Find(id);
            if (chain == null)
            {
                return NotFound();
            }

            return Ok(chain);
        }

        // PUT: api/Chains/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChain(int id, Chain chain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chain.Id)
            {
                return BadRequest();
            }

            db.Entry(chain).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChainExists(id))
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

        // POST: api/Chains
        [ResponseType(typeof(Chain))]
        public IHttpActionResult PostChain(Chain chain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Chains.Add(chain);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chain.Id }, chain);
        }

        // DELETE: api/Chains/5
        [ResponseType(typeof(Chain))]
        public IHttpActionResult DeleteChain(int id)
        {
            Chain chain = db.Chains.Find(id);
            if (chain == null)
            {
                return NotFound();
            }

            db.Chains.Remove(chain);
            db.SaveChanges();

            return Ok(chain);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChainExists(int id)
        {
            return db.Chains.Count(e => e.Id == id) > 0;
        }
    }
}
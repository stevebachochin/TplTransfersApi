using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TplTransfersApi.Models;

namespace TplTransfersApi.Controllers
{
    public class Depopulation_BatchController : ApiController
    {
        private DbEntities db = new DbEntities();

        // GET: api/Depopulation_Batch
        [HttpGet]
        [Route("api/depopbatch")]
        public IQueryable<Depopulation_Batch> GetDepopulation_Batch()
        {
            return db.Depopulation_Batch;
        }

        // GET: api/Depopulation_Batch/5
        [HttpGet]
        [Route("api/depopbatch/{id}")]
        [ResponseType(typeof(Depopulation_Batch))]
        public async Task<IHttpActionResult> GetDepopulation_Batch(int id)
        {
            Depopulation_Batch depopulation_Batch = await db.Depopulation_Batch.FindAsync(id);
            if (depopulation_Batch == null)
            {
                return NotFound();
            }

            return Ok(depopulation_Batch);
        }

        // PUT: api/Depopulation_Batch/5
        [HttpPut]
        [Route("api/depopbatch/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDepopulation_Batch(int id, Depopulation_Batch depopulation_Batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != depopulation_Batch.did)
            {
                return BadRequest();
            }

            db.Entry(depopulation_Batch).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Depopulation_BatchExists(id))
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

        // POST: api/Depopulation_Batch
        [HttpPost]
        [Route("api/depopbatch")]
        [ResponseType(typeof(Depopulation_Batch))]
        public async Task<IHttpActionResult> PostDepopulation_Batch(Depopulation_Batch depopulation_Batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Depopulation_Batch.Add(depopulation_Batch);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = depopulation_Batch.did }, depopulation_Batch);
        }

        // DELETE: api/Depopulation_Batch/5
        [Route("api/depopbatch/{id}")]
        [HttpDelete]
        [ResponseType(typeof(Depopulation_Batch))]
        public async Task<IHttpActionResult> DeleteDepopulation_Batch(int id)
        {
            Depopulation_Batch depopulation_Batch = await db.Depopulation_Batch.FindAsync(id);
            if (depopulation_Batch == null)
            {
                return NotFound();
            }

            db.Depopulation_Batch.Remove(depopulation_Batch);
            await db.SaveChangesAsync();

            return Ok(depopulation_Batch);
        }
        //GET RECORD BY DONOR ID
        [HttpGet]
        [Route("api/depopbatch/filter/{DonorID}")]
        public IHttpActionResult GetBatchRecordsByDonorID(string DonorID = null)
        {

            if (!string.IsNullOrEmpty(DonorID))
            {
                var source = Enumerable.Empty<Depopulation_Batch>().AsQueryable();
                source = (
                    from recordList in db.Depopulation_Batch.AsEnumerable()
                    where recordList.DonorID.ToLower() == DonorID.ToLower()
                    select recordList
                    ).AsQueryable();

                //return Ok(source.ToList().FirstOrDefault());
                return Ok(source.ToList());

            }

            return NotFound();

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Depopulation_BatchExists(int id)
        {
            return db.Depopulation_Batch.Count(e => e.did == id) > 0;
        }
    }
}
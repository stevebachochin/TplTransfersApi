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
    public class Transfer_SIDController : ApiController
    {
        private DbEntities db = new DbEntities();

        // GET: api/Transfer_SID
        [HttpGet]
        [Route("api/transfersid")]
        public IQueryable<Transfer_SID> GetTransfer_SID()
        {
            return db.Transfer_SID;
        }

        // GET: api/Transfer_SID/5
        [HttpGet]
        [Route("api/transfersid/{id}")]
        [ResponseType(typeof(Transfer_SID))]
        public async Task<IHttpActionResult> GetTransfer_SID(int id)
        {
            Transfer_SID transfer_SID = await db.Transfer_SID.FindAsync(id);
            if (transfer_SID == null)
            {
                return NotFound();
            }

            return Ok(transfer_SID);
        }

        // PUT: api/Transfer_SID/5
        [HttpPut]
        [Route("api/transfersid/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransfer_SID(int id, Transfer_SID transfer_SID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transfer_SID.tsid)
            {
                return BadRequest();
            }

            db.Entry(transfer_SID).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Transfer_SIDExists(id))
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

        // POST: api/Transfer_SID
        [HttpPost]
        [Route("api/transfersid")]
        [ResponseType(typeof(Transfer_SID))]
        public async Task<IHttpActionResult> PostTransfer_SID(Transfer_SID transfer_SID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transfer_SID.Add(transfer_SID);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = transfer_SID.tsid }, transfer_SID);
        }

        // DELETE: api/Transfer_SID/5
        [HttpDelete]
        [Route("api/transfersid/{id}")]
        [ResponseType(typeof(Transfer_SID))]
        public async Task<IHttpActionResult> DeleteTransfer_SID(int id)
        {
            Transfer_SID transfer_SID = await db.Transfer_SID.FindAsync(id);
            if (transfer_SID == null)
            {
                return NotFound();
            }

            db.Transfer_SID.Remove(transfer_SID);
            await db.SaveChangesAsync();

            return Ok(transfer_SID);
        }

        //GET RECORD BY DONOR ID
        [HttpGet]
        [Route("api/transfersid/filter/{UNID}")]
        public IHttpActionResult GetSIDRecordsByUNID(string UNID = null)
        {

            if (!string.IsNullOrEmpty(UNID))
            {
                var source = Enumerable.Empty<Transfer_SID>().AsQueryable();
                source = (
                    from recordList in db.Transfer_SID.AsEnumerable()
                    where recordList.UNID.ToLower() == UNID.ToLower()
                    select recordList
                    ).AsQueryable();

                return Ok(source.ToList().FirstOrDefault());

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

        private bool Transfer_SIDExists(int id)
        {
            return db.Transfer_SID.Count(e => e.tsid == id) > 0;
        }
    }
}
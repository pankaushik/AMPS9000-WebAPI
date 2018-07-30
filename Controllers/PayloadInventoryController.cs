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
using AMPS9000_WebAPI;

namespace AMPS9000_WebAPI.Controllers
{
    public class PayloadInventoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PayloadInventory
        public IQueryable<DropDownDTO> GetPayloadInventory()
        {
            var results = (from a in db.PayloadInventory
                           select new DropDownDTO { id = a.id, description = a.serialNumber });

            return results;
        }

        // GET: api/PayloadInventoryData
        public IHttpActionResult GetPayloadInventoryData()
        {
            var results = (from a in db.PayloadInventory
                           join b in db.Payloads on a.metaDataID equals b.PayloadID
                           join c in db.PayloadTypes on b.PayloadType equals c.id into lojType
                           from d in lojType.DefaultIfEmpty()
                           join e in db.Units on a.owningUnit equals e.id into lojUnit
                           from f in lojUnit.DefaultIfEmpty()
                           join g in db.BranchSubordinateTier1 on f.branchSubordinateTier1ID equals g.id into lojBST
                           from h in lojBST.DefaultIfEmpty()
                           join i in db.Locations on a.locationID equals i.LocationID
                           select new
                           {
                               ID = a.id,
                               type = d.abbreviation ?? "Unknown",
                               typeDesc = d.description ?? "Unknown",
                               name = b.PayloadName, 
                               serialNumber = a.serialNumber ?? "Unknown",
                               COCOM = db.COCOMs.Where(x => x.id == h.COCOM).Select(x => x.description).FirstOrDefault() ?? "Unknown",
                               location = i.LocationName,
                               recordDate = a.lastUpdate
                           });

            return Ok(results);
        }

        // GET: api/PayloadInventory/{guid}
        [ResponseType(typeof(PayloadInventory))]
        public IHttpActionResult GetPayloadInventory(string id)
        {
            PayloadInventory payloadInventory = db.PayloadInventory.Find(id);
            if (payloadInventory == null)
            {
                return NotFound();
            }

            return Ok(payloadInventory);
        }

        // PUT: api/PayloadInventory/{guid}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayloadInventory(string id, PayloadInventory payloadInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payloadInventory.id)
            {
                return BadRequest();
            }

            db.Entry(payloadInventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadInventoryExists(id))
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

        // POST: api/PayloadInventory
        [ResponseType(typeof(PayloadInventory))]
        public IHttpActionResult PostPayloadInventory(PayloadInventory payloadInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            payloadInventory.id = Guid.NewGuid().ToString();
            payloadInventory.lastUpdate = DateTime.Now;
            payloadInventory.lastUpdateUserId = "000";

            db.PayloadInventory.Add(payloadInventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PayloadInventoryExists(payloadInventory.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.PayloadStatus.Add(new PayloadStatu
            {
                PayloadInventoryID = payloadInventory.id,
                StatusCode = (int)AssetStatuses.FLIGHT_READY,
                ETIC = DateTime.Now.Add(new TimeSpan(90, 0, 0, 0)),
                lastUpdate = DateTime.Now,
                lastUpdateUserId = "1"
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = payloadInventory.id }, payloadInventory);
        }

        // DELETE: api/PayloadInventory/{guid}
        [ResponseType(typeof(PayloadInventory))]
        public IHttpActionResult DeletePayloadInventory(string id)
        {
            PayloadInventory payloadInventory = db.PayloadInventory.Find(id);
            if (payloadInventory == null)
            {
                return NotFound();
            }

            db.PayloadInventory.Remove(payloadInventory);
            db.SaveChanges();

            return Ok(payloadInventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayloadInventoryExists(string id)
        {
            return db.PayloadInventory.Count(e => e.id == id) > 0;
        }
    }
}
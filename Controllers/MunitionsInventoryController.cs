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
    public class MunitionsInventoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/MunitionsInventory
        public IQueryable<DropDownDTO> GetMunitionsInventory()
        {
            var results = (from a in db.MunitionsInventory
                           select new DropDownDTO { id = a.id, description = a.serialNumber });

            return results;
        }

        // GET: api/MunitionsInventoryData
        public IHttpActionResult GetMunitionsInventoryData()
        {
            var results = (from a in db.MunitionsInventory
                           join b in db.Munitions on a.metaDataID equals b.MunitionID
                           join c in db.MunitionRoles on b.MunitionRole equals c.id into lojRole
                           from d in lojRole.DefaultIfEmpty()
                           join e in db.Units on a.owningUnit equals e.id into lojUnit
                           from f in lojUnit.DefaultIfEmpty()
                           join g in db.BranchSubordinateTier1 on f.branchSubordinateTier1ID equals g.id into lojBST
                           from h in lojBST.DefaultIfEmpty()
                           join i in db.Locations on a.locationID equals i.LocationID
                           select new
                           {
                               ID = a.id,
                               type = d.description,
                               name = b.MunitionName,
                               serialNumber = a.serialNumber ?? "Unknown",
                               COCOM = db.COCOMs.Where(x => x.id == h.COCOM).Select(x => x.description).FirstOrDefault() ?? "Unknown",
                               owningUnit = f.description ?? "Unknown",
                               location = i.LocationName,
                               recordDate = a.lastUpdate
                           });

            return Ok(results);
        }

        // GET: api/MunitionsInventory/{guid}
        [ResponseType(typeof(MunitionsInventory))]
        public IHttpActionResult GetMunitionsInventory(string id)
        {
            MunitionsInventory munitionsInventory = db.MunitionsInventory.Find(id);
            if (munitionsInventory == null)
            {
                return NotFound();
            }

            return Ok(munitionsInventory);
        }

        // PUT: api/MunitionsInventory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMunitionsInventory(string id, MunitionsInventory munitionsInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != munitionsInventory.id)
            {
                return BadRequest();
            }

            db.Entry(munitionsInventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunitionsInventoryExists(id))
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

        // POST: api/MunitionsInventory
        [ResponseType(typeof(MunitionsInventory))]
        public IHttpActionResult PostMunitionsInventory(MunitionsInventory munitionsInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            munitionsInventory.id = Guid.NewGuid().ToString();
            munitionsInventory.lastUpdate = DateTime.Now;
            munitionsInventory.lastUpdateUserId = "000";

            db.MunitionsInventory.Add(munitionsInventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MunitionsInventoryExists(munitionsInventory.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.MunitionStatus.Add(new MunitionStatu
            {
                MunitionInventoryID = munitionsInventory.id,
                StatusCode = (int)AssetStatuses.FLIGHT_READY,
                ETIC = DateTime.Now.Add(new TimeSpan(90, 0, 0, 0)),
                lastUpdate = DateTime.Now,
                lastUpdateUserId = "000"
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = munitionsInventory.id }, munitionsInventory);
        }

        // DELETE: api/MunitionsInventory/{guid}
        [ResponseType(typeof(MunitionsInventory))]
        public IHttpActionResult DeleteMunitionsInventory(string id)
        {
            MunitionsInventory munitionsInventory = db.MunitionsInventory.Find(id);
            if (munitionsInventory == null)
            {
                return NotFound();
            }

            db.MunitionsInventory.Remove(munitionsInventory);
            db.SaveChanges();

            return Ok(munitionsInventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MunitionsInventoryExists(string id)
        {
            return db.MunitionsInventory.Count(e => e.id == id) > 0;
        }
    }
}
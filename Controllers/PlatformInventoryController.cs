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
    public class PlatformInventoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PlatformInventory
        public IHttpActionResult GetPlatformInventory()
        {
            var results = (from a in db.PlatformInventory
                           select new DropDownDTO
                           {
                               id = a.id,
                               description = a.tailNumber
                           });

            return Ok(results);
        }

        // GET: api/PlatformInventoryData
        public IHttpActionResult GetPlatformInventoryData()
        {
            var results = (from a in db.PlatformInventory
                           join b0 in db.Platforms on a.metaDataID equals b0.PlatformID
                           join b in db.PlatformStatus on a.id equals b.PlatformInventoryID into lojStat
                           join c in db.Locations on a.locationID equals c.LocationID into lojLoc
                           join d in db.Units on a.owningUnit equals d.id into lojUnit
                           from e in lojStat.DefaultIfEmpty()
                           from f in lojLoc.DefaultIfEmpty()
                           from g in lojUnit.DefaultIfEmpty()
                           join h in db.BranchSubordinateTier1 on g.branchSubordinateTier1ID equals h.id into lojBST
                           from i in lojBST.DefaultIfEmpty()
                           select new 
                           {
                               id = a.id,
                               description = a.tailNumber,
                               status = db.StatusCodes.Where(x => x.id == e.StatusCode && x.type == 3).Select(x => x.description).FirstOrDefault() ?? "Unknown",
                               statusComment = e.StatusComments ?? "None",
                               ETIC = e.ETIC,
                               location = f.LocationName ?? "Unknown",
                               owningUnit = g.description ?? "Unknown",
                               category = db.PlatformCategories.Where(x => x.id == b0.PlatformCategory).Select(x => x.abbreviation).FirstOrDefault() ?? "Unknown",
                               categoryDesc = db.PlatformCategories.Where(x => x.id == b0.PlatformCategory).Select(x => x.description).FirstOrDefault() ?? "Unknown",
                               branchOfService = db.BranchOfServices.Where(x => x.id == i.branchOfService).Select(x => x.description).FirstOrDefault() ?? "Unknown"
                           });

            return Ok(results);
        }

        // GET: api/PlatformInventory/5
        [ResponseType(typeof(PlatformInventory))]
        public IHttpActionResult GetPlatformInventory(string id)
        {
            PlatformInventory platformInventory = db.PlatformInventory.Find(id);
            if (platformInventory == null)
            {
                return NotFound();
            }

            return Ok(platformInventory);
        }

        // PUT: api/PlatformInventory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlatformInventory(string id, PlatformInventory platformInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platformInventory.id)
            {
                return BadRequest();
            }

            db.Entry(platformInventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformInventoryExists(id))
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

        // POST: api/PlatformInventory
        [ResponseType(typeof(PlatformInventory))]
        public IHttpActionResult PostPlatformInventory(PlatformInventory platformInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            platformInventory.id = Guid.NewGuid().ToString();
            platformInventory.lastUpdate = DateTime.Now;
            platformInventory.lastUpdateUserId = "000";

            db.PlatformInventory.Add(platformInventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlatformInventoryExists(platformInventory.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.PlatformStatus.Add(new PlatformStatu
            {
                PlatformInventoryID = platformInventory.id,
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

            return CreatedAtRoute("DefaultApi", new { id = platformInventory.id }, platformInventory);
        }

        // DELETE: api/PlatformInventory/5
        [ResponseType(typeof(PlatformInventory))]
        public IHttpActionResult DeletePlatformInventory(string id)
        {
            PlatformInventory platformInventory = db.PlatformInventory.Find(id);
            if (platformInventory == null)
            {
                return NotFound();
            }

            db.PlatformInventory.Remove(platformInventory);
            db.SaveChanges();

            return Ok(platformInventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformInventoryExists(string id)
        {
            return db.PlatformInventory.Count(e => e.id == id) > 0;
        }
    }
}
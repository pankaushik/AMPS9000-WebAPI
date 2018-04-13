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
using System.Web.Http.Cors;

namespace AMPS9000_WebAPI.Controllers
{
    public class LocationsController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Locations
        public IQueryable<DropDownDTO> GetLocations()
        {
            var result = (from a in db.Locations
                          orderby a.LocationName ascending
                          select new DropDownDTO { id = a.LocationID.ToString(), description = a.LocationName.Trim() }).AsQueryable();
            return result;
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocations(string id)
        {
            Location locations = db.Locations.Find(id);
            if (locations == null)
            {
                return NotFound();
            }

            return Ok(locations);
        }

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocations(string id, Location locations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locations.LocationID)
            {
                return BadRequest();
            }

            db.Entry(locations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationsExists(id))
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

        // POST: api/Locations
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocations(Location locations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(locations.LocationName == null || locations.LocationName == "")
            {
                return BadRequest("Invalid Location Name");
            }

            if(locations.LocationRegion == null)
            {
                return BadRequest("Invalid Region");
            }

            if (locations.LocationCountry == null)
            {
                return BadRequest("Invalid Country");
            }

            locations.LocationID = Guid.NewGuid().ToString();
            db.Locations.Add(locations);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LocationsExists(locations.LocationID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = locations.LocationID }, locations);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocations(string id)
        {
            Location locations = db.Locations.Find(id);
            if (locations == null)
            {
                return NotFound();
            }

            db.Locations.Remove(locations);
            db.SaveChanges();

            return Ok(locations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationsExists(string id)
        {
            return db.Locations.Count(e => e.LocationID == id) > 0;
        }
    }
}
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

        // GET: api/Locations/{guid}
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

        // GET: api/Locations/GetLocationsData
        public IHttpActionResult GetLocationsData()
        {
            var result = (from a in db.Locations
                          join b in db.COCOMs on a.LocationCOCOM equals b.id into lojCOCOM
                          join c in db.Regions on a.LocationRegion equals c.id into lojReg
                          join d in db.Countries on a.LocationCountry equals d.id into lojCountry
                          from e in lojCOCOM.DefaultIfEmpty()
                          from f in lojReg.DefaultIfEmpty()
                          from g in lojCountry.DefaultIfEmpty()
                          orderby a.LocationName ascending
                          select new {
                              id = a.LocationID.ToString(),
                              name = a.LocationName.Trim(),
                              COCOM = e.description == null ? "Unknown" : e.description.Trim(),
                              country = g.description == null ? "Unknown" : g.description.Trim(),
                              region = f.description == null ? "Unknown" : f.description.Trim(),
                              category = a.LocationCategory1.description == null ? "Unknown" : a.LocationCategory1.description.Trim(),
                              lastUpdate = a.LastUpdate
                          }).AsQueryable();
            return Ok(result);

        }

        // PUT: api/Locations/{guid}
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

            if (locations.LocationName == null || locations.LocationName.ToString().Trim() == "")
            {
                return BadRequest("Invalid Location Name");
            }

            if (locations.LocationRegion == null)
            {
                return BadRequest("Invalid Region");
            }
            else if (db.Regions.Where(x => x.id == locations.LocationRegion).Count() <= 0)
            {
                return BadRequest("Invalid Region");
            }

            if (locations.LocationCountry == null)
            {
                return BadRequest("Invalid Country");
            }
            else if (db.Countries.Where(x => x.id == locations.LocationCountry).Count() <= 0)
            {
                return BadRequest("Invalid Country");
            }

            if (locations.LocationCOCOM == null || locations.LocationCOCOM.ToString().Trim() == "")
            {
                return BadRequest("Invalid COCOM");
            }
            else if (db.COCOMs.Where(x => x.id == locations.LocationCOCOM).Count() <= 0)
            {
                return BadRequest("Invalid COCOM");
            }

            if(locations.LocationCategory != null && locations.LocationCategory.ToString().Trim() != "")
            {
                if(db.LocationCategories.Where(x => x.id == locations.LocationCategory).Count() <= 0)
                {
                    return BadRequest("Invalid Location Category");
                }
            }

            locations.LastUpdate = DateTime.Now;

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

            if(locations.LocationName == null || locations.LocationName.ToString().Trim() == "")
            {
                return BadRequest("Invalid Location Name");
            } 

            if(locations.LocationRegion == null)
            {
                return BadRequest("Invalid Region");
            } else if(db.Regions.Where(x => x.id == locations.LocationRegion).Count() <= 0)
            {
                return BadRequest("Invalid Region");
            }

            if (locations.LocationCountry == null)
            {
                return BadRequest("Invalid Country");
            } else if (db.Countries.Where(x => x.id == locations.LocationCountry).Count() <= 0)
            {
                return BadRequest("Invalid Country");
            }

            if (locations.LocationCOCOM == null || locations.LocationCOCOM.ToString().Trim() == "")
            {
                return BadRequest("Invalid COCOM");
            } else if (db.COCOMs.Where(x => x.id == locations.LocationCOCOM).Count() <= 0)
            {
                return BadRequest("Invalid COCOM");
            }

            if (locations.LocationCategory != null && locations.LocationCategory.ToString().Trim() != "")
            {
                if (db.LocationCategories.Where(x => x.id == locations.LocationCategory).Count() <= 0)
                {
                    return BadRequest("Invalid Location Category");
                }
            }

            locations.LocationID = Guid.NewGuid().ToString();
            locations.LastUpdate = DateTime.Now;
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
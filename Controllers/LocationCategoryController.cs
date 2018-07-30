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
    public class LocationCategoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/LocationCategory
        public IQueryable<LocationCategory> GetLocationCategories()
        {
            return db.LocationCategories;
        }

        // GET: api/LocationCategory/5
        [ResponseType(typeof(LocationCategory))]
        public IHttpActionResult GetLocationCategory(int id)
        {
            LocationCategory locationCategory = db.LocationCategories.Find(id);
            if (locationCategory == null)
            {
                return NotFound();
            }

            return Ok(locationCategory);
        }

        // PUT: api/LocationCategory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocationCategory(int id, LocationCategory locationCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationCategory.id)
            {
                return BadRequest();
            }

            db.Entry(locationCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationCategoryExists(id))
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

        // POST: api/LocationCategory
        [ResponseType(typeof(LocationCategory))]
        public IHttpActionResult PostLocationCategory(LocationCategory locationCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LocationCategories.Add(locationCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = locationCategory.id }, locationCategory);
        }

        // DELETE: api/LocationCategory/5
        [ResponseType(typeof(LocationCategory))]
        public IHttpActionResult DeleteLocationCategory(int id)
        {
            LocationCategory locationCategory = db.LocationCategories.Find(id);
            if (locationCategory == null)
            {
                return NotFound();
            }

            db.LocationCategories.Remove(locationCategory);
            db.SaveChanges();

            return Ok(locationCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationCategoryExists(int id)
        {
            return db.LocationCategories.Count(e => e.id == id) > 0;
        }
    }
}
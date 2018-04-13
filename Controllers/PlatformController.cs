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
    public class PlatformController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Platform
        public IQueryable<DropDownDTO> GetPlatforms()
        {
            var result = (from a in db.Platforms
                          orderby a.PlatformName ascending
                          select new DropDownDTO { id = a.PlatformID.ToString(), description = a.PlatformName }).AsQueryable();
            return result;
        }

        // GET: api/Platform/5
        [ResponseType(typeof(Platform))]
        public IHttpActionResult GetPlatform(string id)
        {
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }

        // PUT: api/Platform/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlatform(string id, Platform platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platform.PlatformID)
            {
                return BadRequest();
            }

            db.Entry(platform).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(id))
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

        // POST: api/Platform
        [ResponseType(typeof(Platform))]
        public IHttpActionResult PostPlatform(Platform platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            platform.PlatformID = Guid.NewGuid().ToString();

            db.Platforms.Add(platform);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlatformExists(platform.PlatformID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = platform.PlatformID }, platform);
        }

        // DELETE: api/Platform/5
        [ResponseType(typeof(Platform))]
        public IHttpActionResult DeletePlatform(string id)
        {
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                return NotFound();
            }

            db.Platforms.Remove(platform);
            db.SaveChanges();

            return Ok(platform);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformExists(string id)
        {
            return db.Platforms.Count(e => e.PlatformID == id) > 0;
        }
    }
}
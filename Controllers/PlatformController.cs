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

        // GET: api/Platform/{guid}
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

        // GET: api/platform/GetPlatformsData
        public IHttpActionResult GetPlatformsData()
        {
            var result = (from a in db.Platforms
                          join b in db.PlatformCategories on a.PlatformCategory equals b.id into lojCatg
                          join c in db.PlatformRoles on a.PlatformRole equals c.id into lojRole
                          from d in lojCatg.DefaultIfEmpty()
                          from e in lojRole.DefaultIfEmpty()
                          select new
                          {
                              ID = a.PlatformID,
                              platform = a.PlatformName,
                              category = d.abbreviation,
                              categoryDesc = d.description,
                              role = e.description
                          });

            return Ok(result);
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

            if (platform.PlatformName == null || platform.PlatformName.Trim() == "")
            {
                return BadRequest("Invalid Platform Name: " + platform.PlatformName);
            }

            if (!PlatformCategoryExists(platform.PlatformCategory))
            {
                return BadRequest("Invalid Platform Category: " + platform.PlatformCategory);
            }

            if (!PlatformRoleExists(platform.PlatformRole))
            {
                return BadRequest("Invalid Platform Role: " + platform.PlatformRole);
            }

            if (!IsValidMOS(platform.PlatformFlightCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformFlightCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformLineCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformLineCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPayloadCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPayloadCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPEDCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPEDCrewMOS);
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

            if(platform.PlatformName == null || platform.PlatformName.Trim() == "")
            {
                return BadRequest("Invalid Platform Name: " + platform.PlatformName);
            }

            if(!PlatformCategoryExists(platform.PlatformCategory))
            {
                return BadRequest("Invalid Platform Category: " + platform.PlatformCategory);
            }

            if(!PlatformRoleExists(platform.PlatformRole))
            {
                return BadRequest("Invalid Platform Role: " + platform.PlatformRole);
            }

            if(!IsValidMOS(platform.PlatformFlightCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformFlightCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformLineCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformLineCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPayloadCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPayloadCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPEDCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPEDCrewMOS);
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

        private bool IsValidMOS(int? platformFlightCrewMOS)
        {
            if (platformFlightCrewMOS == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.MOS_Desc.Count(e => e.id == platformFlightCrewMOS) > 0;
            }
        }

        private bool PlatformRoleExists(int? platformRole)
        {
            if (platformRole == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.PlatformRoles.Count(e => e.id == platformRole) > 0;
            }
        }

        private bool PlatformCategoryExists(int? platformCategory)
        {
            if (platformCategory == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.PlatformCategories.Count(e => e.id == platformCategory) > 0;
            }
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

        private bool PayloadExists(string id)
        {
            return db.Payloads.Count(e => e.PayloadID == id) > 0;
        }
    }
}
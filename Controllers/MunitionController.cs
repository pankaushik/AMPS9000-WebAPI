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
    public class MunitionController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Munition
        public IQueryable<DropDownDTO> GetMunitions()
        {
            var result = (from a in db.Munitions
                          orderby a.MunitionName ascending
                          select new DropDownDTO { id = a.MunitionID.ToString(), description = a.MunitionName }).AsQueryable();
            return result;
        }

        // GET: api/Munition/5
        [ResponseType(typeof(Munition))]
        public IHttpActionResult GetMunition(string id)
        {
            Munition munition = db.Munitions.Find(id);
            if (munition == null)
            {
                return NotFound();
            }

            return Ok(munition);
        }

        // GET: api/GetMunitionsData
        [ResponseType(typeof(Munition))]
        public IHttpActionResult GetMunitionsData()
        {
            var result = (from a in db.Munitions
                          join b in db.MunitionRoles on a.MunitionRole equals b.id into lojRole
                          from c in lojRole.DefaultIfEmpty()
                          select new
                          {
                              ID = a.MunitionID,
                              munition = a.MunitionName,
                              role = c.description ?? "Unknown",
                              reference = a.MunitionsReferenceCode ?? ""
                          });

            return Ok(result);
        }


        // PUT: api/Munition/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMunition(string id, Munition munition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != munition.MunitionID)
            {
                return BadRequest();
            }

            if (munition.MunitionName == null || munition.MunitionName.Trim() == "")
            {
                return BadRequest("Invalid Munition Name: " + munition.MunitionName);
            }

            if (!IsValidMunitionRole(munition.MunitionRole))
            {
                return BadRequest("Invalid Munition Role: " + munition.MunitionRole);
            }

            if (!IsValidMOS(munition.MunitionMOS1))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS1);
            }

            if (!IsValidMOS(munition.MunitionMOS2))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS2);
            }

            if (!IsValidMOS(munition.MunitionMOS3))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS3);
            }

            db.Entry(munition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunitionExists(id))
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

        private bool IsValidMunitionRole(int? munitionRole)
        {
            if (munitionRole == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.MunitionRoles.Count(e => e.id == munitionRole) > 0;
            }
        }

        // POST: api/Munition
        [ResponseType(typeof(Munition))]
        public IHttpActionResult PostMunition(Munition munition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(munition.MunitionName == null || munition.MunitionName.Trim() == "")
            {
                return BadRequest("Invalid Munition Name: " + munition.MunitionName);
            }

            if (!IsValidMunitionRole(munition.MunitionRole))
            {
                return BadRequest("Invalid Munition Role: " + munition.MunitionRole);
            }

            if (!IsValidMOS(munition.MunitionMOS1))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS1);
            }

            if (!IsValidMOS(munition.MunitionMOS2))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS2);
            }

            if (!IsValidMOS(munition.MunitionMOS3))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS3);
            }

            munition.MunitionID = Guid.NewGuid().ToString();

            db.Munitions.Add(munition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MunitionExists(munition.MunitionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = munition.MunitionID }, munition);
        }

        // DELETE: api/Munition/5
        [ResponseType(typeof(Munition))]
        public IHttpActionResult DeleteMunition(string id)
        {
            Munition munition = db.Munitions.Find(id);
            if (munition == null)
            {
                return NotFound();
            }

            db.Munitions.Remove(munition);
            db.SaveChanges();

            return Ok(munition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MunitionExists(string id)
        {
            return db.Munitions.Count(e => e.MunitionID == id) > 0;
        }

        private bool IsValidMOS(int? MOS)
        {
            if (MOS == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.MOS_Desc.Count(e => e.id == MOS) > 0;
            }
        }
    }
}
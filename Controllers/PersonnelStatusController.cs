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
    public class PersonnelStatusController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PersonnelStatus
        public IHttpActionResult GetPersonnelStatus()
        {
            var results = (from a in db.Personnels
                           join b in db.PersonnelStatus on a.PersonnelID equals b.PersonnelID
                           orderby a.LastName
                           select new
                           {
                               FirstName = a.FirstName,
                               LastName = a.LastName,
                               CACID = a.CACid,
                               Status = new 
                               {
                                   id = b.StatusCode,
                                   description = db.StatusCodes.Where(x => x.id == b.StatusCode && x.type == 1).Select(x => x.description).FirstOrDefault()
                               },
                               Rank = new
                               {
                                   id = a.Rank,
                                   abbreviation = db.Ranks.Where(x => x.id == a.Rank).Select(x => x.rankAbbreviation).FirstOrDefault(),
                                   description = db.Ranks.Where(x => x.id == a.Rank).Select(x => x.description).FirstOrDefault()
                               },
                               BranchOfService = new
                               {
                                   id = a.ServiceBranch,
                                   description = db.BranchOfServices.Where(x => x.id == a.ServiceBranch).Select(x => x.description).FirstOrDefault()
                               },
                               DeployedUnit = new
                               {
                                   id = a.DeployedUnit,
                                   description = db.Units.Where(x => x.id == a.DeployedUnit).Select(x => x.description).FirstOrDefault()
                               }
                           });

            return Ok(results);
        }

        // GET: api/PersonnelStatus/5
        [ResponseType(typeof(PersonnelStatu))]
        public IHttpActionResult GetPersonnelStatu(string id)
        {
            PersonnelStatu personnelStatu = db.PersonnelStatus.Find(id);
            if (personnelStatu == null)
            {
                return NotFound();
            }

            return Ok(personnelStatu);
        }

        // PUT: api/PersonnelStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonnelStatu(string id, PersonnelStatu personnelStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personnelStatu.PersonnelID)
            {
                return BadRequest();
            }

            db.Entry(personnelStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnelStatuExists(id))
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

        // POST: api/PersonnelStatus
        [ResponseType(typeof(PersonnelStatu))]
        public IHttpActionResult PostPersonnelStatu(PersonnelStatu personnelStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonnelStatus.Add(personnelStatu);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonnelStatuExists(personnelStatu.PersonnelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = personnelStatu.PersonnelID }, personnelStatu);
        }

        // DELETE: api/PersonnelStatus/5
        [ResponseType(typeof(PersonnelStatu))]
        public IHttpActionResult DeletePersonnelStatu(string id)
        {
            PersonnelStatu personnelStatu = db.PersonnelStatus.Find(id);
            if (personnelStatu == null)
            {
                return NotFound();
            }

            db.PersonnelStatus.Remove(personnelStatu);
            db.SaveChanges();

            return Ok(personnelStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonnelStatuExists(string id)
        {
            return db.PersonnelStatus.Count(e => e.PersonnelID == id) > 0;
        }
    }
}
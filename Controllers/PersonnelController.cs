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
    public class PersonnelController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Personnel
        public IQueryable<DropDownDTO> GetPersonnels()
        {
            var result = (from a in db.Personnels
                          orderby a.FirstName ascending
                          select new DropDownDTO { id = a.PersonnelID.ToString(), description = (a.Rank1.rankAbbreviation.Trim() + " " + a.FirstName.Trim() + " " + a.LastName.Trim()) }).AsQueryable();
            return result;
        }

        // GET: api/Personnel/5
        [ResponseType(typeof(Personnel))]
        public IHttpActionResult GetPersonnel(string id)
        {
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return NotFound();
            }

            return Ok(personnel);
        }

        // PUT: api/Personnel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonnel(string id, Personnel personnel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personnel.PersonnelID)
            {
                return BadRequest();
            }

            db.Entry(personnel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnelExists(id))
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

        // POST: api/Personnel
        [ResponseType(typeof(Personnel))]
        public IHttpActionResult PostPersonnel(Personnel personnel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            personnel.PersonnelID = Guid.NewGuid().ToString();
            personnel.CreateDate = DateTime.Now;

            db.Personnels.Add(personnel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonnelExists(personnel.PersonnelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = personnel.PersonnelID }, personnel);
        }

        // DELETE: api/Personnel/5
        [ResponseType(typeof(Personnel))]
        public IHttpActionResult DeletePersonnel(string id)
        {
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return NotFound();
            }

            db.Personnels.Remove(personnel);
            db.SaveChanges();

            return Ok(personnel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonnelExists(string id)
        {
            return db.Personnels.Count(e => e.PersonnelID == id) > 0;
        }
    }
}
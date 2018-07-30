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
        public IQueryable<DropDownDTO> GetPersonnel()
        {
            var result = (from a in db.Personnels
                          orderby a.FirstName ascending
                          select new DropDownDTO { id = a.PersonnelID.ToString(), description = (a.Rank1.rankAbbreviation.Trim() + " " + a.FirstName.Trim() + " " + a.LastName.Trim()) }).AsQueryable();
            return result;
        }

        // GET: api/Personnel/{guid}
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

        // GET: api/Personnel/GetPersonnelData
        public IHttpActionResult GetPersonnelData()
        {
            var result = (from a in db.Personnels
                          join b in db.PersonnelStatus on a.PersonnelID equals b.PersonnelID
                          join c in db.MOS_Desc on a.MOS1 equals c.id into lojMOS1
                          from d in lojMOS1.DefaultIfEmpty()
                          join e in db.MOS_Desc on a.MOS2 equals e.id into lojMOS2
                          from f in lojMOS2.DefaultIfEmpty()
                          join g in db.MOS_Desc on a.MOS3 equals g.id into lojMOS3
                          from h in lojMOS3.DefaultIfEmpty()
                          select new
                          {
                              ID = a.PersonnelID,
                              CACID = a.CACid ?? "Unknown",
                              fullName = a.LastName + ", " + a.FirstName,
                              firstName = a.FirstName,
                              lastName = a.LastName,
                              company = db.Companies.Where(x => x.id == a.Company).Select(x => x.description).FirstOrDefault(),
                              rank = new
                              {
                                  id = a.Rank,
                                  abbreviation = db.Ranks.Where(x => x.id == a.Rank).Select(x => x.rankAbbreviation).FirstOrDefault(),
                                  description = db.Ranks.Where(x => x.id == a.Rank).Select(x => x.description).FirstOrDefault(),
                              },
                              branchOfService = new
                              {
                                  id = a.ServiceBranch,
                                  description = db.BranchOfServices.Where(x => x.id == a.ServiceBranch).Select(x => x.description).FirstOrDefault()
                              },
                              mos = d.MOSCode + ", " + f.MOSCode + ", " + h.MOSCode,
                              duty = a.DutyPosition.description,
                              arrive = b.PersonnelArrive,
                              depart = b.PersonnelDepart,
                              deployedUnit = new
                              {
                                  id = a.DeployedUnit,
                                  description = db.Units.Where(x => x.id == a.DeployedUnit).Select(x => x.description).FirstOrDefault()
                              },
                              assignedUnit = new
                              {
                                  id = a.AssignedUnit,
                                  description = db.Units.Where(x => x.id == a.AssignedUnit).Select(x => x.description).FirstOrDefault()
                              }
                          });

            return Ok(result);
        }

        // PUT: api/Personnel/{guid}
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

                PersonnelStatu pStatus = new PersonnelStatu
                {
                    PersonnelID = personnel.PersonnelID,
                    StatusCode = (int)PersonnelStatuses.PENDING,
                    PersonnelArrive = personnel.CurrentAssignmentStart ?? DateTime.Today,
                    PersonnelDepart = personnel.CurrentAssignmentEnd ?? DateTime.Today,
                    lastUpdate = DateTime.Now,
                    lastUpdateUserId = "000"
                };

                db.PersonnelStatus.Add(pStatus);
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
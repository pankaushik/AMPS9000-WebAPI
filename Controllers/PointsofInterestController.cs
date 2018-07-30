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
    public class PointsofInterestController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PointsofInterest
        public IQueryable<DropDownDTO> GetPointsofInterests()
        {
            var result = (from a in db.PointsofInterests
                          orderby a.Name ascending
                          select new DropDownDTO { id = a.PointofInterestID.ToString(), description = a.Name.Trim() }).AsQueryable();

            return result;
        }

        // GET: api/PointsofInterest/5
        [ResponseType(typeof(PointsofInterest))]
        public IHttpActionResult GetPointsofInterest(string id)
        {
            PointsofInterest pointsofInterest = db.PointsofInterests.Find(id);
            if (pointsofInterest == null)
            {
                return NotFound();
            }

            return Ok(pointsofInterest);
        }

        
        [ResponseType(typeof(PointsofInterest))]
        public IHttpActionResult GetPointsofInterestData()
        {
            var result = (from a in db.PointsofInterests
                          orderby a.Name ascending
                          select new
                          {
                              id = a.PointofInterestID,
                              name = a.Name,
                              description = a.Description,
                              latitude = a.Latitude,
                              longitude = a.Longitude,
                              MGRS = a.MGRS,
                              elevation = a.Elevation,
                              imageFile = a.Image,
                              document = a.Document,
                              KML = a.KML,
                              created = a.createDate,
                              lastUpdate = a.lastUpdate
                          });

            return Ok(result);
        }

        // PUT: api/PointsofInterest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPointsofInterest(string id, PointsofInterest pointsofInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pointsofInterest.PointofInterestID)
            {
                return BadRequest();
            }

            if(pointsofInterest.Name == null || pointsofInterest.Name.Trim() == "")
            {
                return BadRequest("Invalid Name");
            }

            pointsofInterest.lastUpdate = DateTime.Now;
            db.Entry(pointsofInterest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointsofInterestExists(id))
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

        // POST: api/PointsofInterest
        [ResponseType(typeof(PointsofInterest))]
        public IHttpActionResult PostPointsofInterest(PointsofInterest pointsofInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointsofInterest.Name == null || pointsofInterest.Name.Trim() == "")
            {
                return BadRequest("Invalid Name");
            }

            pointsofInterest.PointofInterestID = Guid.NewGuid().ToString();
            pointsofInterest.createDate = DateTime.Now;
            pointsofInterest.createUserId = "";   //update after security is in place
            pointsofInterest.lastUpdate = DateTime.Now;
            db.PointsofInterests.Add(pointsofInterest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PointsofInterestExists(pointsofInterest.PointofInterestID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pointsofInterest.PointofInterestID }, pointsofInterest);
        }

        // DELETE: api/PointsofInterest/5
        [ResponseType(typeof(PointsofInterest))]
        public IHttpActionResult DeletePointsofInterest(string id)
        {
            PointsofInterest pointsofInterest = db.PointsofInterests.Find(id);
            if (pointsofInterest == null)
            {
                return NotFound();
            }

            db.PointsofInterests.Remove(pointsofInterest);
            db.SaveChanges();

            return Ok(pointsofInterest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PointsofInterestExists(string id)
        {
            return db.PointsofInterests.Count(e => e.PointofInterestID == id) > 0;
        }
    }
}
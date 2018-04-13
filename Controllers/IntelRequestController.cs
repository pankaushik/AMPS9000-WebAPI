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
    public class IntelRequestController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/IntelRequest
        public IQueryable<IntelRequest> GetIntelRequests()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.IntelRequests;
        }

        // GET: api/IntelRequest/5
        [ResponseType(typeof(IntelRequest))]
        public IHttpActionResult GetIntelRequest(string id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            IntelRequest intelRequest = db.IntelRequests.Find(id);
            if (intelRequest == null)
            {
                return NotFound();
            }

            return Ok(intelRequest);
        }

        // PUT: api/IntelRequest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIntelRequest(string id, IntelRequest intelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != intelRequest.IntelRequestID)
            {
                return BadRequest();
            }

            db.Entry(intelRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntelRequestExists(id))
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

        // POST: api/IntelRequest
        [ResponseType(typeof(IntelRequest))]
        public IHttpActionResult PostIntelRequest(IntelRequest intelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(intelRequest.IntelRequestID == null || intelRequest.IntelRequestID == "0")
            {
                intelRequest.IntelRequestID = Guid.NewGuid().ToString();
            }

            intelRequest.IntelReqStatus = new List<IntelReqStatu>
            {
                new IntelReqStatu
                {
                    IntelRequestID = intelRequest.IntelRequestID,
                    StatusDateTime = DateTime.Now,
                    Status = 1  //Pending
                }
            };

            db.IntelRequests.Add(intelRequest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IntelRequestExists(intelRequest.IntelRequestID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = intelRequest.IntelRequestID }, intelRequest);
        }

        // DELETE: api/IntelRequest/5
        [ResponseType(typeof(IntelRequest))]
        public IHttpActionResult DeleteIntelRequest(string id)
        {
            IntelRequest intelRequest = db.IntelRequests.Find(id);
            if (intelRequest == null)
            {
                return NotFound();
            }

            //Update status to be canceled
            intelRequest.IntelReqStatus = new List<IntelReqStatu>
            {
                new IntelReqStatu
                {
                    IntelRequestID = intelRequest.IntelRequestID,
                    Status = 5, // Canceled
                    StatusDateTime = DateTime.Now
                }
            };

            db.Entry(intelRequest).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(intelRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IntelRequestExists(string id)
        {
            return db.IntelRequests.Count(e => e.IntelRequestID == id) > 0;
        }
    }
}
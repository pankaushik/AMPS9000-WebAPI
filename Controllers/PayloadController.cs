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
    public class PayloadController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Payload
        public IQueryable<DropDownDTO> GetPayloads()
        {
            var result = (from a in db.Payloads
                          orderby a.PayloadName ascending
                          select new DropDownDTO { id = a.PayloadID.ToString(), description = a.PayloadNomenclature }).AsQueryable();
            return result;
        }

        // GET: api/Payload/{guid}
        [ResponseType(typeof(Payload))]
        public IHttpActionResult GetPayload(string id)
        {
            Payload payload = db.Payloads.Find(id);
            if (payload == null)
            {
                return NotFound();
            }

            return Ok(payload);
        }

        // GET: api/Payload/GetPayloadsData
        public IHttpActionResult GetPayloadsData()
        {
            var result = (from a in db.Payloads
                          join b in db.Companies on a.PayloadManufacturer equals b.id into lojComp
                          from c in lojComp.DefaultIfEmpty()
                          select new
                          {
                              ID = a.PayloadID,
                              payload = a.PayloadName,
                              nomenclature = a.PayloadNomenclature,
                              manufacturer = c.description ?? "Unknown",                         
                              type = a.PayloadType == null ? "Unknown" : db.PayloadTypes.Where(x => x.id == a.PayloadType).Select(x => x.abbreviation).FirstOrDefault(),
                              typeDescription = a.PayloadType == null ? "Unknown" : db.PayloadTypes.Where(x => x.id == a.PayloadType).Select(x => x.description).FirstOrDefault()
                          });

            return Ok(result);
        }

        // PUT: api/Payload/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayload(string id, Payload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payload.PayloadID)
            {
                return BadRequest();
            }

            if(payload.PayloadName == null || payload.PayloadName.Trim() == "")
            {
                return BadRequest("Invalid Payload Name: " + payload.PayloadName);
            }

            if (!IsValidMOS(payload.PayloadMOS1))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS1);
            }

            if (!IsValidMOS(payload.PayloadMOS2))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS2);
            }

            if (!IsValidMOS(payload.PayloadMOS3))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS3);
            }

            db.Entry(payload).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadExists(id))
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

        // POST: api/Payload
        [ResponseType(typeof(Payload))]
        public IHttpActionResult PostPayload(Payload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (payload.PayloadName == null || payload.PayloadName.Trim() == "")
            {
                return BadRequest("Invalid Payload Name: " + payload.PayloadName);
            }

            if (!IsValidMOS(payload.PayloadMOS1))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS1);
            }

            if (!IsValidMOS(payload.PayloadMOS2))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS2);
            }

            if (!IsValidMOS(payload.PayloadMOS3))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS3);
            }

            payload.PayloadID = Guid.NewGuid().ToString();

            db.Payloads.Add(payload);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PayloadExists(payload.PayloadID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = payload.PayloadID }, payload);
        }

        // DELETE: api/Payload/5
        [ResponseType(typeof(Payload))]
        public IHttpActionResult DeletePayload(string id)
        {
            Payload payload = db.Payloads.Find(id);
            if (payload == null)
            {
                return NotFound();
            }

            db.Payloads.Remove(payload);
            db.SaveChanges();

            return Ok(payload);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayloadExists(string id)
        {
            return db.Payloads.Count(e => e.PayloadID == id) > 0;
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
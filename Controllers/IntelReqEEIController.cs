﻿using System;
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
    public class IntelReqEEIController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/IntelReqEEI
        public IQueryable<IntelReqEEI_DTO> GetIntelReqEEIs()
        {
            var result = (from ir in db.IntelReqEEIs
                          join l in db.LIMIDSReqs on ir.LIMIDS_Req equals l.id
                          join t in db.EEIThreats on ir.threatGroupID equals t.id
                          select new IntelReqEEI_DTO
                          {
                              id = ir.id,
                              intelReqID = ir.intelReqID,
                              targetName = ir.targetName,
                              targetNum = ir.targetNum,
                              threatGroupID = t.description,
                              location = ir.location,
                              district = ir.district,
                              gridCoordinates = ir.gridCoordinates,
                              LIMIDS_Req = l.description,
                              POI1_ID = ir.POI1_ID,
                              POI2_ID = ir.POI2_ID,
                              POI3_ID = ir.POI3_ID,
                              EEIs = ir.EEIs,
                              objective = ir.objective
                          });


            return result;
        }

        // GET: api/IntelReqEEI/5
        [ResponseType(typeof(IntelReqEEI))]
        public IHttpActionResult GetIntelReqEEI(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            IntelReqEEI intelReqEEI = db.IntelReqEEIs.Find(id);

            if (intelReqEEI == null)
            {
                return NotFound();
            }

            return Ok(intelReqEEI);
        }

        // GET: api/IntelReqEEI/{GUID}
        [ResponseType(typeof(IntelReqEEI))]
        public IQueryable<IntelReqEEI_DTO> GetIntelReqEEIs(String id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            var result = (from ir in db.IntelReqEEIs
                          join y in db.LIMIDSReqs on ir.LIMIDS_Req equals y.id into tempTable
                          from l in tempTable.DefaultIfEmpty()
                          join z in db.EEIThreats on ir.threatGroupID equals z.id into tempTable2
                          from t in tempTable2.DefaultIfEmpty()
                          where ir.intelReqID == id
                          select new IntelReqEEI_DTO
                          {
                              id = ir.id,
                              intelReqID = ir.intelReqID,
                              targetName = ir.targetName,
                              targetNum = ir.targetNum,
                              threatGroupID = t.description,
                              location = ir.location,
                              district = ir.district,
                              gridCoordinates = ir.gridCoordinates,
                              LIMIDS_Req = l.description,
                              POI1_ID = ir.POI1_ID,
                              POI2_ID = ir.POI2_ID,
                              POI3_ID = ir.POI3_ID,
                              EEIs = ir.EEIs,
                              objective = ir.objective
                          });

            return result;
        }


        // PUT: api/IntelReqEEI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIntelReqEEI(int id, IntelReqEEI intelReqEEI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != intelReqEEI.id)
            {
                return BadRequest();
            }

            intelReqEEI.lastUpdateDate = DateTime.Now;

            db.Entry(intelReqEEI).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntelReqEEIExists(id))
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

        // POST: api/IntelReqEEI
        [ResponseType(typeof(IntelReqEEI))]
        public IHttpActionResult PostIntelReqEEI(IntelReqEEI intelReqEEI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(intelReqEEI.intelReqID == null || intelReqEEI.intelReqID == "0") { intelReqEEI.intelReqID = Guid.NewGuid().ToString(); }

            intelReqEEI.createDate = DateTime.Now;

            db.IntelReqEEIs.Add(intelReqEEI);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = intelReqEEI.id }, intelReqEEI);
        }

        // DELETE: api/IntelReqEEI/5
        [ResponseType(typeof(IntelReqEEI))]
        public IHttpActionResult DeleteIntelReqEEI(int id)
        {
            IntelReqEEI intelReqEEI = db.IntelReqEEIs.Find(id);
            if (intelReqEEI == null)
            {
                return NotFound();
            }

            db.IntelReqEEIs.Remove(intelReqEEI);
            db.SaveChanges();

            return Ok(intelReqEEI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IntelReqEEIExists(int id)
        {
            return db.IntelReqEEIs.Count(e => e.id == id) > 0;
        }
    }
}
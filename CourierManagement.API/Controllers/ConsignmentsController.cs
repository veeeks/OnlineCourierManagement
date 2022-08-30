using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CourierManagement.API.Models;

namespace CourierManagement.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Consignments")]
    public class ConsignmentsController : ApiController
    {
        private CourierManagementEntities db = new CourierManagementEntities();

        // GET: api/Consignments
        //public IQueryable<Consignment> GetConsignments()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    return db.Consignments;
        //}

        [HttpGet]
        [Route("TrackBy/{cNo}")]
        public IHttpActionResult TrackConsignmentStatus(string cNo)
        {
            try
            {
                var consignment_Status = db.usp_TrackCourier(int.Parse(cNo)).FirstOrDefault();
                if (consignment_Status == null)
                {

                    return NotFound();
                }


                return Ok(consignment_Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ResponseType(typeof(Consigner))]
        [ActionName("InsertConsigner")]
        public async Task<IHttpActionResult> PostConsigner(Consigner consigner)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Consigners.Add(consigner);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = consigner.Consigner_id }, consigner);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("ConsignerId")]
        public int GetLatestConsignerId()
        {
            try { 
            return db.Consigners.OrderByDescending(x => x.Consigner_id).Select(x => x.Consigner_id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [ResponseType(typeof(Consignee))]
        [ActionName("InsertConsignee")]
        public async Task<IHttpActionResult> PostConsignee(Consignee consignee)
        {
            try 
            { 
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Consignees.Add(consignee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = consignee.Consignee_id }, consignee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("ConsigneeId")]
        public int GetLatestConsigneeId()
        {
            try { 
            return db.Consignees.OrderByDescending(x => x.Consignee_id).Select(x => x.Consignee_id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("ConID")]
        public int GetLatestConsignmentId()
        {
            try { 
            return db.Consignments.OrderByDescending(x => x.Consignment_Id).Select(x => x.Consignment_Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ResponseType(typeof(Consignment))]
        [ActionName("InsertConsignment")]
        public async Task<IHttpActionResult> PostConsignment(Consignment consignment)
        {
            try { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Consignments.Add(consignment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = consignment.Consignment_Id }, consignment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ResponseType(typeof(Consignment_Status))]
        [ActionName("InsertConsignmentStatus")]
        public async Task<IHttpActionResult> PostConsignment_Status(Consignment_Status consignment_Status)
        {
            try { 
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Consignment_Status.Add(consignment_Status);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = consignment_Status.Status_id }, consignment_Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetBill/{conId}")]
        public IHttpActionResult GetBill(int conId)
        {
            var bill = db.usp_GenerateBillDetails(conId).FirstOrDefault();
            if (bill == null)
            {

                return NotFound();
            }

            return Ok(bill);
        }

    }
}
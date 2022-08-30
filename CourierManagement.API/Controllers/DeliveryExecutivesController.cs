using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
    [RoutePrefix("api/DeliveryExecutives")]
    public class DeliveryExecutivesController : ApiController
    {
        private CourierManagementEntities db = new CourierManagementEntities();



        [HttpGet]
        [Route("GetExecutiveEmail/{email}/")]
        [AllowAnonymous]
        public string GetEmail(string email)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.Delivery_Executive.Where(x => x.DE_Email == email).Select(x => x.DE_Email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetByEmail/{search}/")]
        //[Route("GetByEmail")]
        public IHttpActionResult GetConsignmentStatusByEmailId(string search)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                Delivery_Executive de = new Delivery_Executive();
                // var result = db.Database.ExecuteSqlCommand("usp_ViewConsignmentByDeliveryExecutiveEmail @Email", new SqlParameter("@Email", search));
                var result = db.usp_ViewConsignmentByDeliveryExecutiveEmail(search).OrderByDescending(n => n.Consignment_Id).ToList();
                //var result = db.Delivery_Executive;
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetConsignmentById/{CId}")]
        public IHttpActionResult GetConsignmentStatusByCId(int CId)
        {
            try
            {
                var result = db.usp_ViewConsignmentDetailsByConsignmentId(CId).FirstOrDefault();
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetConsignmentByIdToEdit/{CId}")]
        public IHttpActionResult GetConsignmentStatusByCIdToEdit(int CId)
        {
            var result = db.usp_ViewUpdateConsignmentDetails(CId).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        // PUT: api/ConsignmentStatus/5
        [HttpPut]
        [ResponseType(typeof(void))]
        [ActionName("UpdateConsignmentStatus")]
        public IHttpActionResult UpdateConsignment_Status([FromUri] int id, [FromBody] Consignment_Status consignment_Status)
        {
            var updateStatus = db.Consignment_Status.Where(x => x.Consignment_Id == id).FirstOrDefault<Consignment_Status>();
            if (updateStatus != null)
            {
                updateStatus.Status = consignment_Status.Status;
                updateStatus.Remarks = consignment_Status.Remarks;
                updateStatus.ExpectedDelivery = consignment_Status.ExpectedDelivery;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("GetDeliveryExecutiveDetails/{search}")]
        public IHttpActionResult GetDelivery_Executive(string search)
        {
            var result = db.usp_GetDelieveryExecutiveDetailsByEmail(search).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("GetDeliveryExecutiveDetailsById/{id}")]
        public IHttpActionResult GetDeliveryExecutiveDetailsById(int id)
        {
            var result = db.usp_GetDelieveryExecutiveDetailsById(id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // PUT: api/DeliveryExecutive/5
        [HttpPut]
        [ResponseType(typeof(void))]
        [ActionName("UpdateProfile")]
        public IHttpActionResult UpdateDelivery_Executive(int id, Delivery_Executive delivery_Executive)
        {
            var updateData = db.Delivery_Executive.Where(x => x.Delivery_Id == id).FirstOrDefault<Delivery_Executive>();
            if (updateData != null)
            {
                updateData.Name = delivery_Executive.Name;
                updateData.City = delivery_Executive.City;
                updateData.Contact_number = delivery_Executive.Contact_number;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Consignment_StatusExists(int id)
        {
            return db.Consignment_Status.Count(e => e.Status_id == id) > 0;
        }
    }
}
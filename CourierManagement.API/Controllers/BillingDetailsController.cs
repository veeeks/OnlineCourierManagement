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
using CourierManagement.API.Dtos;
using CourierManagement.API.Models;

namespace CourierManagement.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/BillingDetails")]
    public class BillingDetailsController : ApiController
    {
        private CourierManagementEntities db = new CourierManagementEntities();

        [HttpGet]
        public IEnumerable<BillingDetailsDto> GetBillDetails()
        {
            try
            {

                return db.Billing_Details.Select(x => new BillingDetailsDto { ConsignmentType = x.Consignement_Type }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("Price/{consignmentType}")]
        public double GetTypePrice(string consignmentType)
        {
            try
            {
                return db.Billing_Details.Where(x => x.Consignement_Type == consignmentType).Select(x => x.Type_Price).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
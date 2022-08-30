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
using CourierManagement.API.DTO;
using CourierManagement.API.Models;



namespace CourierManagement.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Administrators")]
    public class AdministratorsController : ApiController
    {
        private CourierManagementEntities db = new CourierManagementEntities();


        //-------------------Delivery-----------------------
        [HttpGet]
        [Route("AvailableDEs")]
        public IEnumerable<AvailableDEs> GetAvailableDEs()
        {
            try
            {
                var result = db.Delivery_Executive.Where(De => De.DStatus == "Available" && De.MaxQty <= 9).Select(De => new AvailableDEs { Delivery_Id = De.Delivery_Id, Name = De.Name }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("PendingConsginments")]
        public IEnumerable<PendingConsignments> GetPendingConsignments()
        {
            try
            {
                var result = db.Consignment_Status.Where(De => De.Status == "Pending").Select(De => new PendingConsignments { Consignment_Id = (int)De.Consignment_Id }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("OriginCities")]
        public IEnumerable<OriginiCities> GetOriginiCities()
        {
            try
            {
                var res = db.City_Price_Details.GroupBy(c => c.Source_City).Select(c => new OriginiCities { Source_City = c.Key }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllCities")]
        public IQueryable<City_Price_Details> GetOriginCities()
        {

            return db.City_Price_Details;
        }
        [HttpPost]
        [ActionName("AddCities")]
        // POST: api/City_Price_Details
        [ResponseType(typeof(City_Price_Details))]
        public async Task<IHttpActionResult> PostCity_Price_Details(City_Price_Details city_Price_Details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.City_Price_Details.Add(city_Price_Details);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = city_Price_Details.City_Id }, city_Price_Details);
        }
        [HttpPut]
        [Route("UpdatePrice/{sCity}/{dCity}/{price}")]
        public IHttpActionResult UpdateCityPrice(string sCity, string dCity, float price)
        {

            var city = (from c in db.City_Price_Details
                        where c.Destination_City == dCity && c.Source_City == sCity
                        select c).FirstOrDefault();

            city.Cost = price;

            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);

        }
        [HttpDelete]
        // DELETE: api/City_Price_Details/5
        [ActionName("DeleteCity")]
        [ResponseType(typeof(City_Price_Details))]
        public async Task<IHttpActionResult> DeleteCity_Price_Details(int id)
        {
            City_Price_Details city_Price_Details = await db.City_Price_Details.FindAsync(id);
            if (city_Price_Details == null)
            {
                return NotFound();
            }

            db.City_Price_Details.Remove(city_Price_Details);
            await db.SaveChangesAsync();

            return Ok(city_Price_Details);
        }
        [HttpGet]
        [Route("DestinationCities")]
        public IEnumerable<DestinationCities> GetDestinationCities()
        {
            var res = db.City_Price_Details.GroupBy(c => c.Destination_City).Select(c => new DestinationCities { Destination_City = c.Key }).ToList();
            return res;
        }
        private bool City_Price_DetailsExists(int id)
        {
            return db.City_Price_Details.Count(e => e.City_Id == id) > 0;
        }




        //---------------------------------Assigning/Consignment_Status------------------------------------------------------
        [HttpPut]
        [Route("AssignDEs/{consginment_id}/{delivery_id}")]

        public IHttpActionResult Assign(int consginment_id, int delivery_id)
        {
            var result = db.Database.ExecuteSqlCommand("usp_AssignConsignmentToDeliveryExecutive @ConsignmentId, @DeliveryId", new SqlParameter("@ConsignmentId", consginment_id), new SqlParameter("@DeliveryId", delivery_id));
            if (result > 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return BadRequest();

        }
        [HttpGet]
        [Route("GetAllConsignments")]
        public IHttpActionResult GetConsignment_Status()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var result = db.usp_GetAllConsignments();

            return Ok(result.ToList());

        }
        //--------------------------------------------------------------------------
        //[HttpGet]
        [Route("GetDEs")]
        public IQueryable<Delivery_Executive> GetDelivery_Executive()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Delivery_Executive;
        }
        //[HttpGet]
        [Route("GetDes/{id}")]
        // GET: api/Administrators/5
        [ResponseType(typeof(Delivery_Executive))]
        public async Task<IHttpActionResult> GetDelivery_Executive(int id)
        {
            Delivery_Executive delivery_Executive = await db.Delivery_Executive.Where(c => c.Delivery_Id == id).FirstOrDefaultAsync();
            if (delivery_Executive == null)
            {
                return NotFound();
            }

            return Ok(delivery_Executive);
        }

        [HttpGet]
        [Route("SearchByCity/{city}")]
        public IEnumerable<Delivery_Executive> SearchByCity(string city)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Delivery_Executive.Where(x => x.City == city).ToList();

        }

        [HttpPost]
        [ActionName("AddNewDes")]
        [ResponseType(typeof(Delivery_Executive))]
        public async Task<IHttpActionResult> PostDelivery_Executive(Delivery_Executive delivery_Executive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //string num = delivery_Executive.Contact_number;
            //if (db.Delivery_Executive.Find(delivery_Executive.Contact_number).Equals(num))
            //{
            //    return BadRequest("Cannot Add");
            //}
            db.Delivery_Executive.Add(delivery_Executive);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = delivery_Executive.Delivery_Id }, delivery_Executive);
        }
        [HttpDelete]
        [ActionName("DeleteDe")]
        [ResponseType(typeof(Delivery_Executive))]
        public async Task<IHttpActionResult> DeleteDelivery_Executive(int id)
        {
            Delivery_Executive delivery_Executive = await db.Delivery_Executive.FindAsync(id);
            if (delivery_Executive == null)
            {
                return NotFound();
            }
            else if (delivery_Executive.DStatus == "Busy")
            {
                return BadRequest();
            }

            db.Delivery_Executive.Remove(delivery_Executive);
            await db.SaveChangesAsync();

            return Ok(delivery_Executive);
        }
    }
}
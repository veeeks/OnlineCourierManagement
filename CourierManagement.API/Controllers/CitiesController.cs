using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CourierManagement.API.Dtos;
using CourierManagement.API.Models;
namespace CourierManagement.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Cities")]
    public class CitiesController : ApiController
    {
        private CourierManagementEntities db = new CourierManagementEntities();

        [HttpGet]
        [Route("GetCities")]
        public IEnumerable<CityDto> GetCities()
        {
            try
            {
                return db.City_Price_Details.GroupBy(x => x.Source_City).Select(x => new CityDto { City = x.Key }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

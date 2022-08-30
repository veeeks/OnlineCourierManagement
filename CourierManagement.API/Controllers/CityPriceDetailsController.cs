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
    [RoutePrefix("api/CityPriceDetails")]
    public class CityPriceDetailsController : ApiController
    {
        private CourierManagementEntities db = new CourierManagementEntities();

        [HttpGet]
        [Route("ConsignerCity")]
        public IEnumerable<ConsignerCitiesDto> GetConsignerCities()
        {
            //return (IEnumerable<CitiesDto>)db.City_Price_Details.Select(x => new CitiesDto { ConsignerCity=x.Source_City,ConsigneeCity=x.Destination_City }).GroupBy(x=>x.ConsignerCity).ToList();
            return db.City_Price_Details.GroupBy(x => x.Source_City).Select(x => new ConsignerCitiesDto { ConsignerCity=x.Key }).ToList();
        }

        [HttpGet]
        [Route("ConsigneeCity")]
        public IEnumerable<ConsigneeCitiesDto> GetConsigneeCities()
        {
            //return (IEnumerable<CitiesDto>)db.City_Price_Details.Select(x => new CitiesDto { ConsignerCity=x.Source_City,ConsigneeCity=x.Destination_City }).GroupBy(x=>x.ConsignerCity).ToList();
            return db.City_Price_Details.GroupBy(x => x.Destination_City).Select(x => new ConsigneeCitiesDto { ConsigneeCity = x.Key }).ToList();
        }

        [HttpGet]
        [Route("GetPrice/{consignerCity}/{consigneeCity}")]
        public double GetCityPrice(string consignerCity,string consigneeCity)
        {
            return db.City_Price_Details.Where(x => x.Source_City == consignerCity && x.Destination_City == consigneeCity).Select(x => x.Cost).FirstOrDefault();
        }
    }
}
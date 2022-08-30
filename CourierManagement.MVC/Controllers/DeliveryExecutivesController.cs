using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourierManagement.MVC.ViewModels;

namespace CourierManagement.MVC.Controllers
{
    [Authorize]
    public class DeliveryExecutivesController : Controller
    {

        public DeliveryExecutivesController()
        {

        }

        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                string search = null;
                if (Session["CurrentEmail"] != null)
                {
                    search = Session["CurrentEmail"].ToString();
                }

                // GlobalVariables.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalVariables.BearerToken);
                if (search == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                IEnumerable<ConsignmentStatusDisplay> consignmentDetails = null;
                var result = await GlobalVariables.client.GetAsync($"DeliveryExecutives/GetByEmail/{search}/");

                if (result.IsSuccessStatusCode)
                {
                    consignmentDetails = await result.Content.ReadAsAsync<IEnumerable<ConsignmentStatusDisplay>>();
                }
                else
                {
                    consignmentDetails = Enumerable.Empty<ConsignmentStatusDisplay>();
                    ModelState.AddModelError(string.Empty, "server error");
                }
                return View(consignmentDetails);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();

        }

        [HttpGet]
        public async Task<ActionResult> ConsignmentDetails(int? id)
        {
            ConsignmentStatusDetailsViewModel consignmentDetails = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var result = await GlobalVariables.client.GetAsync($"DeliveryExecutives/GetConsignmentById/{id.Value}");
            if (result.IsSuccessStatusCode)
            {
                consignmentDetails = await result.Content.ReadAsAsync<ConsignmentStatusDetailsViewModel>();
            }
            else
            {
                consignmentDetails = null;
                ModelState.AddModelError(string.Empty, "server error");
            }
            return View(consignmentDetails);
        }

        [HttpGet]
        public async Task<ActionResult> ConsignmentStatusEdit(int? id)
        {
            UpdateConsignmentDetailsViewModel consignments = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await GlobalVariables.client.GetAsync($"DeliveryExecutives/GetConsignmentByIdToEdit/{id.Value}");
            if (result.IsSuccessStatusCode)
            {
                consignments = await result.Content.ReadAsAsync<UpdateConsignmentDetailsViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(consignments);
        }

        [HttpPost]
        public async Task<ActionResult> ConsignmentStatusEdit(int? id, UpdateConsignmentDetailsViewModel consignmentDetails)
        {
            if (consignmentDetails == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await GlobalVariables.client.PutAsJsonAsync<UpdateConsignmentDetailsViewModel>($"DeliveryExecutives/UpdateConsignmentStatus/" + id, consignmentDetails);
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(consignmentDetails);
        }

        [HttpGet]
        public async Task<ActionResult> ProfileDetails()
        {
            string search = null;
            if (Session["CurrentEmail"] != null)
            {
                search = Session["CurrentEmail"].ToString();
            }

            DeliveryExecutivesViewModel deliveryExecutives = null;
            var result = await GlobalVariables.client.GetAsync($"DeliveryExecutives/GetDeliveryExecutiveDetails/{search}/");

            if (result.IsSuccessStatusCode)
            {
                deliveryExecutives = await result.Content.ReadAsAsync<DeliveryExecutivesViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error");
            }
            return View(deliveryExecutives);
        }

        // GET: DeliveryExecutives
        [HttpGet]
        public async Task<ActionResult> EditProfile(int? id)
        {
            //string search = "shilpa@gmail.com"
            DeliveryExecutivesViewModel deliveryExecutives = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await GlobalVariables.client.GetAsync($"DeliveryExecutives/GetDeliveryExecutiveDetailsById/{id.Value}");
            if (result.IsSuccessStatusCode)
            {
                deliveryExecutives = await result.Content.ReadAsAsync<DeliveryExecutivesViewModel>();
                IEnumerable<CitiesViewModel> cities = null;
                var data = await GlobalVariables.client.GetAsync("Cities/GetCities");
                if (data.IsSuccessStatusCode)
                {
                    cities = await data.Content.ReadAsAsync<IEnumerable<CitiesViewModel>>();
                }
                ViewBag.City = cities;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(deliveryExecutives);
        }

        [HttpPost]
        public async Task<ActionResult> EditProfile(int? id, DeliveryExecutivesViewModel deliveryExecutives)
        {
            if (deliveryExecutives == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await GlobalVariables.client.PutAsJsonAsync<DeliveryExecutivesViewModel>($"DeliveryExecutives/UpdateProfile/" + id, deliveryExecutives);
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (GlobalVariables.client != null)
                //{
                //    GlobalVariables.client.Dispose();
                //}
            }
            base.Dispose(disposing);
        }
    }
}
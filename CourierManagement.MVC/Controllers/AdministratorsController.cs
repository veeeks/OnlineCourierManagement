using CourierManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;



namespace CourierManagement.MVC.Controllers
{
    [Authorize]
    public class AdministratorsController : Controller
    {
        //private HttpClient client = null;

        public AdministratorsController()
        {
            //client = new HttpClient()
            //{
            //    BaseAddress = new Uri(ConfigurationManager.AppSettings["api"])
            //};
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        


        public ActionResult Home()
        {
            return View();
        }
        // GET: ConsignmetStatus
        public async Task<ActionResult> Index()
        {
            IEnumerable<AConsignmentStatusViewModel> details = null;
            try
            {
                
                var result = await GlobalVariables.client.GetAsync("Administrators/GetAllConsignments");
                if (result.IsSuccessStatusCode)
                {
                    details = await result.Content.ReadAsAsync<IEnumerable<AConsignmentStatusViewModel>>();
                }
                else
                {
                    details = Enumerable.Empty<AConsignmentStatusViewModel>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(details.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> ViewAllCities()
        {

            IEnumerable<AllCitiesViewModel> details = null;
            var result = await GlobalVariables.client.GetAsync("Administrators/GetAllCities");
            if (result.IsSuccessStatusCode)
            {
                details = await result.Content.ReadAsAsync<IEnumerable<AllCitiesViewModel>>();
            }
            else
            {
                details = Enumerable.Empty<AllCitiesViewModel>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(details.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            IEnumerable< AvailableDesViewModel> availableDes = null;
            var data = await GlobalVariables.client.GetAsync("Administrators/AvailableDEs");
            if (data.IsSuccessStatusCode)
            {
                availableDes = await data.Content.ReadAsAsync<IEnumerable<AvailableDesViewModel>>();
            }

            IEnumerable<PendingConsignmentsViewModel> pendingConsignments = null;
            var data1 = await GlobalVariables.client.GetAsync("Administrators/PendingConsginments");
            if (data1.IsSuccessStatusCode)
            {
                pendingConsignments = await data1.Content.ReadAsAsync<IEnumerable<PendingConsignmentsViewModel>>();
            }
            ViewBag.PendingConsignmnets = pendingConsignments;
            ViewBag.AvailableDes = availableDes;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(AssignDEsViewModel assignDEs)
        {
            IEnumerable<AvailableDesViewModel> availableDes = null;
            var data = await GlobalVariables.client.GetAsync("Administrators/AvailableDEs");
            if (data.IsSuccessStatusCode)
            {
                availableDes = await data.Content.ReadAsAsync<IEnumerable<AvailableDesViewModel>>();
            }

            IEnumerable<PendingConsignmentsViewModel> pendingConsignments = null;
            var data1 = await GlobalVariables.client.GetAsync("Administrators/PendingConsginments");
            if (data1.IsSuccessStatusCode)
            {
                pendingConsignments = await data1.Content.ReadAsAsync<IEnumerable<PendingConsignmentsViewModel>>();
            }
            ViewBag.PendingConsignmnets = pendingConsignments;
            ViewBag.AvailableDes = availableDes;
            if (assignDEs == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var deliveryid = await GlobalVariables.client.PutAsJsonAsync<AssignDEsViewModel>($"Administrators/AssignDEs/{assignDEs.Consignment_Id}/{assignDEs.Delivery_Id}", assignDEs);
                if (deliveryid.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return View(assignDEs);
        }
        [HttpGet]
        
        public ActionResult AddCities()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddCities(AllCitiesViewModel cities)
        {
            if (cities == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var result = await GlobalVariables.client.PostAsJsonAsync("Administrators/AddCities", cities);
                if (result.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction("ViewAllCities");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View(cities);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateCities()
        {
            IEnumerable<OriginCityViewModel> cities1 = null;
            var data = await GlobalVariables.client.GetAsync("Administrators/OriginCities");
            if (data.IsSuccessStatusCode)
            {
                cities1 = await data.Content.ReadAsAsync<IEnumerable<OriginCityViewModel>>();
            }
            ViewBag.OriginCities = cities1;
            IEnumerable<DestinationCityViewModel> cities2 = null;
            var data1 = await GlobalVariables.client.GetAsync("Administrators/DestinationCities");
            if (data1.IsSuccessStatusCode)
            {
                cities2 = await data1.Content.ReadAsAsync<IEnumerable<DestinationCityViewModel>>();
            }
            ViewBag.DestinationCities = cities2;

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCities(AllCitiesViewModel cities)
        {

            IEnumerable<OriginCityViewModel> cities1 = null;
            var data = await GlobalVariables.client.GetAsync("Administrators/OriginCities");
            if (data.IsSuccessStatusCode)
            {
                cities1 = await data.Content.ReadAsAsync<IEnumerable<OriginCityViewModel>>();
            }
            ViewBag.City_Price_Details = cities1;
            IEnumerable<DestinationCityViewModel> cities2 = null;
            var data1 = await GlobalVariables.client.GetAsync("Administrators/DestinationCities");
            if (data1.IsSuccessStatusCode)
            {
                cities2 = await data1.Content.ReadAsAsync<IEnumerable<DestinationCityViewModel>>();
            }

            ViewBag.City_Price_Details1 = cities2;

            if (cities1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var result = await GlobalVariables.client.PutAsJsonAsync($"Administrators/UpdatePrice/{cities.Source_City}/{cities.Destination_City}/{cities.Cost}", cities);
                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction("ViewAllCities");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View(cities);
        }
        public ActionResult Delete(int id)
        {
            var DeletedCity = GlobalVariables.client.DeleteAsync($"Administrators/DeleteCity/{id.ToString()}");
            DeletedCity.Wait();
            var result = DeletedCity.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewAllCities");
            }
            return RedirectToAction("ViewAllCities");
        }
        //----------------------------------------------------------------

        public async Task<ActionResult> ViewAllDes()
        {
            IEnumerable<ViewDEViewModel> del = null;
            Session["DE"] = "DE";
            ////GlobalVariables.client.BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]);
            var get = await GlobalVariables.client.GetAsync("Administrators/getdes");
            if (get.IsSuccessStatusCode)
            {

                var content = get.Content.ReadAsAsync<IEnumerable<ViewDEViewModel>>();
                del = content.Result;
            }
            else
            {
                del = Enumerable.Empty<ViewDEViewModel>();
                ViewBag.Errors = "Server Error";
            }
            return View(del);
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllDes(string city)
        {
            IEnumerable<ViewDEViewModel> de = null;
            if (city == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await GlobalVariables.client.GetAsync($"Administrators/SearchByCity/{city}");

            if (result.IsSuccessStatusCode)
            {
                de = await result.Content.ReadAsAsync<IEnumerable<ViewDEViewModel>>();
            }
            else
            {
                de = null;
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(de);
        }

        [HttpGet]
        public async Task<ActionResult> AddNewDe()
        {
           
            IEnumerable<OriginCityViewModel> city = null;
            var cities = await GlobalVariables.client.GetAsync("Administrators/OriginCities");
            //originCityViewModel = await client.GetAsync("Administrators/OriginCities");
            
            if (cities.IsSuccessStatusCode)
            {
                
                city = await cities.Content.ReadAsAsync<IEnumerable<OriginCityViewModel>>();
                
            }
            
            ViewBag.OriginCityViewModel = city;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewDe(AddDEViewModel del)
        {
            IEnumerable<OriginCityViewModel> city = null;
            if (del == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cities = await GlobalVariables.client.GetAsync("OriginCities");
            if (cities.IsSuccessStatusCode)
            {
                city = await cities.Content.ReadAsAsync<IEnumerable<OriginCityViewModel>>();
            }
            ViewBag.OriginCityViewModel = city;
            del.DE_Email = Session["DEmail"].ToString();
            del.DE_Password = Session["DEPassword"].ToString();
            if (ModelState.IsValid)
            {
                var data = await GlobalVariables.client.PostAsJsonAsync<AddDEViewModel>("Administrators/AddNewDes", del);
                if (data.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction("ViewAllDes");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                } 
            }
            return View(del);
        }

        public ActionResult Details(int? id)
        {
            ViewDEViewModel customer = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]);
                var get = GlobalVariables.client.GetAsync($"Administrators/getdes/{+id.Value}");
                get.Wait();
                var result = get.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<ViewDEViewModel>();
                    data.Wait();
                    customer = data.Result;
                }
                else
                {
                    customer = null;
                    ViewBag.Errors = "Server Error";
                }
                return View(customer);
            }
        }

        public ActionResult DeleteDe(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]);
                var deleteTask = client.DeleteAsync("Administrators/Deletede/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("ViewAlldes");
                }
                else
                {
                    ViewBag.CantDelete = "No";
                    return View();
                }
            }

           // return RedirectToAction("ViewAlldes");
        
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
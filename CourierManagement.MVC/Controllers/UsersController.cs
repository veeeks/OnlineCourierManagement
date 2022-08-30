using CourierManagement.MVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace CourierManagement.MVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        //private HttpClient client = null;
        
        public UsersController()
        {
            //client = new HttpClient()
            //{
            //    BaseAddress = new Uri(ConfigurationManager.AppSettings["api"])
            //};
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

        

        public ActionResult Home()
        {
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalVariables.BearerToken);
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> TrackingDetails(string consignmentNo)
        {
            try
            {
                TrackCourierViewModel trackConsignment = null;
                if (consignmentNo == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = await GlobalVariables.client.GetAsync($"Consignments/TrackBy/{consignmentNo}");


                if (result.IsSuccessStatusCode)
                {
                    trackConsignment = await result.Content.ReadAsAsync<TrackCourierViewModel>();
                    Session["conId"] = consignmentNo;
                    return View(trackConsignment);

                }
                else
                {
                    trackConsignment = null;
                    ModelState.AddModelError(string.Empty, "Server Error");
                    ViewBag.valid = "No";
                    return View();
                }

            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();




        }


        public async Task< ActionResult> BillDetails(string fromTrack)
        {
            if(fromTrack==null)
            {
                ViewBag.booking = "Yes";
            }
           
            
            BillViewModel bill = null;
            
            int conId = int.Parse(Session["conId"].ToString());
            
            var result = await GlobalVariables.client.GetAsync($"Consignments/GetBill/{conId}");
            if (result.IsSuccessStatusCode)
            {
                bill = await result.Content.ReadAsAsync<BillViewModel>();
                return View(bill);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Book()
        {
            IEnumerable<ConsignerCitiesViewModel> ConsignerCity = null;
            IEnumerable<ConsigneeCitiesViewModel> ConsigneeCity = null;
            IEnumerable<BillingDetailsViewModel> billingDetails = null;

            var data1 = await GlobalVariables.client.GetAsync("CityPriceDetails/ConsignerCity");
            var data2 = await GlobalVariables.client.GetAsync("CityPriceDetails/ConsigneeCity");
            var data3 = await GlobalVariables.client.GetAsync("BillingDetails");
            if (data1.IsSuccessStatusCode && data2.IsSuccessStatusCode && data3.IsSuccessStatusCode)
            {
                ConsignerCity = await data1.Content.ReadAsAsync<IEnumerable<ConsignerCitiesViewModel>>();
                ConsigneeCity = await data2.Content.ReadAsAsync<IEnumerable<ConsigneeCitiesViewModel>>();
                billingDetails = await data3.Content.ReadAsAsync<IEnumerable<BillingDetailsViewModel>>();
            }
            ViewBag.ConsignerCities = ConsignerCity;
            ViewBag.ConsigneeCities = ConsigneeCity;
            ViewBag.BillingDetails = billingDetails;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Book(string consignerCity,string consigneeCity,string consignmentType,string weight,string getPrice,string next)
        {
            IEnumerable<ConsignerCitiesViewModel> ConsignerCity = null;
            IEnumerable<ConsigneeCitiesViewModel> ConsigneeCity = null;
            IEnumerable<BillingDetailsViewModel> billingDetails = null;

            var data1 = await GlobalVariables.client.GetAsync("CityPriceDetails/ConsignerCity");
            var data2 = await GlobalVariables.client.GetAsync("CityPriceDetails/ConsigneeCity");
            var data3 = await GlobalVariables.client.GetAsync("BillingDetails");
            if (data1.IsSuccessStatusCode && data2.IsSuccessStatusCode && data3.IsSuccessStatusCode)
            {
                ConsignerCity = await data1.Content.ReadAsAsync<IEnumerable<ConsignerCitiesViewModel>>();
                ConsigneeCity = await data2.Content.ReadAsAsync<IEnumerable<ConsigneeCitiesViewModel>>();
                billingDetails = await data3.Content.ReadAsAsync<IEnumerable<BillingDetailsViewModel>>();
            }
            ViewBag.ConsignerCities = ConsignerCity;
            ViewBag.ConsigneeCities = ConsigneeCity;
            ViewBag.BillingDetails = billingDetails;


            double CityPrice = 0;
            double typePrice = 0;

            var data4 = await GlobalVariables.client.GetAsync($"CityPriceDetails/GetPrice/{consignerCity}/{consigneeCity}");
            var data5 = await GlobalVariables.client.GetAsync($"BillingDetails/Price/{consignmentType}");

            if (data4.IsSuccessStatusCode && data5.IsSuccessStatusCode)
            {
                typePrice = await data5.Content.ReadAsAsync<double>();
                CityPrice = await data4.Content.ReadAsAsync<double>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Service to the selected location does not exist ");
            }


            double totalPrice = CityPrice + typePrice * double.Parse(weight);



            if (!string.IsNullOrEmpty(getPrice))
            {
                
                ViewBag.totalPrice = totalPrice;
            }
            
            if(!string.IsNullOrEmpty(next))
            {
                ViewBag.ConsignerCity = consignerCity;
                ViewBag.ConsigneeCity = consigneeCity;
                ViewBag.consignmentType = consignmentType;
                ViewBag.totalPrice = totalPrice;
                ViewBag.Weight = weight;
                

                Session["consignerCity"] = consignerCity;
                Session["consigneeCity"] = consigneeCity;
                Session["consignmentType"] = consignmentType;
                Session["weight"] = weight;
                Session["price"] = totalPrice;

                return View("BookConsignment");
            }

            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BookConsignment(BookConsignmentViewModel booking)
        {
            string crcity = Session["consignerCity"].ToString();
            string cecity = Session["consigneeCity"].ToString();
            string cnType = Session["consignmentType"].ToString();
            double weight = double.Parse(Session["weight"].ToString());
            double price = double.Parse(Session["price"].ToString());

            if (booking==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                ConsignerViewModel consigner = new ConsignerViewModel
                {
                    Consigner_Name = booking.Consigner_Name,
                    City = crcity,
                    Address = booking.SAddress,
                    Mobile_No = booking.SMobile_No
                };
                ConsigneeViewModel consignee = new ConsigneeViewModel
                {
                    Consignee_Name = booking.Consignee_Name,
                    City = cecity,
                    Address = booking.RAddress,
                    Mobile_No = booking.RMobile_No
                };

                var info1 = await GlobalVariables.client.PostAsJsonAsync("Consignments/InsertConsigner", consigner);
                var info2= await GlobalVariables.client.PostAsJsonAsync("Consignments/InsertConsignee", consignee);

                if((info1.StatusCode==HttpStatusCode.Created) && (info2.StatusCode==HttpStatusCode.Created) )
                {
                    int consignerId = 0, consigneeId = 0;
                    var crId = await GlobalVariables.client.GetAsync("Consignments/ConsignerId");
                    var ceId = await GlobalVariables.client.GetAsync("Consignments/ConsigneeId");
                    if(crId.IsSuccessStatusCode && ceId.IsSuccessStatusCode)
                    {
                        consignerId = await crId.Content.ReadAsAsync<int>();
                        consigneeId = await ceId.Content.ReadAsAsync<int>();

                        ConsignmentViewModel consignment = new ConsignmentViewModel
                        {
                            Consignment_Type = cnType,
                            Weight = weight,
                            Description = booking.Description,
                            Billing_Amount = price,
                            Consigner_Id = consignerId,
                            Consignee_Id = consigneeId
                        };

                        var info3 = await GlobalVariables.client.PostAsJsonAsync("Consignments/InsertConsignment", consignment);
                        if(info3.IsSuccessStatusCode)
                        {
                            int consignmentId = 0;
                            var latestConId = await GlobalVariables.client.GetAsync("Consignments/ConId");
                            if(latestConId.IsSuccessStatusCode)
                            {
                                consignmentId = await latestConId.Content.ReadAsAsync<int>();

                                ConsignmentStatusViewModel constatus = new ConsignmentStatusViewModel
                                {
                                    Consignment_Id = consignmentId
                                };

                                var info4 = await GlobalVariables.client.PostAsJsonAsync("Consignments/InsertConsignmentStatus", constatus);
                                if(info4.IsSuccessStatusCode)
                                {
                                    Session["conId"] = consignmentId;
                                    return RedirectToAction("BillDetails");
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, "Server Error");
                                }

                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Server Error");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server Error");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

            return View();
        }
    }
}
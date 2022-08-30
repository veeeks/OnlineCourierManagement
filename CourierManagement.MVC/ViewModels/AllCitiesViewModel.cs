using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourierManagement.MVC.ViewModels
{
    public class AllCitiesViewModel
    {
        [Display(Name ="City Id")]
        public int City_Id { get; set; }
        [Display(Name = "Source City")]
        public string Source_City { get; set; }
        [Display(Name = " Destination City")]
        public string Destination_City { get; set; }
        public double Cost { get; set; }
    }
}
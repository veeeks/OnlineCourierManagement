using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class CitiesPricesViewModel
    {
        [Required]
        public string ConsignerCity { get; set; }
        [Required]
        public string ConsigneeCity { get; set; }
        [Required]
        public string ConsignmentType { get; set; }

        public double Amount { get; set; }

        public double Weight { get; set; }
    }
}
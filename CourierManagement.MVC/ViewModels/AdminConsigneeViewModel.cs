using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourierManagement.MVC.ViewModels
{
    public class AdminConsigneeViewModel
    {
        [Display(Name = "Consignee Id")]
        public int Consignee_id { get; set; }
        [Display(Name = "Consignee Name")]
        public string Consignee_Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Contact Number")]
        public string Mobile_No { get; set; }

    }
}
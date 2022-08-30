using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourierManagement.MVC.ViewModels
{
    public class AdminConsignerViewModel
    {
        [Display(Name = "Consigner Id")]
        public int Consigner_id { get; set; }
        [Display(Name = "Consigner Name")]
        public string Consigner_Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Contact  Number")]
        public string Mobile_No { get; set; }
    }
}
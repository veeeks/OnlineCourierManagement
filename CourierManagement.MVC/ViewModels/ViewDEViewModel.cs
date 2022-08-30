using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class ViewDEViewModel
    {
        public int Delivery_Id { get; set; }
        [Display(Name = "Email")]
        public string DE_Email { get; set; }

        [Display(Name = "Password")]
        public string DE_Password { get; set; }

        [Display(Name = "Contact Number")]
        public string Contact_number { get; set; }

        public string Name { get; set; }

        [Display(Name = "Status")]
        public string DStatus { get; set; }
        [Display(Name = "Origin City")]
        public string City { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class AssignDEsViewModel
    {
        [Required(ErrorMessage = "Please Select")]
        [Display(Name="Consignment")]
        public int Consignment_Id { get; set; }
        [Required(ErrorMessage = "Please Select")]
        [Display(Name = "Delivery Executive")]
        public int Delivery_Id { get; set; }
        
        public string Name { get; set; }
    }
}
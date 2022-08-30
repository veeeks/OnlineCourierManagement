using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourierManagement.MVC.ViewModels
{
    public class AdminConsignmentViewModel
    {
        [Display(Name = "Consignment Id")]
        public int Consignment_Id { get; set; }

        [Display(Name="Type")]
        public string Consignment_Type { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        [Display(Name = "Booking Date")]
        public Nullable<System.DateTime> BookingDate { get; set; }

        [Display(Name = "Billing Amount")]
        public double Billing_Amount { get; set; }

        [Display(Name = "Consignee Id")]
        public Nullable<int> Consignee_Id { get; set; }

        [Display(Name = "Consigner Id")]
        public Nullable<int> Consigner_Id { get; set; }

        public virtual AdminConsigneeViewModel Consignee { get; set; }
        public virtual AdminConsignerViewModel Consigner { get; set; }
    }
}
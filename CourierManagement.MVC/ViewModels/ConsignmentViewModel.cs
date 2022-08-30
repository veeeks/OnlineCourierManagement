using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class ConsignmentViewModel
    {
        public string Consignment_Type { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; } = 1;
        public string Description { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public double Billing_Amount { get; set; }
        public int Consignee_Id { get; set; }
        public int Consigner_Id { get; set; }
    }
}
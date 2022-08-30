using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class ConsignmentStatusViewModel
    {
        public string Status { get; set; } = "Pending";

        public DateTime ExpectedDelivery { get; set; } = DateTime.Now.AddDays(3);

        public int Consignment_Id { get; set; }
    }
}
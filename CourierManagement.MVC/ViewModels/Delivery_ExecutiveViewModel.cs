using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class Delivery_ExecutiveViewModel
    {

        public int Delivery_Id { get; set; }
        public Nullable<int> Consignment_Id { get; set; }
        public string DE_Email { get; set; }
        public byte[] DE_Password { get; set; }
        public string Contact_number { get; set; }
        public string Name { get; set; }
        public string DStatus { get; set; }
        public string City { get; set; }
        public Nullable<int> MaxQty { get; set; }
    }
}
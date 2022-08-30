using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CourierManagement.MVC.ViewModels
{
    public class AConsignmentStatusViewModel
    {
       // public int Status_id { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> ExpectedDelivery { get; set; }  

        [Display(Name="Consignment Id")]
        public Nullable<int> Consignment_Id { get; set; }

        [Display(Name = "Delivery Executive Id")]
        public Nullable<int> Delivery_Id { get; set; }
        [Display(Name = "Consignee Id")]
        public int Consignee_id { get; set; }
        [Display(Name = "Consignee Name")]
        public string Consignee_Name { get; set; }

        [Display(Name = "Consigner Id")]
        public int Consigner_id { get; set; }
        [Display(Name = "Consigner Name")]
        public string Consigner_Name { get; set; }

        public virtual AdminConsigneeViewModel Consignee { get; set; }
        public virtual AdminConsignerViewModel Consigner { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class TrackCourierViewModel
    {
        [Display(Name="Consignee Name")]
        public string Consignee_Name { get; set; }
        [Display(Name = "Consignee Address")]
        public string RAddress { get; set; }
        [Display(Name = "Consignee City")]
        public string RCity { get; set; }
        [Display(Name = "Consigner Name")]
        public string Consigner_Name { get; set; }
        [Display(Name = "Consigner Address")]
        public string SAddress { get; set; }
        [Display(Name = "Consigner City")]
        public string SCity { get; set; }
        [Display(Name = "Consignment Number")]
        public int Consignment_Id { get; set; }

        public string Status { get; set; }
        [Display(Name = "Mobile Number")]
        public string Contact_number { get; set; }
        [Display(Name = "Delivery Executive Name")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Expected Delivery")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ExpectedDelivery { get; set; }
    }
}
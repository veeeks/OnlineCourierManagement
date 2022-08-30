using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class BillViewModel
    {
        [Display(Name ="Consignee Name")]
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
        [Display(Name = "Consignment Type")]
        public string Consignment_Type { get; set; }
        [Display(Name = "Weight")]
        public double Weight { get; set; }
        [Display(Name = "Total Amount (Rs)")]
        public double Billing_Amount { get; set; }
    }
}
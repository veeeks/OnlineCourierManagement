using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class BookConsignmentViewModel
    {
       
        [Display(Name ="Consigner Name")]
        [Required]
        public string Consigner_Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name ="Consigner Address")]
        public string SAddress { get; set; }
        [Display(Name ="Consigner City")]
        public string SCity { get; set; }
        [Required]
        [Display(Name ="Mobile Number")]
        [RegularExpression(@"[7-9]\d{9}", ErrorMessage = "Enter valid mobile number.")]
        public string SMobile_No { get; set; }



        [Required]
        [Display(Name = "Consignee Name")]
        public string Consignee_Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Consignee Address")]
        [Required]
        public string RAddress { get; set; }
        [Display(Name = "Consignee City")]
        public string RCity { get; set; }
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"[6-9]\d{9}",ErrorMessage ="Enter valid mobile number.")]
        [Required]
        public string RMobile_No { get; set; }




        [Display(Name = "Description (Optional)")]
        public string Description { get; set; }
        [Display(Name = "Consignment Type")]
        public string Consignment_Type { get; set; }
        public double Weight { get; set; }
        [Display(Name = "Total amount (Rs)")]
        public double Billing_Amount { get; set; }

    }
}
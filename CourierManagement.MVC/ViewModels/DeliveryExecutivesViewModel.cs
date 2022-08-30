using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CourierManagement.MVC.ViewModels
{
    public class ConsignmentStatusDisplay
    {
        public int Consignment_Id { get; set; }
        [Display(Name ="Type")]
        public string Consignment_Type { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        [Display(Name ="Consigner Name")]
        public string Consigner_Name { get; set; }
        [Display(Name = "Consignee Name")]
        public string Consignee_Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Booking Date")]
        public DateTime BookingDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Exepcted Delivery")]
        public DateTime ExpectedDelivery { get; set; }
        public string Status { get; set; }
        
    }

    public class ConsignmentStatusDetailsViewModel
    {
        [Display(Name ="Consigner Details")]
        public string Consigner_Name { get; set; }
        public string Consigner_Address { get; set; }

        [Display(Name ="Consignee Details")]
        public string Consignee_Name { get; set; }
        public string ConsigneeAddress { get; set; }
        [Display(Name="Booking Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BookingDate { get; set; }

        [Display(Name = "Exepcted Delivery")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ExpectedDelivery { get; set; }

        [Display(Name ="Billing Amount")]
        public double Billing_Amount { get; set; }
        public string Consigner_ContactNumber { get; set; }
        public string City { get; set; }
        public string Consignee_Contact_Number { get; set; }
        public string Consignee_City { get; set; }
    }

    public class UpdateConsignmentDetailsViewModel
    {
        [Required]

        [Display(Name = "Consignment Status")]
        public string Status { get; set; }
        [Required]
        public string Remarks { get; set; }

        [Required]
        [Display(Name = "Courier Delivery Date")]
       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime ExpectedDelivery { get; set; }
    }
    public enum Status
    {
        Transit,
        Delivered,
    }
    public class DeliveryExecutivesViewModel
    {
        public int Delivery_Id { get; set; }
       
        public string Name { get; set; }

        [Display(Name="Contact Number")]
        [RegularExpression(@"^[6789][0-9]{9}$",ErrorMessage ="Enter the correct mobile number")]
        public string Contact_number { get; set; }
        [Required]
        public string City { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourierManagement.API
{
    using System;
    
    public partial class usp_GetAllConsignments_Result
    {
        public int Consignment_Id { get; set; }
        public string Consignment_Type { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public double Billing_Amount { get; set; }
        public Nullable<int> Consignee_Id { get; set; }
        public Nullable<int> Consigner_Id { get; set; }
        public string Status { get; set; }
        public Nullable<int> Delivery_Id { get; set; }
        public Nullable<System.DateTime> ExpectedDelivery { get; set; }
    }
}

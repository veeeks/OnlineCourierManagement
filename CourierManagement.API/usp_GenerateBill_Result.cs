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
    
    public partial class usp_GenerateBill_Result
    {
        public string Consignee_Name { get; set; }
        public int Consignee_id { get; set; }
        public string RAddress { get; set; }
        public string RCity { get; set; }
        public string Consigner_Name { get; set; }
        public int Consigner_id { get; set; }
        public string SAddress { get; set; }
        public string SCity { get; set; }
        public int Consignment_Id { get; set; }
        public double Billing_Amount { get; set; }
    }
}

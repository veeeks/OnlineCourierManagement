﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CourierManagementEntities : DbContext
    {
        public CourierManagementEntities()
            : base("name=CourierManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Consignee> Consignees { get; set; }
        public virtual DbSet<Billing_Details> Billing_Details { get; set; }
        public virtual DbSet<City_Price_Details> City_Price_Details { get; set; }
        public virtual DbSet<Consigner> Consigners { get; set; }
        public virtual DbSet<Consignment> Consignments { get; set; }
        public virtual DbSet<Consignment_Status> Consignment_Status { get; set; }
        public virtual DbSet<Delivery_Executive> Delivery_Executive { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
    
        public virtual ObjectResult<usp_GenerateBillDetails_Result> usp_GenerateBillDetails(Nullable<int> consignmentId)
        {
            var consignmentIdParameter = consignmentId.HasValue ?
                new ObjectParameter("consignmentId", consignmentId) :
                new ObjectParameter("consignmentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GenerateBillDetails_Result>("usp_GenerateBillDetails", consignmentIdParameter);
        }
    
        public virtual ObjectResult<usp_GetDelieveryExecutiveDetailsByEmail_Result> usp_GetDelieveryExecutiveDetailsByEmail(string dEmail)
        {
            var dEmailParameter = dEmail != null ?
                new ObjectParameter("DEmail", dEmail) :
                new ObjectParameter("DEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetDelieveryExecutiveDetailsByEmail_Result>("usp_GetDelieveryExecutiveDetailsByEmail", dEmailParameter);
        }
    
        public virtual ObjectResult<usp_GetDelieveryExecutiveDetailsById_Result> usp_GetDelieveryExecutiveDetailsById(Nullable<int> dId)
        {
            var dIdParameter = dId.HasValue ?
                new ObjectParameter("DId", dId) :
                new ObjectParameter("DId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetDelieveryExecutiveDetailsById_Result>("usp_GetDelieveryExecutiveDetailsById", dIdParameter);
        }
    
        public virtual ObjectResult<usp_TrackCourier_Result> usp_TrackCourier(Nullable<int> consignmentid)
        {
            var consignmentidParameter = consignmentid.HasValue ?
                new ObjectParameter("Consignmentid", consignmentid) :
                new ObjectParameter("Consignmentid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_TrackCourier_Result>("usp_TrackCourier", consignmentidParameter);
        }
    
        public virtual ObjectResult<usp_ViewConsignmentByDeliveryExecutiveId_Result> usp_ViewConsignmentByDeliveryExecutiveId(Nullable<int> did)
        {
            var didParameter = did.HasValue ?
                new ObjectParameter("Did", did) :
                new ObjectParameter("Did", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ViewConsignmentByDeliveryExecutiveId_Result>("usp_ViewConsignmentByDeliveryExecutiveId", didParameter);
        }
    
        public virtual ObjectResult<usp_ViewConsignmentDetailsByConsignmentId_Result> usp_ViewConsignmentDetailsByConsignmentId(Nullable<int> consignmentId)
        {
            var consignmentIdParameter = consignmentId.HasValue ?
                new ObjectParameter("ConsignmentId", consignmentId) :
                new ObjectParameter("ConsignmentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ViewConsignmentDetailsByConsignmentId_Result>("usp_ViewConsignmentDetailsByConsignmentId", consignmentIdParameter);
        }
    
        public virtual ObjectResult<usp_ViewUpdateConsignmentDetails_Result> usp_ViewUpdateConsignmentDetails(Nullable<int> cId)
        {
            var cIdParameter = cId.HasValue ?
                new ObjectParameter("CId", cId) :
                new ObjectParameter("CId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ViewUpdateConsignmentDetails_Result>("usp_ViewUpdateConsignmentDetails", cIdParameter);
        }
    
        public virtual int usp_AssignConsignmentToDeliveryExecutive(Nullable<int> consignmentId, Nullable<int> deliveryId)
        {
            var consignmentIdParameter = consignmentId.HasValue ?
                new ObjectParameter("ConsignmentId", consignmentId) :
                new ObjectParameter("ConsignmentId", typeof(int));
    
            var deliveryIdParameter = deliveryId.HasValue ?
                new ObjectParameter("DeliveryId", deliveryId) :
                new ObjectParameter("DeliveryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_AssignConsignmentToDeliveryExecutive", consignmentIdParameter, deliveryIdParameter);
        }
    
        public virtual ObjectResult<usp_GetAllConsignments_Result> usp_GetAllConsignments()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetAllConsignments_Result>("usp_GetAllConsignments");
        }
    
        public virtual int usp_AddDeliveryExecutive(string contact_Number, string name, byte[] password)
        {
            var contact_NumberParameter = contact_Number != null ?
                new ObjectParameter("Contact_Number", contact_Number) :
                new ObjectParameter("Contact_Number", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_AddDeliveryExecutive", contact_NumberParameter, nameParameter, passwordParameter);
        }
    
        public virtual int usp_DeleteDeliveryExecutive(Nullable<int> delivery_Id)
        {
            var delivery_IdParameter = delivery_Id.HasValue ?
                new ObjectParameter("Delivery_Id", delivery_Id) :
                new ObjectParameter("Delivery_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteDeliveryExecutive", delivery_IdParameter);
        }
    
        public virtual ObjectResult<usp_GenerateBill_Result> usp_GenerateBill(Nullable<int> consignmentId)
        {
            var consignmentIdParameter = consignmentId.HasValue ?
                new ObjectParameter("consignmentId", consignmentId) :
                new ObjectParameter("consignmentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GenerateBill_Result>("usp_GenerateBill", consignmentIdParameter);
        }
    
        public virtual ObjectResult<usp_ViewConsignmentByDeliveryExecutiveEmail_Result1> usp_ViewConsignmentByDeliveryExecutiveEmail(string email)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_ViewConsignmentByDeliveryExecutiveEmail_Result1>("usp_ViewConsignmentByDeliveryExecutiveEmail", emailParameter);
        }
    }
}
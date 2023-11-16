using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.ProductOrder
{
    public class OrderHeadViewModel :Column1To12 
    {
      public int? OrderHeadId{get;set;}
      public int? OrderNo{get;set;}
      public int? OrderTypeId{get;set;}
      public string? OrderTypeName{get;set;}
      public string? OrderDate{get;set;}
      public string? OrderedBy{get;set;}
      public string? CreatedAt{get;set;}
      public string? UpdatedAt{get;set;}
      public string? UpdatedBy{get;set;}
      public int? OrderStatusId{get;set;}
      public string?OrderStatusName{get;set;}
      public string?PossibleDeliveryDate{get;set;}
      public int?PaymentMethodId{get;set;}
      public string?PaymentMethodName{get;set;}
      public string?PaymentMethodNameShortName{get;set;}
      public decimal?DeliveryCharge{get;set;}
      public bool?IsConfirmOrder{get;set;}
      public string?ConfirmedBy{get;set;}
      public string?ConfirmedAt{get;set;}
      public bool?IsOrderPreparedForDelivery{get;set;}
      public string?OrderPreparedBy{get;set;}
      public bool?IsCancelled{get;set;}
      public string?CancelledBy{get;set;}
      public string?CancelledAt{get;set;}
      public int?CancelledReasonId{get;set;}
      public string?CancelledReason{get;set;}
      public bool?IsDelivered{get;set;}
      public string?DeliveredBy{get;set;}
      public string?DeliveredAt{get;set;}
      public bool?IsReturnProduct{get;set;}
      public string?ReturnProductAt{get;set;}
      public string?ReturnProductReason{get;set;}
      public int?TotalProductItem{get;set;}
      public int?TotalProductQuqntity{get;set;}
      public decimal?TotalProductPrice{get;set;}
      public int?TotalDeliveryItem{get;set;}
      public decimal?TotalDeliveryQuantity{get;set;}
      public decimal?TotalDeliveryPrice{get;set;}
      public int?LocationId{get;set;}
      public string?LocationName{get;set;}
      public string?BillingAddress{get;set;}
        public string? Apt_Suit { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? District { get; set; }
        public string? Lat { get; set; }
        public string? Lang { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? AdditionalPhoneNumber { get; set; }
        public string? DisplayUserStatus { get; set; } 
        public int? TotalRows { get; set; }
        public int? StoreId { get; set; } 



    }
    public class OrderHeadDataModel 
    {
        public List<OrderHeadViewModel>? OrderHeadList { get; set; }  
        public Pager? pager { get; set; }
    }
    public class OrderDetailsViewModel 
    {
      public int?OrderDetailId{get;set;}
      public int?OrderHeadId{get;set;}
      public int?ProductId{get;set;}
      public string?ProductName{get;set;}
      public string?ProductDescription{get;set;}
      public string?ProductShortDescription{get;set;}
      public string?ProductImageName{get;set;}
      public string?CategoryName{get;set;}
      public string?CategoryImage{get;set;}
      public string?BrandName{get;set;}
      public string?BrandImage{get;set;}
      public int?ProductColorId{get;set;}
      public string?ColorName{get;set;}
      public string?ColorCode{get;set;}
      public string?ProductSize{get;set;}
      public int? ProductImageId{get;set;}   
      public int?OrderQty{get;set;}
      public decimal?UnitPrice{get;set;}
      public decimal?TotalPrice{get;set;}
      public decimal?UniteDiscount{get;set;}
      public decimal?TotalDiscount{get;set;}
      public bool?IsConfirmed{get;set;}
      public bool?IsCancelled{get;set;}
      public string?CancelledReason{get;set;}
      public int? CancelledReasonId { get; set; }
      public string?CancelledAt{get;set;}
      public bool? IsRequestToStock { get; set; }
    public string? Column1 { get; set; }
    public string? Column2 { get; set; }
    public string? Column3 { get; set; }
    public string? Column4 { get; set; }
    public string? Column5 { get; set; }
    public string? Column6 { get; set; }
    public string? Column7 { get; set; }
    public string? Column8 { get; set; }
    public string? Column9 { get; set; }
    public string? Column10 { get; set; }
    public string? Column11 { get; set; }
    public string? Column12 { get; set; }
    public int? StoreId { get; set; }
    public int? ReturnQty { get; set; }
    public string? ReturnDate { get; set; }
    public string? Reason { get; set; }
    }
    public class TypeOrderDetailsConfirmation
    {
        public int? OrderDetailId {get;set;}
	    public bool? IsCancelled {get;set;}
	    public string? CancellationReason {get;set;}
	    public int? OrderQty {get;set;}
	    public decimal? UnitPrice {get;set;}
	    public decimal? TotalPrice {get;set;}
	    public string? Column1 {get;set;}
	    public string? Column2{get;set;}
	    public string? Column3{get;set;}
	    public string? Column4{get;set;}
	    public string? Column5{get;set;}
	    public string? Column6{get;set;}
	    public string? Column7{get;set;}
    }
    public class OrderProcessConfirmationModel
    {
        public int? Id { get; set; }
        public string? CancellationReason { get; set; }
        public int? OrderStatusId { get; set; }
        public List<TypeOrderDetailsConfirmation>? TypeOrderDetailsConfirmation { get; set; } 
    }
    public class OrderProcessStatusChangeModel 
    {
        public int? Id { get; set; }
        public string? Reason { get; set; }
        public int? OrderStatusId { get; set; }
    }
    public class CustomerDefaultAddressModel
    { 
        public string? Address { get; set; }
        public string? Apt_Suit { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }	
       public string? Country { get; set; }
       public string? State { get; set; }	
       public string? PostCode { get; set; }	
       public string? Direction { get; set; }	
       public double? Lat { get; set; }	
       public double? Lang { get; set; } 
       public int? Role { get; set; }
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? Column1 { get; set; } 
        public string? Column2 { get; set; } 
        public string? Column3 { get; set; }
        public string? Column4 { get; set; }
        public string? Column5 { get; set; }
        public string? Column6 { get; set; }
        public string? Column7 { get; set; }
    }
    public class OrderReturnViewModel
    {
        public int? OrderDetailId { get; set; }
        public int? OrderNo { get; set; }
        public string? ProductName { get; set; }
        public string? ProductNameBangla { get; set; }
        public string? CategoryName { get; set; }
        public int? OrderQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? ReturnQty { get; set; }
        public string? ReturnDate { get; set; }
        public string? Reason { get; set; }
        public string? CreatedBy { get; set; }
        public string? ProductImage { get; set; }
        public string? DeliveryDate { get; set; }

    }
    public class ProductReturnModel
    {
        public int? OrderDetailId { get; set; }
        public int? ReturnQty { get; set; }
        public string? Reason { get; set; }
        public string? ReturnDate { get; set; }
    }
       
 

}

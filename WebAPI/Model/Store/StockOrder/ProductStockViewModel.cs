using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Store.StockOrder
{
    public class ProductStockViewModel
    {
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
        public string? ProductSize { get; set; }
        public int? ProductImageId { get; set; }
        public string? ProductImageName { get; set; }
        public int? ProductBrandId { get; set; }
        public string? BrandName { get; set; }
        public string? BrandImage { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryImage { get; set; }
        public string? SupplierName { get; set; }
        public int? ProductSupplierId { get; set; }
        public int? OrderQty { get; set; }
        public int? StockQty { get; set; }
        public int? BalanceQty { get; set; }
        public int? RequireStock { get; set; }
        public decimal? SupplyPrice { get; set; }
        public decimal? VendorPrice { get; set; } 
        public decimal? VendorSpecialPrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? SpecialPrice { get; set; }
    }
    public class TypeOrderHeadIdList
    {
        public int? OrderHeadId { get; set; } 
    }
    public class ProductSupplyOrderModel
    {
        public int? Id { get; set; }
        public int? SupplierId { get; set; }
        public string? DeliveryDate { get; set; }
        public string? SupplyNote { get; set; }
        public int? Quantity { get; set; } 
        public List<TypeProductSupplyDetails>? ProductSupplyDetails { get; set; }
        public List<TypeExpenseHistoryModel>? ExpenceList { get; set; }
    }
  
    public class TypeExpenseHistoryModel 
    {
        public int? ExpenseReasonId { get; set;}
        public decimal? CostAmount { get; set; }
    }
    public class TypeProductSupplyDetails 
    {
        public int?Id  { get; set; }
        public int?ProductId  { get; set; }
        public string?Color{ get; set; }
        public string?Size{ get; set; }
        public int?ImageId{ get; set; }
        public int?OrderQty{ get; set; }
        public decimal?UnitPrice{ get; set; }
        public decimal?TotalPrice{ get; set; }
        public int?SupplyQty{ get; set; }
        public decimal?SupplyUnitPrice{ get; set; }
        public decimal?TotalSupplyPrice{ get; set; }
        public int? SupplierId{ get; set; } 
    }
    public class ProductSupplyOrderViewModel 
    {
      public int?  ProductSupplyOrderId { get; set; }
      public int?  OrderNo { get; set; }
      public string?  StoreName { get; set; }
      public int?  StoreId { get; set; }
      public int?  SupplierId { get; set; }
      public string?  SupplierName { get; set; }
      public string?  SupplierContactName { get; set; }
      public string?  SupplierContactNumber { get; set; }
      public string?  SupplierAddress { get; set; }
      public string?  OrderDate { get; set; }
      public string?  RequiredDeliveryDate { get; set; }
      public int?  SupplyOrderStatusId { get; set; }
      public string?  SupplyOrderStatusName { get; set; }
      public string?  CreatedAt { get; set; }
      public string?  CreatedBy { get; set; }
      public bool?  IsApproved { get; set; }
      public string?  ApprovedBy { get; set; }
      public string?  ApprovedAt { get; set; }
      public string?  ReceivedAt { get; set; }
      public string?  ReceivedBy { get; set; }
      public bool?  IsCancelled { get; set; }
      public string?  CancelledBy { get; set; }
      public string?  CancelledReason { get; set; }
      public string?  UpdatedBy { get; set; }
      public string?  UpdatedAt { get; set; }
      public bool?  IsDeleted { get; set; }
      public string?  DeletedAt { get; set; }
      public string?  DeletedBy { get; set; }
      public int?  TotalOrderItem { get; set; }
      public int?  TotalOrderQty { get; set; }
      public decimal?  TotalOrderPrice { get; set; }
      public int?  ReceivedItem { get; set; }
      public int?  TotalReceivedQty { get; set; }
      public decimal?  TotalReceivedPrice { get; set; } 
    }

    public class ProductSupplyDetailsViewModel 
    {
       public int?  ProductSupplyDetailId { get; set; }  
       public int?  ProductSupplyOrderId { get; set; } 
       public int?  ProductId { get; set; } 
       public string?  ProductName { get; set; } 
       public string?  ProductNameBangla { get; set; } 
       public string?  CategoryName { get; set; } 
       public string?  Color { get; set; } 
       public string?  Size { get; set; } 
       public int?  ImageId { get; set; } 
       public string?  ImageName { get; set; } 
       public int?  OrderQty { get; set; } 
       public decimal?  UnitPrice { get; set; } 
       public decimal?  TotalPrice { get; set; } 
       public int?  SupplyQty { get; set; } 
       public decimal?  SupplyUnitPrice { get; set; } 
       public decimal?  TotalSupplyPrice { get; set; } 
       public int?  SupplierId { get; set; } 
       public string?  SupplierName { get; set; }  
    }
    public class OrderStatusViewModel
    {
        public int? SupplyOrderStatusId { get; set; }
        public string? SupplyOrderStatusName { get; set; } 
    }
    public class ProductStockModel 
    {
        public int? Id { get; set; }
        public int? StockQty { get; set; } 
      
    }
    public class ProductStockOrderViewModel 
    {
        public int? ProductSupplyOrderId { get; set; }  
        public int? OrderNo { get; set; } 
        public string? StoreName { get; set; } 	
        public int? StoreId	 { get; set; } 
        public int? SupplierId { get; set; } 	
        public string? SupplierName { get; set; } 	
        public string? SupplierContactName { get; set; } 	
        public string? SupplierContactNumber { get; set; } 	
        public string? SupplierAddress { get; set; } 	
        public string? OrderDate { get; set; } 
        public string? RequiredDeliveryDate { get; set; } 	
        public int? SupplyOrderStatusId { get; set; } 	
        public string? SupplyOrderStatusName { get; set; } 	
        public string? CreatedAt { get; set; } 	
        public string? CreatedBy { get; set; } 	
        public bool? IsApproved { get; set; } 
        public string? ApprovedBy { get; set; } 	
        public string? ApprovedAt { get; set; } 	
        public string? ReceivedAt { get; set; } 	
        public string? ReceivedBy { get; set; } 	
        public bool? IsCancelled { get; set; } 
        public string? CancelledBy	 { get; set; } 
        public string? CancelledReason { get; set; } 	
        public string? UpdatedBy { get; set; }
        public string? UpdatedAt { get; set; } 
        public bool? IsDeleted { get; set; } 
        public string? DeletedAt { get; set; } 
        public string? DeletedBy { get; set; } 
        public int? TotalOrderItem { get; set; } 
        public int? TotalOrderQty { get; set; } 
        public decimal? TotalOrderPrice { get; set; } 
        public int? TotalReceivedItem { get; set; } 
        public int? TotalReceivedQty { get; set; } 
        public decimal? TotalReceivedPrice { get; set; }
    }
    public class StockOrderAppovalModel
    {
        public int?Id { get; set; }
        public string? Reason { get; set; } 
    }
    public class StockProductModel
    {
        public int? ProductCategoryId { get; set; } 	
        public string?CategoryName { get; set; } 	
        public int?ProductId { get; set; } 	
        public string? ProductName { get; set; } 	
        public string? ProductNameBangla { get; set; } 	
        public int?TotalProduct { get; set; } 	
        public decimal?SupplyPrice { get; set; } 	
        public decimal?VendorPrice { get; set; } 
        public decimal?VendorSpecialPrice { get; set; } 	
        public decimal?RetailPrice { get; set; } 	
        public decimal?SpecialPrice { get; set; } 	
        public decimal?OfferPrice { get; set; } 	
        public string? OfferStartDate { get; set; } 
        public string? OfferEndDate { get; set; } 	
        public string? Barcode { get; set; } 	
        public string? BrandName { get; set; } 	
        public string? BrandImage { get; set; } 	
        public string? MeasurementName { get; set; } 	
        public int?Amount { get; set; } 	
        public string? VolumeMeasurementName { get; set; } 
        public int?VolumeAmount { get; set; } 	
        public string? ShortDescription { get; set; } 
        public string? ProductDescription	 { get; set; } 
        public string? CategoryImage { get; set; } 	
        public string? CategoryParentTree { get; set; } 
        public string? SupplierName { get; set; } 	
        public string? SupplierDescription	 { get; set; } 
        public string? CountryName { get; set; } 	
        public string? CountryShortName { get; set; } 	
        public int?PackSizeQty { get; set; } 	
        public int?Weight { get; set; } 	
        public string? WeightMeasurementName { get; set; } 	
        public string? ImageList { get; set; } 
        public string? UsageAreaList { get; set; } 
        public string? ColorList { get; set; } 
        public string? SizeList { get; set; }
        public bool? IsRequestToStock { get; set; }
        public int? RequestedStockQty { get; set; }
        public string? RequestedBy { get; set; }
        public string? RequestedDate { get; set; } 
    }
    public class StockLevelModel
    {
        public int? StoreId { get; set; }
        public int?ProductCategoryId { get; set; } 
        public string? CategoryName { get; set; } 	
        public int?ProductId { get; set; } 	
        public string? ProductName { get; set; } 	
        public string? ProductNameBangla { get; set; } 	
        public string? BrandName { get; set; } 	
        public string? SupplierName { get; set; } 	
        public string? Image { get; set; } 	
        public int?StockQuantity { get; set; } 	
        public int?LowStockLevelQuantity { get; set; } 
        public int? ReorderQuantity { get; set; }
    }
    public class ProductCategoryModel 
    {
       
        public int? ProductCategoryId { get; set; }
        public string? CategoryName { get; set; }
       
    }
    public class QuickStockLevelModel  
    {
        public int? StoreId { get; set; }
        public int? ProductId { get; set; }
        public int? CurrentStockQty { get; set; }
        public int? LowStockLevelQty { get; set; }
        public int? ReOrderQty { get; set; }

    }
    public class RequestProductStockModel 
    {
        public int? StoreId { get; set; }
        public int? ProductId { get; set; }

    }
    public class ProductDamageOrExpireHistoryModel 
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? DamageTypeId { get; set; }
        public string? Remarks { get; set; }

    }

    public class ExpenseHistoryViewModel
    {
        public int? ExpenseHistoryHeadId { get; set; }
        public int? ProductSupplyOrderId { get; set; }
        public int? OrderNo { get; set; }
        public int? OrderHeadId { get; set; }
        public string? Remarks { get; set; }
        public int? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedAt { get; set; }
        public decimal? TotalExpense { get; set; } 

    }
    public class ExpenseHistoryDetailsViewModel 
    {
        public int? ExpenseHistoryId { get; set; }
        public int? ExpenseReasonId { get; set; } 
        public decimal? CostAmount { get; set; }
        public int? ExpenseHistoryHeadId { get; set; }
        public string? EXReasonNameEn { get; set; }
        public string? EXReasonNameBn { get; set; } 
    }


}

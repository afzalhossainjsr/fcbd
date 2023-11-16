using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.Product
{
    public  class StoreProductViewModel : Column1To12
    {
        public int? Id { get; set; } 
        public string? ProductName { get; set; }
        public string? ProductNameBangla { get; set; }
        public string? Barcode { get; set; }
        public int? ProductBrandId { get; set; }
        public string? BrandName { get; set; }
        public string? BrandImage { get; set; }
        public int? MeasurementId { get; set; }
        public string? MeasurementName { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsAddedVolume { get; set; }
        public int? VolumeMeasurementId { get; set; }
        public string? VolumeMeasurementName { get; set; }
        public decimal? VolumeAmount { get; set; }
        public string? ShortDescription { get; set; }
        public string? ProductDescription { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryImage { get; set; }
        public string? CategoryParentTree { get; set; }
        public int? ParentId { get; set; }
        public int? StageNumber { get; set; }
        public int? TotalProduct { get; set; }
        public decimal? SupplyPrice { get; set; }
        public decimal? VendorPrice { get; set; }
        public decimal? VendorSpecialPrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? SpecialPrice { get; set; }
        public decimal? MarkUpPercentage { get; set; }
        public string? SupplierDescription { get; set; }
        public int? ProductSupplierId { get; set; }
        public string? SupplierName { get; set; }
        public bool? IsAvailableColor { get; set; }
        public bool? AllowColorWhenChoose { get; set; }
        public bool? AllowImageWhenChoose { get; set; }
        public bool? AllowSizeWhenChoose { get; set; }
        public int? OriginCountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryShortName { get; set; }
        public bool? IsAddedWarranty { get; set; }
        public int? WarrantyTypeParameterId { get; set; }
        public string? WarrantyTypeName { get; set; }
        public decimal? WarrantyPeriod { get; set; }
        public int? WarrantyPeriodParameterId { get; set; }
        public string? WarrantyPeriodName { get; set; }
        public bool? IsAddedUsageArea { get; set; }
        public bool? IsAddedPackSize { get; set; }
        public int? PackSizeQty { get; set; }
        public bool? IsAddedWeight { get; set; }
        public decimal? Weight { get; set; }
        public int? WeightMeasurementId { get; set; }
        public string? WeightMeasurementName { get; set; }
        public int? TaxId { get; set; }
        public string? TaxName { get; set; }
        public decimal? TaxAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ImageList { get; set; }
        public string? UsageAreaList { get; set; }
        public string? ColorList { get; set; }
        public string? SizeList { get; set; }
        public string? SearchKeyword { get; set; }

        public decimal? OfferPrice { get; set; }
        public string? OfferStartDate { get; set; }
        public string? OfferEndDate { get; set; }
        public int? IsActive { get; set; }    



    }
    public class StoreProductInitialViewModel
    {
        public List<ProductSKUViewModel>? ProductSKU { get; set; } 
        public List<ProductColorViewModel>? ProductColor { get; set; }
        public List<ProductSizeViewModel>? ProductSize { get; set; }
        public List<ProductImageViewModel>? ProductImage { get; set; }
        public List<ProductUsageAreaViewModel>? ProductUsageArea { get; set; }
        public List<ProductFeaturesListViewModel>? ProductFeaturesList { get; set; }
        public List<ProductOfferListViewModel>? ProductOfferList { get; set; }
        public List<ProductDescriptionViewModel>? ProductDescription { get; set; }
        public List<StoreWiseProductLowStockAndReOrderingViewModel>? StoreWiseProductLowStockAndReOrdering { get; set; }
       
    }
    public class ProductSKUViewModel   
    {
        public int? ProductId { get; set; }
        public string? SKU { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
   
    public class ProductColorViewModel 
    {
        public int? Id { get; set; } 
        public int? ProductId { get; set; }
        public int? ColorInfoId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
        public bool? IsActive { get; set; }
    }
    public class ProductSizeViewModel 
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? Size { get; set; }
        public string? SizeName { get; set; }
    }
    public class ProductFeaturesListViewModel : Column1To7
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? FeaturesHeadings { get; set; }
        public string? FeaturesDescription { get; set; }
    }
    
    public class ProductOfferListViewModel : Column1To7
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? OfferHeadings { get; set; }
        public string? OfferDescription { get; set; } 
    }
    public class ProductDescriptionViewModel : Column1To7
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? DescriptionHeadings { get; set; }
        public string? Description { get; set; }
    }
    public class ProductImageViewModel
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? ImageName { get; set; }
        public int? ImageOrder { get; set; }
    }

    public class StoreWiseProductLowStockAndReOrderingViewModel
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? StoreId { get; set; }
        public int? LowStockLevelQuantity { get; set; }
        public int? ReorderQuantity { get; set; }
        public bool? ReceiveLowStockNotifications { get; set; } 
    }
    public class ProductUsageAreaViewModel 
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? UsageAreaParameterId { get; set; }
        public string? UsageAreaName { get; set; } 
    }
   

}

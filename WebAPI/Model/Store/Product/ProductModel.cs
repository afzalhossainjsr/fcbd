using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.Product
{
    public class StoreProductModel 
    {
        public int?Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductNameBangla { get; set; }
        public string? Barcode { get; set; }
        public int? ProductBrandId { get; set; }
        public bool? IsAddedVolume { get; set; }
        public int? VolumeMeasurementId { get; set; }
        public decimal? VolumeAmount { get; set; }
        public int? MeasurementId { get; set; }
        public decimal? Amount { get; set; }
        public string? ShortDescription { get; set; }
        public string? ProductDescription { get; set; }
        public int? ProductCategoryId { get; set; }
        public decimal? SupplyPrice { get; set; }
        public decimal? VendorPrice { get; set; }
        public decimal? VendorSpecialPrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? SpecialPrice { get; set; }
        public decimal? MarkUpPercentage { get; set; }
        public int? ProductSupplierId { get; set; }
        public bool? TrackStockQuantity { get; set; }
        public bool? IsAvailableColor { get; set; }
        public bool? AllowColorWhenChoose { get; set; }
        public bool? AllowImageWhenChoose { get; set; }
        public bool? AllowSizeWhenChoose { get; set; }
        public int? OriginCountryId { get; set; }
        public bool? IsAddedWarranty { get; set; }
        public int? WarrantyTypeParameterId { get; set; }
        public decimal? WarrantyPeriod { get; set; }
        public int? WarrantyPeriodParameterId { get; set; }
        public bool? IsAddedUsageArea { get; set; }
        public bool? IsAddedPackSize { get; set; }
        public int? PackSizeQty { get; set; }
        public bool? IsAddedWeight { get; set; }
        public decimal? Weight { get; set; }
        public int? WeightMeasurementId { get; set; }
        public int? TaxId { get; set; }
        public string? SearchKeyword { get; set; }
        public List<TypeProductSKUModel>? ProductSKU { get; set; }
        public List<TypeStoreWiseProductStockModel>? StoreWiseProductStock { get; set; }
        public List<TypeProductLowStockAndReOrderingModel>? ProductLowStockAndReOrdering { get; set; }
        public List<TypeProductImageModel>? ProductImage { get; set; }
        public List<TypeProductColor>? ProductColor { get; set; }
        public List<TypeProductSize>? ProductSize { get; set; }
        public List<TypeProductUsageArea>? ProductUsageArea { get; set; }
        public List<TypeProductFeatures>? ProductFeatures { get; set; }
        public List<TypeProductFeatures>? ProductOffer { get; set; }
        public List<TypeProductFeatures>? ProductDescriptionList { get; set; }
        public List<DeletedProductListModel>? DeletedProductList { get; set; } 


    }
    public class DeletedProductListModel 
    {
        public string? ImageName { get; set; } 
    }
    public class TypeProductFeatures
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? Headings { get; set; }
        public string? Description { get; set; }
        public string? Column1 { get; set; }
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
        public string? Column4 { get; set; }
        public string? Column5 { get; set; }
    }
    public class TypeProductUsageArea
    {
        public int? UsageAreaParameterId { get; set; }
    }
    public class TypeProductSize
    {
        public int? Id { get; set; }
        public int? Size { get; set; }
        public string? SizeName { get; set; }
    }
    public class TypeProductColor
    {
        public int? Id { get; set; }
        public int? ColorInfoId { get; set; }
    }
    public class TypeProductSKUModel
    {
        public int? Id { get; set; }
        public string? SKU { get; set; }
    }
    public class TypeStoreWiseProductStockModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public int? Quantity { get; set; }
    }
    public class TypeProductLowStockAndReOrderingModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public int? LowStockLevelQuantity { get; set; }
        public int? ReorderQuantity { get; set; }
        public bool? ReceiveLowStockNotifications { get; set; }
 
    }
  
    public class TypeProductImageModel
    {
        public int? Id { get; set; }
        public string? ImageName { get; set; }
        public string? Base64String { get; set; }
        public int? ImageOrder { get; set; }
    }
    public class PrductOfferModel
    {
        public int? ProductId { get; set; }
        public decimal? OfferPrice { get; set; }
        public string? OfferStartDate { get; set; }
        public string? OfferEndDate { get; set; }
    }
    public class ProductInactiveModel
    {
        public int? ProductId { get; set; }
    }
}

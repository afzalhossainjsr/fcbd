using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.ProductOrder 
{
    public class WebProductViewModel
    {

       public int? ProductId { get; set; }
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
        public decimal? VendorPrice { get; set; }
        public decimal? VendorSpecialPrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? SpecialPrice { get; set; }
        public decimal? MarkUpPercentage { get; set; }
        public string? SupplierDescription { get; set; }

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

    }
    public class StoreProductWebModel
    {
        public List<WebProductViewModel>? ProductList { get; set; }
        public Pager? pager { get; set; }
        public List<WebProductBrandViewModel>? ProductBrandList { get; set; }
        public List<WebProductCategoryViewModel>? ProductCategoryList { get; set; }
        public WebMinPriceMaxPriceViewModel?  MinPriceMaxPriceRange { get; set; }
        public List<WebCountryViewModel>? OriginCountryList { get; set; }
        public List<WebWarrantyTypeViewModel>? WarrantyTypeList { get; set; }
        public List<WebWarrantyPeriodViewModel>? WarrantyPeriodList { get; set; }
        public List<WebColorViewModel>? ColorList { get; set; }
        public List<WebSizeViewModel>? SizeList { get; set; }
        public List<WebUsageAreaViewModel>? UsageAreaList { get; set; } 
    }
    public class WebProductBrandViewModel
    {
        public int? ProductBrandId { get; set; }
        public string? BrandName { get; set; }
        public string? BrandImage { get; set; }
    }
    public class WebProductCategoryViewModel
    {
        public int? ProductCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryImage { get; set; }
        public string? CategoryParentTree { get; set; }
    }
    public class WebMinPriceMaxPriceViewModel
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
    public class WebCountryViewModel
    {
        public int? OriginCountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CountryShortName { get; set; }
        public string? CountryCode { get; set; }
    }
    public class WebWarrantyTypeViewModel
    {
        public int? WarrantyTypeParameterId { get; set; }
        public string? WarrantyTypeName { get; set; }
    }
    public class WebWarrantyPeriodViewModel
    {
        public int? WarrantyPeriod { get; set; }
        public string? WarrantyPeriodName { get; set; }
    }
    public class WebColorViewModel
    {
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
    }
    public class WebSizeViewModel
    {
        public int? Size { get; set; }
        public string? SizeName { get; set; }
    }
    public class WebUsageAreaViewModel
    {
        public string? UsageAreaName { get; set; }
    }
    public class TotalRowsModel 
    {
        public int? TotalRows { get; set; }
    }

}

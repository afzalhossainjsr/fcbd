using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.Product
{
    public class CountryDataModel:Column1To7
	{
		public int? Id {get;set;}
		public string? CountryName{get;set;}
		public string? CountryCode{get;set;}
		public string? CountryShortName{get;set;}
		public string? GMT{get;set;}
		public string? CurrencyName{get;set;}
		public int? SLNO{get;set;}
		public string? CurrencyDescription{get;set;}
		public bool? IsActive { get; set; } 
	}
	public class MeasurementNameModel
	{
		public int? Id { get; set; }
		public string? MeasurementName { get; set; }
		public bool? IsActive { get; set; }
	}
	public class StoreTaxModel  
	{
		public int? Id { get; set; }
		public string? TaxName { get; set; }
		public decimal? TaxAmount { get; set; }
		public bool? IsActive { get; set; }
	}
	public class StoreInfoModel  
	{
		public int? Id { get; set; }
		public string? ColorName { get; set; }
		public string? ColorCode { get; set; }
		public bool? IsActive { get; set; }
	}

	public class WarrantyTypeModel
	{
		public int? Id { get; set; }
		public string? WarrantyTypeName { get; set; }
		public bool? IsActive { get; set; }
	}
	public class WarrantyPeriodModel
	{
		public int? Id { get; set; }
		public string? WarrantyPeriodName { get; set; }
		public bool? IsActive { get; set; }
	}
	public class UsageAreaModel 
	{
		public int? Id { get; set; }
		public string? UsageAreaName { get; set; }
		public bool? IsActive { get; set; }
	}
	public class OrderTypeModel 
	{
		public int? Id { get; set; }
		public string? OrderTypeName { get; set; }
		public bool? IsActive { get; set; }
	}
	public class OrderStatusModel 
	{
		public int? Id { get; set; }
		public string? StatusName { get; set; }
		public bool? IsActive { get; set; }
		public int? SLNO { get; set; }
	}
}

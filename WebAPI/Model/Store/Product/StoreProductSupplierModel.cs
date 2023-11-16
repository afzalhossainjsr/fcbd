using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.Product
{
    public class StoreProductSupplierModel
    {
        public int? Id { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierDescription { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? PhoneCodeCountryId { get; set; }
        public string? PhoneNumber { get; set; }
        public int? TelephoneCodeCountryId { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Street { get; set; }
        public string? Suburb_Word_Union_Village { get; set; }
        public string? City_Thana { get; set; }
        public string? State_District { get; set; }
        public string? Zip_PostalCode { get; set; }
        public int? CountryId { get; set; }
        public bool? SameAsPostalAddress { get; set; }
        public string? PostalStreet { get; set; }
        public string? PostalSuburb { get; set; }
        public string? PostalCity_Thana { get; set; }
        public string? PostalState_District { get; set; }
        public string? PostalZip_PostalCode { get; set; }
        public int? PostalCountryId { get; set; }
    }
    public class StoreProductSupplierViewModel :Column1To7 
    {
        public int? Id { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierDescription { get; set; }
        public int? TotalProduct { get; set; }
        public string? UpdatedAt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? PhoneCodeCountryId { get; set; }
        public string? PhoneNumber { get; set; }
        public int? TelephoneCodeCountryId { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Street { get; set; }
        public string? Suburb_Word_Union_Village { get; set; }
        public string? City_Thana { get; set; }
        public string? State_District { get; set; }
        public string? Zip_PostalCode { get; set; }
        public int? CountryId { get; set; }
        public bool? SameAsPostalAddress { get; set; }
        public string? PostalStreet { get; set; }
        public string? PostalSuburb { get; set; }
        public string? PostalCity_Thana { get; set; }
        public string? PostalState_District { get; set; }
        public string? PostalZip_PostalCode { get; set; }
        public int? PostalCountryId { get; set; }
    }
}

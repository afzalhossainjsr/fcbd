using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Store.ProductOrder
{
    public class OrderHeadModel
    {


        public int? Id { get; set; }
        public int? OrderTypeId { get; set; }
        public int? OrderStatusId { get; set; }
        public int? PaymentMethodId { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public int? LocationId { get; set; }
        public string? OrderPlacedBy { get; set; }  
        public string? BuyerUserName { get; set; }
        public List<TypeOrderDetailsModel>? OrderDetails { get; set; }
        public string? Address { get; set; }
        public string? Apt_Suit { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostCode { get; set; }
        public string? Direction { get; set; }
        public string? Lat { get; set; }
        public string? Lang { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? AdditionalPhoneNumber { get; set; } 
    }
    public class TypeOrderDetailsModel
    {
        public int? OrderDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? ProductColorId { get; set; }
        public string? ProductSize { get; set; }
        public int? ProductImageId { get; set; }
        public int? OrderQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }

    }
  
}

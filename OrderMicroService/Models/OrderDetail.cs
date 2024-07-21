using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OrderMicroService.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class OrderDetail
    {
        [BsonElement("id"), BsonRepresentation(BsonType.Int64)]
        public long ProductId { get; set; }

        [Required]
        [BsonElement("quantity"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Quantity { get; set; }

        [BsonElement("unit_price"), BsonRepresentation(BsonType.Decimal128)]
        [Required]
        public decimal UnitPrice { get; set; }
    }
}
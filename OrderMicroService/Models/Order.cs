using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
// using Microsoft.EntityFrameworkCore;

namespace OrderMicroService.Models
{
    // [Table("orders")]
    [Serializable, BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("customer_id"), BsonRepresentation(BsonType.Int64)]
        public long CustomerId { get; set; }

        [BsonElement("ordered_on"), BsonRepresentation(BsonType.DateTime)]
        public DateTime OrderOn { get; set; }

        [BsonElement("order_details")]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}

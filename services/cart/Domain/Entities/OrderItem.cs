using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class OrderItem
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int Quantity { get; set; }
}

using MongoDB.Bson;

namespace Domain.Entities;

public class OrderItem
{
    public ObjectId ItemId { get; set; }
    public int Quantity { get; set; }
}

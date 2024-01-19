namespace Domain.Entities;
////The collection name in MongoDB
[BsonCollection("carts")]
//// Book inherits from abstract class Document
public class CustomerCart : Document
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<OrderItem> Order { get; set; }
}

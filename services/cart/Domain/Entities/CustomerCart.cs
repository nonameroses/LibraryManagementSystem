namespace Domain.Entities;
////The collection name in MongoDB
//[BsonCollection("cart")]
//// Book inherits from abstract class Document
public class CustomerCart : Document
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public List<OrderItem> Orders { get; set; }
}

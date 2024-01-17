using Newtonsoft.Json;

namespace Domain.Entities;
////The collection name in MongoDB
[BsonCollection("carts")]
//// Book inherits from abstract class Document
public class CustomerCart : Document
{
    [JsonIgnore]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<OrderItem> Orders { get; set; }
}

namespace Domain.Entities;

//The collection name in MongoDB
[BsonCollection("customer")]
// Book inherits from abstract class Document
public class Customer : Document
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Email { get; set; } 
    public List<string> Orders { get; set; }
}
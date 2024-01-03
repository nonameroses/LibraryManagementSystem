namespace Domain.Entities;
//The collection name in MongoDB
[BsonCollection("books")]
// Book inherits from abstract class Document
public class Book : Document
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public bool Active { get; private set; } = true;
    public int Isbn { get; set; }
    public decimal? Quantity { get; set; }
}
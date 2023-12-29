using Domain.Entities;

[BsonCollection("books")]
public class Book : Document
{
    public string Name { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public bool Active { get; private set; } = true;
    public int Isbn { get; set; }
    public decimal? Quantity { get; set; }
}
namespace Domain.Entities;
//The collection name in MongoDB
[BsonCollection("books")]
// Book inherits from abstract class Document
public class LibraryBook : Document
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public bool Active { get; private set; } = true;
    public int Isbn { get; set; }
    public decimal? Quantity { get; set; }
}

//RuleFor(p => p.Book.Quantity)
//    .GreaterThanOrEqualTo(1)
//    .WithName("Cost")
//    .WithMessage("Quantity must be more than 0!");



//RuleFor(p => p.Book.Quantity)
//    .GreaterThanOrEqualTo(1)
//    .WithName("Cost")
//    .WithMessage("Quantity must be more than 0!");
using Domain.Entities.Abstractions;

namespace Domain.Entities;

public class Book : Document
{
    public string Name { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public decimal? Quantity { get; set; }
    public int Isbn { get; set; }
}
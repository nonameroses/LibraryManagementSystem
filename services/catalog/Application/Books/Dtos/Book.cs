namespace Application.Books.Dtos;
public class Book
{
    public string Name { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public bool Active { get; private set; } = true;
    public int Isbn { get; set; }
    public decimal? Quantity { get; set; }
}

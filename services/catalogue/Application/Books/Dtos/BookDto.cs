namespace Application.Books.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Isbn { get; set; }
    public decimal? Quantity { get; set; }
}
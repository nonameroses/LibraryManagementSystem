using Application.Books.Dtos;
using Domain.Entities;
using Mapster;

namespace Application.Books.Mappings;

public sealed class BookMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookDto>();
    }
}
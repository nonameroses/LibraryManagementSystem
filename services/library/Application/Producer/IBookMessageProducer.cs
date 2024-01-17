using Domain.Entities;

namespace Application.Producer;
public interface IBookMessageProducer
{
    bool ProduceBookMessage(Book book);
}

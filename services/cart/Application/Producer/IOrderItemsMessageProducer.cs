namespace Application.Producer;
public interface IOrderItemsMessageProducer
{
    bool ProduceItemsMessage(IEnumerable<string> items);
}

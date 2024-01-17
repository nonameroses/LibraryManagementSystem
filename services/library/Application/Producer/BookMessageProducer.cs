using System.Text;
using Microsoft.AspNetCore.Connections;


using Domain.Entities;
using Infrastructure.Data;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Application.Producer;
public class BookMessageProducer
{
    private readonly RabbitMqConfigurationSettings _config;

    public BookMessageProducer(RabbitMqConfigurationSettings config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    public bool SendProductOffer(Book book)
    {
        var RabbitMQServer = _config.RabbitURL;
        var RabbitMQUserName = _config.Username;
        var RabbutMQPassword = _config.Password;

        try
        {
            var factory = new ConnectionFactory()
            { 
                HostName = RabbitMQServer, 
                UserName = RabbitMQUserName, 
                Password = RabbutMQPassword
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Direct Exchange Details like name and type of exchange
                channel.ExchangeDeclare(_config.ExchangeName, ExchangeType.Direct);

                //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
                channel.QueueDeclare(_config.QueueName, false, false, false, null);

                //Bind Queue with Exhange and routing details
                channel.QueueBind(_config.QueueName, _config.ExchangeName, _config.RoutingName, null);

                //Seriliaze object using Newtonsoft library
                string productDetail = JsonConvert.SerializeObject(book);
                var body = Encoding.UTF8.GetBytes(productDetail);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //publish msg
                channel.BasicPublish(_config.ExchangeName, _config.RoutingName, properties,body);

                return true;
            }
        }

        catch (Exception)
        {
        }
        return false;
    }
}

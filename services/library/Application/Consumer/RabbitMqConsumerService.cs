using Application.Producer;
using Domain.Entities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Application.Consumer;

public class RabbitMqConsumerService : IRabbitMqConsumerService
{
    private readonly RabbitMqConfigurationSettings _config;

    public RabbitMqConsumerService(IOptions<RabbitMqConfigurationSettings> config)
    {
        _config = config.Value;
    }

    public string ConsumeMessage()
    {
        var RabbitMQServer = _config.RabbitURL;
        var RabbitMQUserName = _config.Username;
        var RabbutMQPassword = _config.Password;

        var message = "";

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
                channel.QueueBind(_config.QueueName, _config.ExchangeName, _config.RouteKey, null);
                // Consume only 1 message at time
                channel.BasicQos(0, 1, false);



                var consumer = new EventingBasicConsumer(channel);

                // Seriliaze object using Newtonsoft library
                consumer.Received += (sender, args) =>
                {
                    var body = args.Body.ToArray();

                    message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Message Received: {message}");
                    channel.BasicAck(args.DeliveryTag, false);
                };

                string consumerTag = channel.BasicConsume(_config.QueueName, false, consumer);

                channel.BasicCancel(consumerTag);
                channel.Close();
                connection.Close();

                return message;
            }
        }

        catch (Exception)
        {
        }

        return message;
    }
}

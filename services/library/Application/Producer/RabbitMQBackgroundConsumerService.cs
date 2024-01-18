using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Application.Producer;
    public class RabbitMqBackgroundConsumerService : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IServiceScopeFactory serviceScopeFactory;

        public RabbitMqBackgroundConsumerService(IServiceScopeFactory _serviceScopeFactory)
        {
            serviceScopeFactory = _serviceScopeFactory;
            InitRabbitMq();
        }

        private void InitRabbitMq()
        {
            
            _connection.ConnectionShutdown += RabbitMq_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

                // acknowledge the received message
                _channel.BasicAck(ea.DeliveryTag, false);

                //Deserilized Message
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                //var productDetails = JsonConvert.DeserializeObject<ProductOfferDetail>(message);

                //Stored Offer Details into the Database
                //using (var scope = serviceScopeFactory.CreateScope())
                //{
                //    //var _dbContext = scope.ServiceProvider.GetRequiredService<DbContextClass>();
                //    var result = _dbContext.ProductOffers.Add(productDetails);
                //    _dbContext.SaveChanges();
                //}

            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            //_channel.BasicConsume(StaticConfigurationManager.AppSetting["RabbitMqSettings:QueueName"], false, consumer);
            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMq_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }

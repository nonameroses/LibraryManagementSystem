using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;
public class RabbitMqConfigurationSettings
{
    public string ExchangeName { get; set; }
    public string direct { get; set; }
    public string QueueName { get; set; }
    public string RoutingName { get; set; }
    public string RabbitURL { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public static IConfiguration AppSetting
    {
        get;
    }
    static RabbitMqConfigurationSettings()
    {
        AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    }
}

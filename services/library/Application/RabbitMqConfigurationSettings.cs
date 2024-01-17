using Microsoft.Extensions.Configuration;

namespace Application;
public class RabbitMqConfigurationSettings : IRabbitMqConfigurationSettings
{
    public string? ExchangeName { get; set; }
    public string? direct { get; set; }
    public string? QueueName { get; set; }
    public string? RouteKey { get; set; }
    public string? RabbitURL { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public static IConfiguration AppSetting
    {
        get;
    }
    static RabbitMqConfigurationSettings()
    {
       // AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    }
}

public interface IRabbitMqConfigurationSettings
{
     string ExchangeName { get; set; }
     string direct { get; set; }
     string QueueName { get; set; }
     string RouteKey { get; set; }
     string RabbitURL { get; set; }
     string Username { get; set; }
     string Password { get; set; }

}
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;
public class RabbitMqConfigurationSettings
{
    public string OfferExchange { get; set; }
    public string direct { get; set; }
    public string offer_queue { get; set; }
    public string offer_route { get; set; }

    public static IConfiguration AppSetting
    {
        get;
    }
    static RabbitMqConfigurationSettings()
    {
        AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    }
}

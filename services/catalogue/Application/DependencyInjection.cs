using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
    
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        //services.AddMediatR(cfg => {
        //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //});

        return services;

    }
}
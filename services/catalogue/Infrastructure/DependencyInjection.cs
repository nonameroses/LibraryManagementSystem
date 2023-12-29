using Infrastructure.Data.Repositories;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this WebApplicationBuilder builder)
    {
        var applicationAssembly = typeof(CatalogueApplication).Assembly;
        builder.Services.Configure<MongoOptions>(builder.Configuration.GetSection("MongoDB"));
        builder.Services.AddSingleton<MongoDbContext>();
        builder.Services.AddTransient(typeof(IMongoRepository<,>), typeof(MongoRepository<,>));
        builder.Services.AddTransient<IBookRepository, BookRepository>();


        //services.AddMediatR(cfg => {
        //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //});

        return services;

    }
    //public static void UseCatalogInfrastructure(this WebApplication app)
    //{
    //    app.UseInfrastructure(app.Environment);
    //}
}
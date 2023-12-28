using System.Configuration;
using Domain.Entities;
using FluentAssertions.Common;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Mapster;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
//Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
//typeAdapterConfig.Scan(applicationAssembly);

//builder.Services.AddMapster
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
//builder.Services.AddSingleton<IMongoDbOptions>(serviceProvider =>
//    serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value);
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbContext>();
//builder.Services.AddMongoDbContext<MongoDbContext>(builder.Configuration);
//builder.Services.AddTransient<MongoDbContext>(new MongoDbContext());
//builder.Services.AddTransient(typeof(IMongoRepository<,>), typeof(MongoRepository<,>));


builder.Services.AddTransient(typeof(IMongoRepository<,>), typeof(MongoRepository<,>));

builder.Services.AddTransient<IBookRepository, BookRepository>();

//builder.Services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDbOptions"));

//builder.Services.AddSingleton<MongoDbOptions>(serviceProvider =>
//    serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();

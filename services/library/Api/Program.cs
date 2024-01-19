using Application;
using Application.Books.Features.Commands;
using Application.Common.Behaviours;
using Application.Consumer;
using Application.Producer;
using FluentValidation;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);


// Dependenc
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblies(assembly);
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    });
}

builder.Services.AddScoped<IBookMessageProducer, BookMessageProducer>();
builder.Services.AddScoped<IRabbitMqConsumerService, RabbitMqConsumerService>();
//builder.Services.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<RabbitMqConfigurationSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddValidatorsFromAssemblyContaining<AddBook.Validator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

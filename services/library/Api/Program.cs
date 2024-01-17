using Application;
using Application.Books.Features.Queries;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using FluentValidation.AspNetCore;
using Application.Books.Features.Commands;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behaviours;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Application.Producer;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddValidatorsFromAssembly(applicationAssembly);#

//builder.Services.AddValidatorsFromAssemblyContaining<AddBook.Validator>(); // register validators
//builder.Services.AddValidatorsFromAssemblyContaining<GetBook.Validator>(); // register validators
//builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
//builder.Services.AddFluentValidationClientsideAdapters(); // for client side



//builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddBook.Validator>());
//builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetBook.Validator>());

// Dependency Injection

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblies(assembly);
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    });
}

//builder.Services.AddHostedService<RabbitMQBackgroundConsumerService>();
builder.Services.AddScoped<IBookMessageProducer, BookMessageProducer>();
//builder.Services.
//AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<RabbitMqConfigurationSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));





builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddValidatorsFromAssemblyContaining<AddBook.Validator>();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.BusinessLogic.Logic;
using MyBlazor.DataAccess.Context;
using MyBlazor.DataAccess.Interfaces;
using MyBlazor.DataAccess.Repo;
using MyBlazor.Server;
using MyBlazor.Server.RabbitMQPublisher;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var mappingConfig = new MapperConfiguration(mc =>
{
    var mapping = new Mapping();
    mc.AddProfile(mapping);
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IGameRepo, GameRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<ICategoryLogic, CategoryLogic>();
builder.Services.AddScoped<IGameLogic, GameLogic>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();
builder.Services.Configure<Connection>(configuration.GetSection("connection"));
builder.Services.AddSingleton<MyContext>();
builder.Services.AddSingleton<OrderPublisher>();

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        var jsonConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(jsonConverter);
    }
);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


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
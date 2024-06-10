using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using MyBlazor.DataAccess.Context;
using MyBlazor.DataAccess.Interfaces;
using MyBlazor.DataAccess.Repo;
using RabbitMQ.Consumer;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;
builder.Services.Configure<Connection>(configuration.GetSection("connection"));
builder.Services.AddSingleton<MyContext>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddHostedService<Worker>();


var host = builder.Build();

host.Run();
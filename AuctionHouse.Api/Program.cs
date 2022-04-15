using AuctionHouse.Application;
using AuctionHouse.Application.Plugins;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Load plug-ins
var infraPlugin = builder.Configuration["Plugins:Infrastructure"];
var infraAssembly = InfrastructurePluginLoadContext.LoadPlugin(infraPlugin);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration, infraAssembly);

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

app.Services.UseInfrastructureServices(builder.Configuration, infraAssembly);

app.Run();

using AuctionHouse.Application;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var directoy = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration, Assembly.LoadFile($"{directoy}/AuctionHouse.Infrastructure.dll"));

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

app.Services.UseInfrastructureServices(builder.Configuration, Assembly.LoadFile($"{directoy}/AuctionHouse.Infrastructure.dll"));

app.Run();

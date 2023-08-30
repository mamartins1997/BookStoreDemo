using BookStoreApi.Application;
using BookStoreApi.Host.Configuration;
using BookStoreApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.ConfigureSwaggerServices();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.ConfigureSwagger();

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

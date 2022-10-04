using ApiGateway.Services;
using Confluent.SchemaRegistry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var schemaRegistruConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081",
    EnableSslCertificateVerification = false,
};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProducerServices,ProducerServices>();
builder.Services.AddSingleton(schemaRegistruConfig);

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

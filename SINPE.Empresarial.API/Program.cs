using Microsoft.EntityFrameworkCore;
using SINPE.Empresarial.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("SinpeDb");
builder.Services.AddScoped<SINPE_Empresarial_DB>(_ => new SINPE_Empresarial_DB(conn));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SINPE.Empresarial.API v1");
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

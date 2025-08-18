using SINPE.Empresarial.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("WebOrigin", p => p
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var conn = builder.Configuration.GetConnectionString("SinpeDb");
builder.Services.AddScoped(_ => new SINPE_Empresarial_DB(conn));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SINPE.Empresarial.API v1");
});

app.UseHttpsRedirection();

app.UseCors("WebOrigin");

app.MapControllers();

app.Run();

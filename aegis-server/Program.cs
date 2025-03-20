using Microsoft.EntityFrameworkCore;
using aegis_server.Hubs;
using aegis_server.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Permitir acesso da origem do frontend
              .AllowAnyMethod() // Permitir qualquer método (GET, POST, etc.)
              .AllowAnyHeader() // Permitir qualquer cabeçalho
              .AllowCredentials(); // Permitir credenciais
    });
});

// Configuração do banco
builder.Services.AddDbContext<AegisDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowLocalhost");
app.MapHub<GameHub>("/gameHub");

app.Run();

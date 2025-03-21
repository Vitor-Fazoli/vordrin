using System.Text;
using aegis_server.Data;
using aegis_server.Hubs;
using aegis_server.Services;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

DotEnv.Load();

var env = DotEnv.Read();

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(env["SECRET"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<AegisDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PlayerService>();

builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowLocalhost");
app.MapHub<GameHub>("/gameHub");

app.Run();
using System.Text;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using dotenv.net;
using Infrastructure;
using Infrastructure.Config;
using Infrastructure.Dtos;
using Infrastructure.Hubs;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


DotEnv.Load();

var env = DotEnv.Read();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

var authSettingsSection = builder.Configuration.GetSection("AuthSettings");
builder.Services.Configure<TokenSettings>(authSettingsSection);

var authSettings = authSettingsSection.Get<TokenSettings>();
var key = Encoding.ASCII.GetBytes(authSettings!.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = authSettings.Issuer,
        ValidAudience = authSettings.Audience
    };
});

builder.Services.AddScoped<ITokenService, Token>();
builder.Services.AddScoped<IUserRepository<UserDto>, UserRepository>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Porta do Vite
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.MapHub<GameHub>("/gameHub");
app.UseCors("CorsPolicy");

app.Run();
using System.Text;
using Gossip.Endpoints;
using Gossip.Models;
using Gossip.Services;
using Gossip.Services.JWT;
using Gossip.Services.Password;
using Gossip.Services.Profiles;
using Gossip.UseCases;
using Gossip.UseCases.DeletePost;
using Gossip.UseCases.GetProfileData;
using Gossip.UseCases.Login;
using Gossip.UseCases.PublishPost;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GossipDbContext>(options =>
{
    var sqlConn = Environment.GetEnvironmentVariable("SQL_CONNECTION");
    options.UseSqlServer(sqlConn);
});

builder.Services.AddTransient<IPasswordService, PBKDF2PasswordService>();
builder.Services.AddTransient<IProfilesService, EFProfileService>();
builder.Services.AddSingleton<IJWTService, JWTService>();

builder.Services.AddTransient<LoginUseCase>();
builder.Services.AddTransient<DeletePostUseCase>();
builder.Services.AddTransient<CreateProfileUseCase>();
builder.Services.AddTransient<PublishPostUseCase>();
builder.Services.AddTransient<GetProfileDataUseCase>();

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);
var key = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "fofoquinha-app",
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = key,
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureAuthEndpoints();
app.ConfigurePostEndpoints();
app.ConfigureProfileEndpoints();

app.Run();
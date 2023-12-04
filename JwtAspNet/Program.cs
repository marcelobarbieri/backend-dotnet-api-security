using JwtAspNet;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet(
    pattern: "/login",
    handler: (TokenService service)
        => 
        {
            var user = new User(
                Id:1,
                Name:"André Baltieri",
                Email:"xyz@balta.io",
                Image:"https://balta.io/",
                Password:"xyz",
                Roles:new[] { "student", "premium" });

            return service.Create(user);
        });

app
    .MapGet(
        pattern: "/restrito",
        handler: () => "Você tem acesso!")
    .RequireAuthorization();

app.Run();
 
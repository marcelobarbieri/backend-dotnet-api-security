using JwtAspNet;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
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
builder.Services
    .AddAuthorization(options =>
    {
        options.AddPolicy("Admin", p => p.RequireRole("admin"));
    });

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
        handler: (ClaimsPrincipal user) =>
            new
            {
                id = user.Claims.FirstOrDefault(x=> x.Type == "id").Value,
                name = user.Claims.FirstOrDefault(x=> x.Type == ClaimTypes.Name).Value,
                email = user.Claims.FirstOrDefault(x=> x.Type == ClaimTypes.Email).Value,
                givenName = user.Claims.FirstOrDefault(x=> x.Type == ClaimTypes.GivenName).Value,
                image = user.Claims.FirstOrDefault(x=> x.Type == "image").Value
            })
    .RequireAuthorization();

app
    .MapGet(
        pattern: "/admin",
        handler: () => "Você tem acesso!")
    .RequireAuthorization("Admin");


app.Run();
 
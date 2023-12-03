using JwtAspNet.Models;
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet(
    pattern: "/",
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

app.Run();

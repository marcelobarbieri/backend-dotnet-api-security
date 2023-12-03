using JwtAspNet.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGet(
    pattern: "/",
    handler: (
        TokenService service,
        ClaimsPrincipal user)
        => new
        {
            Token = service.Create(),
            User = user.Identity.Name
            //User = user.Identity.IsAuthenticated
            //User = user.IsInRole
        });

app.Run();

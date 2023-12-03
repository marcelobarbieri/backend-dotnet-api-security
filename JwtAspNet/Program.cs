using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGet(
    pattern: "/",
    handler: (TokenService service)
        => service.Create());

app.Run();

using JwtStore.Core.Contexts.AccountContext.UseCases.Create;
using MediatR;

namespace JwtStore.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository, // Interface
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Repository>(); // Implementação

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService, // Interface
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Service>(); // Implementação

        #endregion

        #region Authenticate

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository, // Interface
            JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository>(); // Implementação

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/users",async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSucess
                ? Results.Created($"api/v1/users/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion

        #region Authenticate

        app.MapPost("api/v1/authenticate", async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
            IRequestHandler<
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            if (!result.IsSucess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            result.Data.Token = JwtExtension.Generate(result.Data);

            return Results.Ok(result);
        });

        #endregion
    }
}

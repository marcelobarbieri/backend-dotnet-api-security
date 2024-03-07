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
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {

    }
}

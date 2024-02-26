using Flunt.Notifications;

namespace JwtStore.Core.Contexts.SharedContext.UseCases;

public abstract class Response
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; } = 400;
    public IEnumerable<Notification>? Notifications { get; set; }

    public bool IsSucess => Status is >= 200 and <= 299;
}

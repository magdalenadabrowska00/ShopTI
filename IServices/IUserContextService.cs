using System.Security.Claims;

namespace ShopTI.IServices
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}

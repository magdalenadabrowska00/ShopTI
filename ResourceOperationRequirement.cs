using Microsoft.AspNetCore.Authorization;
using ShopTI.Enums;

namespace ShopTI
{
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; }
        public ResourceOperationRequirement(ResourceOperation operation)
        {
            ResourceOperation = operation;
        }
    }
}

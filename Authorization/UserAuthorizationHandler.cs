using Microsoft.AspNetCore.Authorization;
using ShopTI.Entities;
using System.Security.Claims;

namespace ShopTI.Authorization
{
    public class UserAuthorizationHandler : AuthorizationHandler<ResourceOperationRequirement, Product>
    {
        private readonly ShopDbContext _dbContext;
        public UserAuthorizationHandler(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            ResourceOperationRequirement requirement, 
            Product resource)
        {
            if (requirement.ResourceOperation == Enums.ResourceOperation.Create ||
                requirement.ResourceOperation == Enums.ResourceOperation.Update ||
                requirement.ResourceOperation == Enums.ResourceOperation.Read ||
                requirement.ResourceOperation == Enums.ResourceOperation.Delete )
            {
                context.Succeed(requirement);
            }

            if (FindAdminRole(context) is not null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private User FindAdminRole(AuthorizationHandlerContext context)
        {
            var userId = int.Parse(context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
        }
    }
}

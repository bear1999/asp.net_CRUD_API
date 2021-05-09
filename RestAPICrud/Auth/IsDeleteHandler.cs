using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RestAPICrud.Servcies.Employee;
using System;
using System.Threading.Tasks;

namespace RestAPICrud.Auth
{
    public class IsDeleteHandler : AuthorizationHandler<IsDelete>
    {
        public readonly IEmployeeService empService;
        public readonly IHttpContextAccessor httpContextAccessor;
        public IsDeleteHandler(IEmployeeService _empService, IHttpContextAccessor _httpContextAccessor)
        {
            empService = _empService;
            httpContextAccessor = _httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsDelete requirement)
        {
            if (!context.User.HasClaim(x => x.Type == "Id"))
            {
                return Task.CompletedTask;
            }
            var UserId = context.User.FindFirst(x => x.Type == "Id");
            var CheckIsDelete = empService.CheckIsDelete(Guid.Parse(UserId.Value));

            if (CheckIsDelete.Result == requirement.CheckIsDelete)
            {
                context.Succeed(requirement);
            }
            //else
            //{
            //    httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    context.Succeed(requirement);
            //}
            return Task.CompletedTask;
        }
    }
}

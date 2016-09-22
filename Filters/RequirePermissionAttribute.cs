namespace EmployeeDirectory.Filters
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class RequirePermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public Permission Permission { get; }

        public RequirePermissionAttribute(Permission permission)
        {
            Permission = permission;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizer = context.HttpContext.RequestServices.GetService<IPermissionAuthorizer>();

            var hasPermission = await authorizer.HasPermission(context.HttpContext.User.Identity.Name, Permission);

            if (!hasPermission)
            {
                context.Result = new ContentResult
                {
                    Content = "You don't have access!!",
                };
            }
        }
    }
}
namespace EmployeeDirectory.TagHelpers
{
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Services;

    [HtmlTargetElement(Attributes = PermissionAttributeName)]
    public class SecureTagHelper : TagHelper
    {
        private readonly IPermissionAuthorizer authorizer;
        private const string PermissionAttributeName = "asp-permission";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(PermissionAttributeName)]
        public Permission Permission { get; set; }

        public SecureTagHelper(IPermissionAuthorizer authorizer) 
        {
            this.authorizer = authorizer;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var username = ViewContext.HttpContext.User.Identity.Name;

            if (!await authorizer.HasPermission(username, Permission))
            {
                output.SuppressOutput();
            }
        }
    }
}
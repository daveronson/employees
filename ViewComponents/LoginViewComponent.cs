namespace EmployeeDirectory.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;

    public class LoginViewComponent : ViewComponent
    {
        private readonly DirectoryContext _dbContext;

        public LoginViewComponent(DirectoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new LoginViewComponentModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated
            };

            if (model.IsAuthenticated)
            {
                var user = await _dbContext.Employee
                    .Where(e => e.Username == User.Identity.Name)
                    .SingleOrDefaultAsync();

                model.Name = user?.FirstName ?? User.Identity.Name;
            }

            return View(model);
        }
    }
}
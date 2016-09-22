namespace EmployeeDirectory.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;
    using AutoMapper;
    using Domain;
    using Filters;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeController : BaseController
    {
        private readonly DirectoryContext _dbContext;

        public EmployeeController(DirectoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(Office? office)
        {
            var model = await _dbContext.Employee
                .Where(e => !office.HasValue || e.Office == office)
                .ProjectTo<EmployeeIndexModel>()
                .ToListAsync();

            return View(model);
        }

        [RequirePermission(Permission.EditEmployees)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _dbContext
                .Employee
                .Where(e => e.Id == id)
                .ProjectTo<EmployeeEditModel>()
                .SingleOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        [RequirePermission(Permission.EditEmployees)]
        public async Task<IActionResult> Edit(EmployeeEditModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var employee = await _dbContext.Employee
                .SingleOrDefaultAsync(e => e.Id == model.Id);

            Mapper.Map(model, employee);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [RequirePermission(Permission.ManageSecurity)]
        public async Task<IActionResult> EditRoles(int id)
        {
            //Create the model for the Employee using their id
            var model = await CreateEmployeeEditRoleModel(id);

            // Get a list of Roles that are assigned to the employee
            var employeeRoles = await _dbContext
                .EmployeeRole
                .Where(er => er.Employee.Id == id)
                .ToListAsync();

            //Loop through the roles in the model and if the employee belongs to a role
            //then set the IsSelected boolean to true.
            foreach (var role in model.Roles)
            {
                role.IsSelected = employeeRoles.Any(er => er.RoleId == role.Id);
            }


            return View(model);
        }

        private async Task<EmployeeEditRoleModel> CreateEmployeeEditRoleModel(int id)
        {
            //Get all roles and put in a list
            var allRoles = await _dbContext.Role
                .Select(role => new EmployeeEditRoleModel.EmployeeRoleModel
                {
                    Id = role.Id,
                    Name = role.Name
                })
                .ToListAsync();

            var model = new EmployeeEditRoleModel
            {
                Id = id, //employee id
                Roles = allRoles //list of all roles, not just the ones assigned to the employee
            };
            return model;
        }

        [HttpPost]
        [RequirePermission(Permission.ManageSecurity)]
        public async Task<IActionResult> EditRoles(EmployeeEditRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                //The model was not valid
                //Create the model from scratch again for the Employee using their id
                //So we can show the edit page again.
                model = await CreateEmployeeEditRoleModel(model.Id);

                return View(model);
            }

            //Delete all roles for the employee
            _dbContext.EmployeeRole
                .RemoveRange(_dbContext
                    .EmployeeRole
                    .Where(er => er.Employee.Id == model.Id));

            _dbContext.SaveChanges();

            //Get the employee based on their id
            var employee = await _dbContext.Employee
                .SingleOrDefaultAsync(e => e.Id == model.Id);

            //Loop through the roles that were selected / check on the EditRoles View
            foreach (var roleId in model.SelectedRoles)
            {
                var employeeRole = new EmployeeRole
                {
                    EmployeeId = employee.Id,
                    RoleId =  roleId,
                    Employee = employee,
                    Role = await _dbContext.Role.SingleOrDefaultAsync(r => r.Id == roleId)
                };
                _dbContext.EmployeeRole.Add(employeeRole);
            }

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _dbContext.Employee
                .Where(e => e.Id == id)
                .ProjectTo<EmployeeDetailsModel>()
                .SingleOrDefaultAsync();

            return View(model);
        }
    }
}
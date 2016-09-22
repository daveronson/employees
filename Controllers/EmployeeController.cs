namespace EmployeeDirectory.Controllers
{
    using System;
    using System.Collections.Generic;
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
            var model = await CreateEmployeeEditRoleModel(id);

            var employeeRoles = await _dbContext
                .EmployeeRole
                .Where(er => er.Employee.Id == id)
                .ToListAsync();

            foreach (var role in model.Roles)
            {
                role.IsSelected = employeeRoles.Any(er => er.Role.Id == role.Id);
            }


            return View(model);
        }

        private async Task<EmployeeEditRoleModel> CreateEmployeeEditRoleModel(int id)
        {
            var allRoles = await _dbContext.Role
                .Select(role => new EmployeeEditRoleModel.EmployeeRoleModel
                {
                    Id = role.Id,
                    Name = role.Name
                })
                .ToListAsync();

            var model = new EmployeeEditRoleModel
            {
                Id = id,
                Roles = allRoles
            };
            return model;
        }

        [HttpPost]
        [RequirePermission(Permission.ManageSecurity)]
        public async Task<IActionResult> EditRoles(EmployeeEditRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await CreateEmployeeEditRoleModel(model.Id);

                return View(model);
            }

            _dbContext.EmployeeRole
                .RemoveRange(_dbContext
                    .EmployeeRole
                    .Where(er => er.Employee.Id == model.Id));

            var employee = await _dbContext.Employee
                .SingleOrDefaultAsync(e => e.Id == model.Id);

            foreach (var roleId in model.SelectedRoles)
            {
                var employeeRole = new EmployeeRole
                {
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
namespace EmployeeDirectory.Features.Employee
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditRoles
    {
        public class Query : IAsyncRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IValidatableObject, IAsyncRequest
        {
            public int Id { get; set; }

            public List<int> SelectedRoles { get; set; } = new List<int>();
            public List<EmployeeRoleModel> Roles { get; set; }

            public class EmployeeRoleModel
            {
                public bool IsSelected { get; set; }
                public int Id { get; set; }
                public string Name { get; set; }
            }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (!SelectedRoles.Any())
                    yield return new ValidationResult("An employee must belong to at least one role.");
            }
        }

        public class CommandFactory
        {
            private readonly DirectoryContext _dbContext;

            public CommandFactory(DirectoryContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Command> CreateModel(int id)
            {
                var allRoles = await _dbContext.Role
                    .Select(role => new Command.EmployeeRoleModel
                    {
                        Id = role.Id,
                        Name = role.Name
                    })
                    .ToListAsync();

                var model = new Command
                {
                    Id = id, //employee id 
                    Roles = allRoles //list of all roles, not just the ones assigned to the employee 
            
                };
                return model;
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Command>
        {
            private readonly DirectoryContext _dbContext;
            private readonly CommandFactory _modelFactory;

            public QueryHandler(DirectoryContext dbContext,
                CommandFactory modelFactory)
            {
                _dbContext = dbContext;
                _modelFactory = modelFactory;
            }

            public async Task<Command> Handle(Query message)
            {
                //Create the model for the Employee using their id 
                var model = await _modelFactory.CreateModel(message.Id);

                // Get a list of Roles that are assigned to the employee 
                var employeeRoles = await _dbContext
                    .EmployeeRole
                    .Where(er => er.Employee.Id == message.Id)
                    .ToListAsync();

                //Loop through the roles in the model and if the employee belongs to a role 
                //then set the IsSelected boolean to true. 
                foreach (var role in model.Roles)
                {
                    role.IsSelected = employeeRoles.Any(er => er.RoleId == role.Id);
                }

                return model;
            }


        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            private readonly DirectoryContext _dbContext;

            public CommandHandler(DirectoryContext dbContext)
            {
                _dbContext = dbContext;
            }

            protected override async Task HandleCore(Command message)
            {
                //Delete all roles for the employee
                _dbContext.EmployeeRole
                    .RemoveRange(_dbContext
                        .EmployeeRole
                        .Where(er => er.Employee.Id == message.Id));

                await _dbContext.SaveChangesAsync();

                //Get the employee based on their id
                var employee = await _dbContext.Employee
                    .SingleOrDefaultAsync(e => e.Id == message.Id);

                foreach (var roleId in message.SelectedRoles)
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

                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
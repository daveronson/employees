namespace EmployeeDirectory.Features.Employee
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Domain;
    using Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class Details
    {
        public class Query : IAsyncRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Model
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Title { get; set; }
            public Office Office { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public List<EmployeeRoleModel> EmployeeRoles { get; set; }

            public class EmployeeRoleModel
            {
                public string RoleName { get; set; }
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Model>
        {
            private readonly DirectoryContext _dbContext;

            public QueryHandler(DirectoryContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Model> Handle(Query message)
            {
                var model = await _dbContext.Employee
                    .Where(e => e.Id == message.Id)
                    .ProjectTo<Model>()
                    .SingleOrDefaultAsync();

                return model;
            }
        }
    }

}
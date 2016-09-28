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

    public class Index
    {
        public class Model
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Title { get; set; }
            public Office Office { get; set; }
        }

        public class Query : IAsyncRequest<List<Model>>
        {
            public Office? Office { get; set; }
        }

        public class QueryHandler
            : IAsyncRequestHandler<Query, List<Model>>
        {
            private readonly DirectoryContext _dbContext;

            public QueryHandler(DirectoryContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<List<Model>> Handle(Query message)
            {
                var model = await _dbContext.Employee
                    .Where(e => !message.Office.HasValue || e.Office == message.Office)
                    .ProjectTo<Model>()
                    .ToListAsync();

                return model;
            }
        }
    }

}
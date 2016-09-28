namespace EmployeeDirectory.Features.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class Get
    {
        public class Query : IAsyncRequest<List<RepresentationModel>>
        {
        }

        public class QueryHandler : IAsyncRequestHandler<Query, List<RepresentationModel>>
        {
            private readonly DirectoryContext _dbContext;

            public QueryHandler(DirectoryContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<List<RepresentationModel>> Handle(Query message)
            {
                return await _dbContext
                    .Employee
                    .ProjectTo<RepresentationModel>()
                    .ToListAsync();
            }
        }
    }
}
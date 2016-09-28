namespace EmployeeDirectory.Features.Employees
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class Details
    {
        public class Query : IAsyncRequest<RepresentationModel>
        {
            public int Id { get; set; }
        }

        public class QueryHandler :
            IAsyncRequestHandler<Query, RepresentationModel>
        {
            private readonly DirectoryContext _dbContext;

            public QueryHandler(DirectoryContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<RepresentationModel> Handle(Query message)
            {
                var model = await _dbContext
                    .Employee
                    .Where(e => e.Id == message.Id)
                    .ProjectTo<RepresentationModel>()
                    .SingleOrDefaultAsync();

                return model;
            }
        }
    }
}
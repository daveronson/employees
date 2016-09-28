namespace EmployeeDirectory.Features.Employee
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Domain;
    using Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class Edit
    {
        public class Query : IAsyncRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IAsyncRequest
        {
            public int Id { get; set; }

            [Required]
            [MaxLength(100)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(100)]
            public string LastName { get; set; }

            [MaxLength(100)]
            public string Title { get; set; }

            public Office Office { get; set; }

            [EmailAddress]
            [MaxLength(255)]
            public string Email { get; set; }

            [Phone]
            [MaxLength(50)]
            public string PhoneNumber { get; set; }
        }

        public class QueryHandler
            : IAsyncRequestHandler<Query, Command>
        {
            private readonly DirectoryContext _dbContext;

            public QueryHandler(DirectoryContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Command> Handle(Query message)
            {
                var model = await _dbContext
                    .Employee
                    .Where(e => e.Id == message.Id)
                    .ProjectTo<Command>()
                    .SingleOrDefaultAsync();

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
                var employee = await _dbContext.Employee
                    .SingleOrDefaultAsync(e => e.Id == message.Id);

                Mapper.Map(message, employee);

                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
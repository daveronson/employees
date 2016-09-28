namespace EmployeeDirectory.Features.Employees
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class ChangeEmail
    {
        public class Command : IAsyncRequest
        {
            public int Id { get; set; }
            public string Email { get; set; }
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
                var employee = await _dbContext
                    .Employee
                    .SingleOrDefaultAsync(e => e.Id == message.Id);

                employee.Email = message.Email;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
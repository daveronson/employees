namespace EmployeeDirectory.Services
{
    //using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public class DbContextPermissionAuthorizer : IPermissionAuthorizer
    {
        private readonly DirectoryContext db;

        public DbContextPermissionAuthorizer(DirectoryContext db)
        {
            this.db = db;
        }

        public async Task<bool> HasPermission(string username, Permission permission)
        {
            var hasPermission = await db.EmployeeRole
                .Where(er => er.Employee.Username == username)
                .Where(er => er.Role.RolePermissions.Any(rp => rp.Permission == permission))
                .AnyAsync();

            return hasPermission;
        }
    }
}
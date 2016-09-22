using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using EmployeeDirectory.Domain;


namespace EmployeeDirectory.Infrastructure
{
    public class DirectoryContext : DbContext
    {
        public DirectoryContext(DbContextOptions<DirectoryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            //modelbuilder.Entity<FamilyFriend>().HasKey(x => new { x.FamilyID, x.FriendID });
        }  
        public DbSet<Employee> Employee {get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<EmployeeRole> EmployeeRole { get; set; }
    }
    public static class Extensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }

}

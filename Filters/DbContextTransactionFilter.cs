namespace EmployeeDirectory.Filters
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class DbContextTransactionFilter : IActionFilter
    {
        private readonly DirectoryContext db;

        public DbContextTransactionFilter(DirectoryContext db)
        {
            this.db = db;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //db.Database.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                //db.Database.CommitTransaction();
                return;
            }

            try
            {
                //db.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                //db.Database.CommitTransaction();

                throw ex;
            }
        }
    }
}
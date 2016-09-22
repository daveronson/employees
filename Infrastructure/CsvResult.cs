namespace EmployeeDirectory.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using System.Reflection;

    public class CsvActionResult<T> : FileResult
    {
        private readonly T[] _items;
        public CsvActionResult(T[] items) : base("text/csv")
        {
            _items = items;
        }
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var properties = typeof(T).GetProperties();
            var headers = string.Join(",", properties.Select(pi => pi.Name));
            await context.HttpContext.Response.WriteAsync(headers + Environment.NewLine);
            foreach (var item in _items)
            {
                var values = string.Join(",", properties.Select(pi => pi.GetValue(item)));
                await context.HttpContext.Response.WriteAsync(values + Environment.NewLine);
            }
        }
    }
}
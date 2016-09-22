namespace EmployeeDirectory.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : Controller
    {
        protected CsvActionResult<T> Csv<T>(T[] items)
        {
            return new CsvActionResult<T>(items);
        }
    }
}
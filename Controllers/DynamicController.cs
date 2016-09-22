using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDirectory.Controllers
{
    using Infrastructure;

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class DynamicController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index(Person model)
        {
            return Content($"{model.FirstName} {model.LastName}");
        }

        public IActionResult List(Person[] model)
        {
            return Csv(model);
        }
    }
}

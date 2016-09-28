// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDirectory.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Features.Employees;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.SendAsync(new Get.Query()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _mediator.SendAsync(new Details.Query {Id = id});
            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost("{id:int}/changeemail")]
        public async Task<IActionResult> ChangeEmail(
            int id,
            [FromBody] ChangeEmail.Command model)
        {
            model.Id = id;

            await _mediator.SendAsync(model);

            return NoContent();
        }
    }
}
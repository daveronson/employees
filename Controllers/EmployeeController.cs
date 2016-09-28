namespace EmployeeDirectory.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;
    using Domain;
    using Features.Employee;
    using Filters;
    using MediatR;

    public class EmployeeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly EditRoles.CommandFactory _modelFactory;

        public EmployeeController(IMediator mediator,
            EditRoles.CommandFactory modelFactory)
        {
            _mediator = mediator;
            _modelFactory = modelFactory;
        }

        public async Task<IActionResult> Index(Index.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        [RequirePermission(Permission.EditEmployees)]
        public async Task<IActionResult> Edit(Edit.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        [HttpPost]
        [RequirePermission(Permission.EditEmployees)]
        public async Task<IActionResult> Edit(Edit.Command model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _mediator.SendAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [RequirePermission(Permission.ManageSecurity)]
        public async Task<IActionResult> EditRoles(EditRoles.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        [HttpPost]
        [RequirePermission(Permission.ManageSecurity)]
        public async Task<IActionResult> EditRoles(EditRoles.Command model)
        {
            if (!ModelState.IsValid)
            {
                model = await _modelFactory.CreateModel(model.Id);

                return View(model);
            }

            await _mediator.SendAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Details.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }
    }
}
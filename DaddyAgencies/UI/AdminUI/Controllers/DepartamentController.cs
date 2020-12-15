using AutoMapper;
using DaddyAgencies.Application.Features.Departament;
using DaddyAgencies.Application.Features.Region;
using DaddyAgencies.Application.Models;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AdminUI.Models;
using DaddyAgencies.Application.Helpers;

namespace AdminUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartamentController : BaseController
    {
        public DepartamentController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> Create() {

            var regions = await Mediator.SendRequest(new GetRegions());
            if (regions.IsSuccess)
                ViewBag.Regions = regions.Collection;
            return View("Form", new SelectedDeparmentModel());
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var regions = await Mediator.SendRequest(new GetRegions());
            if (regions.IsSuccess)
                ViewBag.Regions = regions.Collection;
            var result = await Mediator.SendRequest(new FindDepartament(id));
            return View("Form", Mapper.Map<DepartamentModel>(result.Payload));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Models.DepartamentModel departament)
        {
            if (ModelState.IsValid)
            {
                var request = Mapper.Map<SaveDepartament>(departament);
                var response = await Mediator.SendRequest(request);
                if (response.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            return View("Form");
        }

        [HttpPost] 
        public async Task<ActionResult> Remove(Guid id)
        {
            var deleteDepartamentRequest = new DeleteDepartament(id);
            var deleteDepartamentResponse = await Mediator.SendRequest(deleteDepartamentRequest);
            return Json(deleteDepartamentRequest);
        }
    }
}
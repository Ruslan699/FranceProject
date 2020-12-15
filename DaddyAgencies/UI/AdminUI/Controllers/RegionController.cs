using AdminUI.Models;
using AutoMapper;
using DaddyAgencies.Application.Features.Region;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Application.Models;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegionController : BaseController
    {
        public RegionController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public ActionResult Index() => View();

        public ActionResult Create() => View("Form", new RegionModel());

        public async Task<ActionResult> Edit(Guid id) => 
            await SendRequest(new FindRegionMapData(id), result => View("Form", result), typeof(RegionModel));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(RegionModel region)
        {
            if (ModelState.IsValid)
            {
                var request = Mapper.Map<SaveRegion>(region);
                var response = await Mediator.SendRequest(request);
                if (response.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            return View("Form");
        }

        [HttpPost]
        public async Task<ActionResult> Remove(Guid id)
        {
            var deleteRegionRequest = new DeleteRegion(id);
            var deleteRegionResponse = await Mediator.SendRequest(deleteRegionRequest);
            return Json(deleteRegionRequest);
        }
    }
}
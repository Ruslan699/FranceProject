using AdminUI.Models;
using AutoMapper;
using DaddyAgencies.Application.Features.Departament;
using DaddyAgencies.Application.Features.PostalCode;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Application.Models;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostalCodeController : BaseController
    {
        public PostalCodeController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> Create()
        {
            var departaments = await Mediator.SendRequest(new GetDepartaments());
            if (departaments.IsSuccess)
                ViewBag.Departaments = departaments.Collection;
            return View("Form", new PostalCodeModel());
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var departaments = await Mediator.SendRequest(new GetDepartaments());
            if (departaments.IsSuccess)
                ViewBag.Departaments = departaments.Collection;

            var command = new FindPostalCode(id);
            var responce = await Mediator.SendRequest(command);
            var model = Mapper.Map<PostalCodeModel>(responce.Payload);
            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Models.PostalCodeModel postalCode)
        {
            if (ModelState.IsValid)
            {
                var request = Mapper.Map<SavePostalCode>(postalCode);
                var response = await Mediator.SendRequest(request);
                if (response.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            return View("Form");
        }

        [HttpPost]
        public async Task<ActionResult> Remove(Guid id)
        {
            var deletePostalCodeRequest = new DeletePostalCode(id);
            await Mediator.SendRequest(deletePostalCodeRequest);
            return Json(deletePostalCodeRequest);
        }
    }
}
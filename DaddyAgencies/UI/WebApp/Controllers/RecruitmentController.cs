using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Features;
using DaddyAgencies.Application.Features.Recruiment;
using DaddyAgencies.Application.Helpers;
using MediatR;
using WebApp.Controllers.Base;
using WebApp.Models;

namespace WebSite.Controllers
{
    public class RecruitmentController : BaseController
    {
        public RecruitmentController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(RecruitmentModel model)
        {
            if (ModelState.IsValid)
            {
                var request = Mapper.Map<SaveRecruitment>(model);
                var response = await Mediator.SendRequest(request);
                if (response.IsSuccess)
                {
                    if (Request.IsAuthenticated)
                        return RedirectToAction("Index", "Profile");

                    return RedirectToAction("NeedToLogIn", "Account",
                        new
                        {
                            @email = model.Email,
                            @fullName = model.FullName,
                            @phoneNumber = model.PhoneNumber
                        });
                }
            }
            return View(nameof(Index), model);  
        }
    }
}
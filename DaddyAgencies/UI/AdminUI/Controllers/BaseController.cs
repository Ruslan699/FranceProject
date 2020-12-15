using AutoMapper;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Common.Contracts;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace AdminUI.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;


        protected BaseController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        protected async Task<ActionResult> SendRequest(Request request,
            Func<ActionResult> onSuccess)
        {
            var result = await Mediator.SendRequest(request);
            if (result.IsFailure)
                return RedirectToErrorPage(result.Issue);
            return onSuccess();
        }


        protected async Task<ActionResult> SendRequest<TResult>(Request<TResult> request,
            Func<object, ActionResult> onSuccess, Type mapResultTo = null, object mapFrom = null)
        {
            var result = await Mediator.SendRequest(request);
            if (result.IsFailure)
                return RedirectToErrorPage(result.Issue);

            if (mapFrom != null)
            {
                if (mapResultTo == null)
                    mapResultTo = mapFrom.GetType();
                Mapper.Map(result.Payload, mapFrom, typeof(TResult), mapResultTo);
                return onSuccess(mapFrom);
            }

            if (mapResultTo != null)
                return onSuccess(Mapper.Map(result.Payload, typeof(TResult), mapResultTo));
            return onSuccess(result.Payload);
        }


        protected async Task<ActionResult> SendRequest<TResult>(RequestOfCollection<TResult> request,
            Func<object, ActionResult> onSuccess, Type mapResultTo = null, object mapFrom = null)
        {
            var result = await Mediator.SendRequest(request);
            if (result.IsFailure)
                return RedirectToErrorPage(result.Issue);

            if (mapFrom != null)
            {
                if (mapResultTo == null)
                    mapResultTo = mapFrom.GetType();
                Mapper.Map(result.Collection, mapFrom, typeof(IEnumerable<TResult>), mapResultTo);
                return onSuccess(mapFrom);
            }

            if (mapResultTo != null)
                return onSuccess(Mapper.Map(result.Collection, typeof(IEnumerable<TResult>), mapResultTo));
            return onSuccess(result.Collection);
        }

        private ActionResult RedirectToErrorPage(Issue resultIssue)
        {
            return RedirectToAction("Index", "Home");
        }

        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }
    }
}
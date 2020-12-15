using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Common.Contracts;
using MediatR;
using WebApp.Helpers;

namespace WebApp.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;

        public Guid UserUid => User.GetGuidValue(ClaimTypes.NameIdentifier);

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
            {
                try
                {
                    var mapped = Mapper.Map(result.Collection, typeof(IEnumerable<TResult>), mapResultTo);
                    return onSuccess(mapped);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return onSuccess(result.Collection);
        }



        protected ActionResult RedirectToErrorPage(Issue resultIssue)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
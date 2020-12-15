using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Features.Region;
using MediatR;
using WebApp.Controllers.Base;

namespace WebApp.Controllers
{
    public class RegionController : BaseController
    {
        public RegionController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public Task<ActionResult> GetRegions(Guid propertyUid) =>
            SendRequest(new GetRegions(), result => Json(result, JsonRequestBehavior.AllowGet));
    }
}
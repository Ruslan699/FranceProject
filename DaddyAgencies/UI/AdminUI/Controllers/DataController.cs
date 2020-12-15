using System;
using AdminUI.Helpers;
using AutoMapper;
using DaddyAgencies.Application.Helpers;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Application.Features;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.EntityFramework.Identity;
using Microsoft.AspNet.Identity;

namespace AdminUI.Controllers
{
    //[Authorize(Roles = "Admin, Agent")]
    public class DataController : Controller
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public DataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetRegionAsync(DataSourceLoadOptions loadOptions)
        {
            var regions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.Inseption.GetRegions());
            var loadResult = DataSourceLoader.Load(regions.Payload, loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersAsync(DataSourceLoadOptions loadOptions)
        {
            var users = await _mediator.SendRequest(new DaddyAgencies.Application.Features.User.GetUsers());
            var loadResult = DataSourceLoader.Load(users.Payload, loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersRolesAsync(DataSourceLoadOptions loadOptions)
        {
            var usersroles = await _mediator.SendRequest(new DaddyAgencies.Application.Features.UserRole.GetUsersRoles());
            var loadResult = DataSourceLoader.Load(usersroles.Payload, loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }


        [HttpGet]
        public async Task<ActionResult> GetInseptionsAsync(DataSourceLoadOptions loadOptions)
        {
            //TODO: UncommentBellow to use UserUid
            Result<IQueryable<Inseption>> inseptions;
            var userId = User.Identity.GetUserId();
            var parsed = Guid.TryParse(userId, out var userUid);
            var postalCodes = await _mediator.SendRequest(new DaddyAgencies.Application.Features.PostalCode.GetPostalCodes());

            if (User.IsInRole("Admin") && parsed)
            {
                 inseptions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.Inseption.GetUserInseptions());
            }
            else
            {
                inseptions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.Inseption.GetUserInseptions(userUid));
            }
            var loadResult = DataSourceLoader.Load(inseptions.Payload,loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }

        [HttpGet]
        public async Task<ActionResult> GetVendorInseptionsAsync(DataSourceLoadOptions loadOptions)
        {
            //TODO: UncommentBellow to use UserUid
            Result<IQueryable<Inseption>> inseptions;
            var userId = User.Identity.GetUserId();
            var parsed = Guid.TryParse(userId, out var userUid);
            var postalCodes = await _mediator.SendRequest(new DaddyAgencies.Application.Features.PostalCode.GetPostalCodes());

            if (User.IsInRole("Admin") && parsed)
            {
                inseptions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.Inseption.GetUserVendorInseptions());
            }
            else
            {
                inseptions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.Inseption.GetUserVendorInseptions(userUid));
            }
            var loadResult = DataSourceLoader.Load(inseptions.Payload, loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }

        //[HttpGet]
        //public async Task<ActionResult> GetPostalCodes(DataSourceLoadOptions loadOptions)
        //{
        //    //TODO: UncommentBellow to use UserUid
        //    //var inseptions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.Inseption.GetUserInseptions(GuidValue));
        //    var inseptions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.PostalCode.GetPostalCodes());
        //    var loadResult = DataSourceLoader.Load(inseptions.Collection, loadOptions);
        //    return Content(loadResult.GetSerializeObject(), "application/json");
        //}
        public static string GetValue(IPrincipal user, string claim)
            => ((ClaimsIdentity)user.Identity).FindFirstValue(claim);

        [HttpGet]
        public async Task<ActionResult> GetPropertiesAsync(DataSourceLoadOptions loadOptions)
        {
            var userId = User.Identity.GetUserId();
            var parsed = Guid.TryParse(userId, out var userUid);

            GetRequest<DaddyAgencies.Application.Models.PropertyPreview> cmd;
            if (parsed && User.IsInRole("Agent"))
                cmd = new DaddyAgencies.Application.Features.Property.GetAgentProperties(userUid);
            else
                cmd = new DaddyAgencies.Application.Features.Property.GetProperties();

            var properties = await _mediator.SendRequest(cmd);
            var loadResult = DataSourceLoader.Load(properties, loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }


        [HttpGet]
        public async Task<ActionResult> GetDepartamentsAsync(DataSourceLoadOptions loadOptions)
        {
            var departaments = await _mediator.SendRequest(new DaddyAgencies.Application.Features.Departament.GetDepartamentsView());
            var loadResult = DataSourceLoader.Load(departaments.Collection, loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }


        [HttpGet]
        public async Task<ActionResult> GetPostalCodesAsync(DataSourceLoadOptions loadOptions)
        {
            var regions = await _mediator.SendRequest(new DaddyAgencies.Application.Features.PostalCode.GetPostalCodesView());
            var loadResult = DataSourceLoader.Load(regions.Collection, loadOptions);
            return Content(loadResult.GetSerializeObject(), "application/json");
        }

        //[HttpGet]
        //public async Task<ActionResult> GetInseptionsAsync(DataSourceLoadOptions loadOptions)
        //{
        //    var request = new DaddyAgencies.Application.Features.Inseption.GetRequestedInseption(HttpContext.);
        //    var response = await _mediator.SendRequest(request);
        //    var loadResult = DataSourceLoader.Load(response.Payload, loadOptions);
        //    return Content(loadResult.GetSerializeObject(), "application/json");
        //}
    }
}
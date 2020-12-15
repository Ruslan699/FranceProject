using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Features.Documents;
using DaddyAgencies.Application.Features.Property;
using DaddyAgencies.Application.Features.Region;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Common.Util;
using MediatR;
using WebApp.Controllers.Base;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PropertyController : BaseController
    {
        public PropertyController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public Task<ActionResult> Index() => SendRequest(new GetRegionMapDatas(), result => View("Index", result));


        [HttpGet]
        public Task<ActionResult> PropertyListForm(PropertyFilter filter) =>
        SendRequest(new GetRegions(), result => View("PropertyList", result), mapFrom: filter);

        [HttpPost]
        public Task<ActionResult> PropertyList(PropertyFilter filter) =>
            SendRequest(new GetRegions(), result => View("PropertyList", result), mapFrom: filter);

        [HttpGet]
        public Task<ViewResult> AddProperty() => Task.FromResult(View("PropertyForm", new PropertyDraftModel()));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<ActionResult> SaveProperty(PropertyDraftModel model) =>
            SendRequest(Mapper.Map<SaveProperty>(model), result => model.CreateAnother ? 
                RedirectToAction("AddProperty", "Property") : 
                RedirectToAction("GetDetails", "Property", new { uid = result }));

        [HttpPost]
        public Task<ActionResult> Search(PageableFilter model) =>
            SendRequest(Mapper.Map<GetPropertiesByFilter>(model),
                result => PartialView("_PropertyPreviewPartial", result), 
                typeof(PropertyModel[]));

        [HttpPost]
        public Task<ActionResult> SortBy(PageableFilter model) =>
            SendRequest(Mapper.Map<GetPropertiesByFilter>(model),
                result => PartialView("_PropertyPreviewPartial", result),
                typeof(PropertyModel[]));

        [HttpPost]
        public Task<ActionResult> GetTotalCount(PageableFilter model) =>
            SendRequest(Mapper.Map<GetPropertyCountByFilter>(model),
                result => Json(result, JsonRequestBehavior.AllowGet));

        public async Task<ActionResult> GetDetails(Guid uid)
        {
           var result2 = await Mediator.SendRequest(new GetDocumentsByPropertyId(uid));
            var model = new PropertyModel()
            {
                PropertyImageIds = result2.ToList()
            };
            return await SendRequest(new FindProperty(uid), result => View("Details", result), typeof(PropertyModel), model);
            
        }

        private string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        [HttpGet]
        [OutputCache(Duration = 86400, VaryByParam = "id")]
        public async Task<ActionResult> GetPropertyImage(Guid id)
        {
            var result = await Mediator.SendRequest(new FindDocument(id));
            var imageStr = result.Payload.Body;
            return File(imageStr, "image/gif");
        }
    }
}
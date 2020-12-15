using AdminUI.Models;
using AutoMapper;
using DaddyAgencies.Application.Features.Departament;
using DaddyAgencies.Application.Features.Inseption;
using DaddyAgencies.Application.Features.PostalCode;
using DaddyAgencies.Application.Features.Property;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DaddyAgencies.Application.Features.Documents;
using DaddyAgencies.Application.Models.Enums;
using System.Reflection;

namespace AdminUI.Controllers
{
    [Authorize(Roles = "Admin, Agent")]
    public class AnnouncementController : BaseController
    {
        public AnnouncementController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> Create()
        {
            var departaments = await Mediator.SendRequest(new GetDepartaments());
            var postalCodes = await Mediator.SendRequest(new GetPostalCodes());
            //if (departaments.IsSuccess)
            //    ViewBag.Departaments = departaments.Collection;
            //if (postalCodes.IsSuccess)
            //    ViewBag.PostalCodes = postalCodes.Collection;

            var values = Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>();
            ViewBag.PropertyTypes = values.Select(x => new SelectListItem { Text = GetDescription(x), Value = ((int)x).ToString() });

            var model = new AnnouncementModel
            {
                Departaments = departaments.ToList(),
                PostalCodes = postalCodes.ToList()
            };
            return View("Form", model);
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

        public async Task<ActionResult> Edit(Guid id)
        {

            var result = await Mediator.SendRequest(new FindProperty(id));
            if (!result.IsSuccess)
                return RedirectToAction(nameof(Index));
            var departaments = await Mediator.SendRequest(new GetDepartaments());
            var postalCodes = await Mediator.SendRequest(new GetPostalCodes());

            //if (departaments.IsSuccess)

            //    ViewBag.Departaments = departaments.Collection;
            //if (postalCodes.IsSuccess)
            //    ViewBag.PostalCodes = postalCodes.Collection;

            var values = Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>();
            ViewBag.PropertyTypes = values.Select(x => new SelectListItem { Text = GetDescription(x), Value = ((int)x).ToString() });

            var imageIds = await Mediator.SendRequest(new GetDocumentsByPropertyId(id));

            var model = Mapper.Map<AnnouncementModel>(result.Payload);
            model.ImageIds = imageIds.ToList();
            model.Departaments = departaments.ToList();
            model.PostalCodes = postalCodes.ToList();
            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(AnnouncementModel announcement)
        {
            if (ModelState.IsValid)
            {
                var request = Mapper.Map<SaveProperty>(announcement);
                var response = await Mediator.SendRequest(request);
                if (response.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }


            var departaments = await Mediator.SendRequest(new GetDepartaments());
            var postalCodes = await Mediator.SendRequest(new GetPostalCodes());
            announcement.Departaments = departaments.ToList();
            announcement.PostalCodes = postalCodes.ToList();
            //if (departaments.IsSuccess)
            //    ViewBag.Departaments = departaments.Collection;
            //if (postalCodes.IsSuccess)
            //    ViewBag.PostalCodes = postalCodes.Collection;

            var values = Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>();
            ViewBag.PropertyTypes = values.Select(x => new SelectListItem { Text = GetDescription(x), Value = ((int)x).ToString() });

            return View("Form", announcement);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(Guid id)
        {
            var deletePropertyRequest = new DeleteProperty(id);
            var deletePropertyResponse = await Mediator.SendRequest(deletePropertyRequest);
            return Json(deletePropertyRequest);
        }

        [HttpPost]
        public async Task<ActionResult> PostalCodes(Guid departamentId)
        {
            List<Guid> departamentIds = new List<Guid>();
            departamentIds.Add(departamentId);
            if (departamentId != null)
            {
                var command = new GetPostalCodes(departamentIds);
                var responce = await Mediator.SendRequest(command);
                var postalCodes = responce.Collection
                   .Select(e => new SelectListItem()
                    {
                        Text = e.Name,
                        Value = e.Uid.ToString()
                    });

                return Json(postalCodes);
            }
            else
            {
                var command = new GetPostalCodes(departamentIds);
                var responce = await Mediator.SendRequest(command);
                var postalCodes = responce.Collection
                   .Select(e => new SelectListItem()
                   {
                       Text = e.Name,
                       Value = e.Uid.ToString()
                   });

                return Json(postalCodes);
            }
        }

        [HttpPost]
        public async Task<ActionResult> RemovePicture(Guid id)
        {
            var deleteDocumentRequest = new DeleteDocument(id);
            var response = await Mediator.SendRequest(deleteDocumentRequest);
            return Json(response);
        }

        public async Task<ActionResult> SortableImage(string itemIds)
        {
            List<Guid> itemIdList = new List<Guid>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList();
            var response = new PropertyDocumentRowNoModel {
                PropertyUids = itemIdList
              };
            var request = Mapper.Map<UpdatePropertyDocumentRowNo>(response);
            var response2 = await Mediator.SendRequest(request);
            if (response2.IsSuccess)
                return RedirectToAction(nameof(Index));
            return Content(itemIds);
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
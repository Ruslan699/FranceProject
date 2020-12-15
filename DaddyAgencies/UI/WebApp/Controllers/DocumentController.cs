using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Features.Documents;
using DaddyAgencies.Application.Helpers;
using MediatR;
using WebApp.Controllers.Base;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DocumentController : BaseController
    {
        public DocumentController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public Task<ActionResult> GetPropetyDocuments(Guid propertyUid) =>
            SendRequest(new GetDocumentsByProperty(propertyUid),
                result => new JsonResult()
                {
                    Data = result,
                    MaxJsonLength = int.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                },
                typeof(ImageDocument[]));

        [HttpGet]
        public async Task<ActionResult> GetPropetyThumbnail(Guid propertyUid)
        {
            var result = await Mediator.SendRequest(new GetMainPropertyImageId(propertyUid));
            if(result.Payload != default(Guid))
                return Json(result.Payload, JsonRequestBehavior.AllowGet);
                

            var image = StaticUI.no_image;

            using (var m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                var imageBytes = m.ToArray();

                var base64String = Convert.ToBase64String(imageBytes);
                return Json(base64String, JsonRequestBehavior.AllowGet);
            }
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
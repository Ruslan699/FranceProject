using AdminUI.Models;
using AdminUI.Security;
using AutoMapper;
using DaddyAgencies.Application.Features.Inseption;
using DaddyAgencies.Application.Features.PostalCode;
using DaddyAgencies.Application.Features.User;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Util.Helpers;
using DaddyAgencies.EntityFramework;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Roles = "Admin, Agent")]
    public class InseptionController : BaseController
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public InseptionController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var postalCodes = await _mediator.SendRequest(new GetPostalCodes());

            var model = new SaleInseptionModel
            {
                PostalCodes = postalCodes.Select(x => new SelectListItem { Text = x.Name, Value = x.Uid.ToString() }).ToList()
            };
            return View(model);
        }

        public async Task<ActionResult> Vendor()
        {
            var postalCodes = await _mediator.SendRequest(new GetPostalCodes());

            var model = new SaleInseptionModel
            {
                PostalCodes = postalCodes.Select(x => new SelectListItem { Text = x.Name, Value = x.Uid.ToString() }).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid uid)
        {
            var request = new FindInseption(uid);
            var response = await _mediator.SendRequest(request);
            var dto = response.Payload;
            return PartialView("Partials/_DetailsPartial", dto);
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(Guid uid,DateTime ConfirmedDate,int Time, string message)
        {
            var request = new FindInseption(uid);
            var response = await _mediator.SendRequest(request);
            var dto = response.Payload;
            DateTime newconfirmedDate = new DateTime(ConfirmedDate.Year, ConfirmedDate.Month, ConfirmedDate.Day, Time, 0, 0);
            var processCmd = new ProcessInseption();
            processCmd.InseptionUid = uid;
            processCmd.NewStatus = InseptionStatus.Confirmé;

            if (response.IsSuccess)
            {               
                if (message == "")
                {
                    processCmd.ConfirmedDate = dto.RequestedDate;
                    message = $"Bonjour, Votre demande de rdv téléphonique a été traité, Papa vous contactera à l’heure de rappel que vous avez indiqué dans le formulaire.";
                    MailHelper.SendEmail(dto.CustomerEmail.ToString(), "Votre demande de RDV chez Les Agences de Papa", message);
                    message = "Cher" + " " + User.Identity.Name + " " + "vous avez commencé à" + " " + dto.CustomerEmail + " " + ",et création créée<br/><br/>" +
                    "L'heure de la rencontre : " + dto.RequestedDate.ToString("dddd, dd MMMM yyyy, HH:mm");
                    MailHelper.SendEmail(User.Identity.Name, "Début", message);
                }
                else
                {
                    processCmd.ConfirmedDate = newconfirmedDate;
                    message += "<br/><br/>L'heure de la rencontre : " + newconfirmedDate.ToString("dddd, dd MMMM yyyy, HH:mm");
                    MailHelper.SendEmail(dto.CustomerEmail.ToString(), "Votre demande de RDV chez Les Agences de Papa", message);
                    #region Send Email to Agent Mail Check!!!
                    message = "";
                    message = "Cher" + " " + User.Identity.Name + " " + "vous avez commencé à" + " " + dto.CustomerEmail + " " + ",et création créée<br/><br/>" +
                        "L'heure de la rencontre : " + newconfirmedDate.ToString("dddd, dd MMMM yyyy, HH:mm");
                    MailHelper.SendEmail(User.Identity.Name, "Début", message);
                    #endregion
                }
            }
            var processResponce = await _mediator.SendRequest(processCmd);
            return Json(response);

        }

        [HttpPost]
        public async Task<ActionResult> Reject(Guid uid, string message)
        {
            var request = new FindInseption(uid);
            var response = await _mediator.SendRequest(request);
            var dto = response.Payload;


            var processCmd = new ProcessInseption();
            processCmd.InseptionUid = uid;
            processCmd.NewStatus = InseptionStatus.Rejeté;
            processCmd.ConfirmedDate = DateTime.Now;
            var processResponce = await _mediator.SendRequest(processCmd);
            if (response.IsSuccess)
            {
                if (message == "")
                    message = "Bonjour, <br/> Nous ne sommes pas en mesure de vous contacter à cette horaire, ne balisez pas Papa vous contacteras au plus vite:) <br/> Merci de verifier vos coordonnées telephonique sur notre site www.lesagencesdepapa.fr <br/> Merci qui? Merci Papa!";
                MailHelper.SendEmail(dto.CustomerEmail.ToString(), "Votre demande de RDV chez Les Agences de Papa", message);
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(Guid id)
        {
            var deleteInseptionRequest = new DeleteInseption(id);
            var deleteInseptionResponse = await Mediator.SendRequest(deleteInseptionRequest);
            return Json(deleteInseptionRequest);
        }
    }
}
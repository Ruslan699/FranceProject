using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Features.Inseption;
using DaddyAgencies.Application.Features.PostalCode;
using DaddyAgencies.Application.Features.Property;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.Util.Helpers;
using MediatR;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Controllers.Base;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class InseptionController : BaseController
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }


        public InseptionController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }


        [HttpGet]
        public async Task<ActionResult> GetRequestedInseption()
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await Mediator.SendRequest(new GetRequestedInseption(UserUid));
                return PartialView("_RequestedInseptionPartial", result.Collection);
            }
            return Json(null);
        }



        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> SaveInseption()
        {
            var postalCodes = await Mediator.SendRequest(new GetPostalCodes());

            var model = new SaleInseptionModel
            {
                PostalCodes = postalCodes.Select(x => new SelectListItem { Text = x.Name, Value = x.Uid.ToString() }).ToList()
            };
            if (Request.IsAuthenticated)
            {
                model.CustomerEmail = User.GetEmail();
                model.CustomerName = User.GetFullName();
                model.CustomerPhone = User.GetPhoneNumber();
            }


            return View("SaleInseptionForm", model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SaveInseption(InseptionModel model, string message)
        {
            
            if (model.PostalCodeUid.HasValue)
            {
                var regionUid = await Mediator.SendRequest(new PostalCodeRegion(model.PostalCodeUid.Value));
                model.RegionUid = regionUid.Payload;
            }
            if (model.PropertyUid.HasValue)
            {
                var propertyResponce = await Mediator.SendRequest(new FindProperty(model.PropertyUid.Value));
                if (propertyResponce.IsSuccess)
                {
                    model.PostalCodeUid = propertyResponce.Payload.PostalCodeUid;
                    model.PropertyTitle = propertyResponce.Payload.Name;
                }
            }


            var request = Mapper.Map<SaveInseption>(model);

            Result<Guid> response;
            if (Request.IsAuthenticated)
            {
                request.UserUid = User.GetUid();
                response = await Mediator.SendRequest(request);
            }
            else
            {
                response = await Mediator.SendRequest(request);
            }


            #region Send Email to Agent Email

            if (response.IsSuccess)
            {
                var agentEmails = await Mediator.SendRequest(new GetAgentsEmailsForProperty(model.PostalCodeUid, model.PropertyUid));
                if (agentEmails.IsSuccess)
                {
                    foreach (var agentEmail in agentEmails)
                    {
                            if (model.PropertyUid.HasValue)
                            {
                                MailHelper.SendEmail(agentEmail.UserEmail, "Nouvelle demande de RDV pour Les Agences de Papa : Achat",
                                "Bonjour, <br/>  Vous avez reçu une demande de RDV téléphonique, merci de contacter le client suivant à l’horaire indiqué ci - dessous :" +

                                                                                        $"<br/> Achat : {model.CustomerName}." +
                                                                                        $"<br/> De : {model.CustomerEmail}." +
                                                                                        $"<br/> Téléphone : {model.CustomerPhone}." +
                                                                                        $"<br/> Date: {model.Date?.ToString("dd.MM.yyyy") ?? "NoDate"} " +
                                                                                        $"<br/> Heure: {(model.Time.HasValue ? $"{model.Time}:00" : "NoTime")}" +
                                                                                        $"<br/> Url: <a target='_blank' href='{model.PropertyLink}'> {model.PropertyTitle} <a/>"
                                                                                        );
                            }
                            else
                            {
                                MailHelper.SendEmail(agentEmail.UserEmail, "Nouvelle demande de RDV pour Les Agences de Papa : Vente",
                                "Bonjour, <br/>  Vous avez reçu une demande de RDV téléphonique, merci de contacter le client suivant à l’horaire indiqué ci - dessous :" +

                                                                                        $"<br/> Vente : {model.CustomerName}." +
                                                                                        $"<br/> De : {model.CustomerEmail}." +
                                                                                        $"<br/> Téléphone : {model.CustomerPhone}." +
                                                                                        $"<br/> Date: {model.Date?.ToString("dd.MM.yyyy") ?? "NoDate"} " +
                                                                                        $"<br/> Heure: {(model.Time.HasValue ? $"{model.Time}:00" : "NoTime")}");
                            }
                    }
                }
            }
            #endregion

            //if (Request.IsAuthenticated)
            //    return RedirectToAction("Index", "Profile");

            return RedirectToAction("Success");
                //new
                //{
                //    @inseptionUid = response.Payload,
                //    @email = model.CustomerEmail,
                //    @fullName = model.CustomerName,
                //    @phoneNumber = model.CustomerPhone
                //});
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Success()
        {
            return View("InseptionSusccess");
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> GetDisableDates(Guid propertyUid)
        {
            var inseptionHours = await Mediator.SendRequest(new GetAvailableInseptionDates(propertyUid));
            return Json(inseptionHours.Collection);
        }
    }
}
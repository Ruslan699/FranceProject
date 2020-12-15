using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Features.Inseption;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Common.EntityFramework.Identity;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApp.Controllers.Base;
using WebApp.Models;

namespace WebApp.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

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

        [AllowAnonymous]
        public async Task<ActionResult> NeedToLogIn(Guid? inseptionUid, string email, string fullName, string phoneNumber)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Register",
                    new RegisterViewModel
                    {
                        InseptionUid = inseptionUid,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Fullname = fullName
                    });
            }
            if (inseptionUid.HasValue)
                await Mediator.SendRequest(new SaveInseptionUser(inseptionUid.Value, user.Id));

            ViewBag.ReturnUrl = Url.Action("Index", "Profile");
            return View("Login", new LoginViewModel { Email = email });
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError("", @"Invalid login attempt.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<IdentityUser>(model);
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);

                    if (model.InseptionUid.HasValue)
                        await Mediator.SendRequest(new SaveInseptionUser(model.InseptionUid.Value, user.Id));

                    return RedirectToAction("SavePersonDraft", "Profile", new { fullName = model.Fullname });
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
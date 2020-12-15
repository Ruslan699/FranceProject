using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AdminUI.Models;
using AdminUI.Security;
using AutoMapper;
using DaddyAgencies.Application.Features.Inseption;
using DaddyAgencies.Application.Features.PostalCode;
using DaddyAgencies.Application.Features.User;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Common.EntityFramework.Identity;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AdminUI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ActionResult Index() => View();

        public ActionResult Create() => View("Form", new CreateModel());

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var userModel = Mapper.Map<CreateModel>(user);
            return View("EditForm", userModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return RedirectToAction(nameof(Index));

            var responce = await Mediator.SendRequest(new DeleteUserReferences(id));

            if (responce.IsSuccess)
                await UserManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        #region UserRole

        public async Task<ActionResult> Connect()
        {
            var postalCodes = await Mediator.SendRequest(new GetPostalCodes());
            var postalCodesUids = await Mediator.SendRequest(new GetUserPostalCodes());

            var model = new SaleInseptionModel
            {
                PostalCodes = postalCodes.Select(x => new SelectListItem { Text = x.Name, Value = x.Uid.ToString() }).ToList(),
                PostalCodeUids = postalCodesUids.ToArray(),
            };
            var Users = await Mediator.SendRequest(new GetUsers());
            if (Users.IsSuccess)
                ViewBag.Users = Users.Payload;
            return View("UserRoleForm", model);
        }

        public async Task<ActionResult> UserPostalCodes(Guid? userId)
        {
            var postalCodes = await Mediator.SendRequest(new GetPostalCodes());
            var postalCodesUids = await Mediator.SendRequest(new GetUserPostalCodes());
            if (userId != default(Guid))
                postalCodesUids = await Mediator.SendRequest(new GetUserPostalCodes(userId.Value));


            var model = new SaleInseptionModel
            {
                PostalCodes = postalCodes.Select(x => new SelectListItem { Text = x.Name, Value = x.Uid.ToString() }).ToList(),
                PostalCodeUids = postalCodesUids.ToArray(),
            };
            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRoleSave(SaleInseptionModel userrole, string RoleName)
        {

            var request = await Mediator.SendRequest(new SaveUserPostalCodes(userrole.UserId, userrole.PostalCodeUids));
            if (ModelState.IsValid)
            {
                var oldRoles = await UserManager.GetRolesAsync(userrole.UserId);
                await UserManager.RemoveFromRolesAsync(userrole.UserId, oldRoles.ToArray());

                var result = await UserManager.AddToRoleAsync(userrole.UserId, RoleName);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var Users = await Mediator.SendRequest(new GetUsers());
                    if (Users.IsSuccess)
                        ViewBag.Users = Users.Payload;
                }
            }
            return View("Form");
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<IdentityUser>(model);
                user.UserName = model.Email;
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, false, false);

                    if (model.InseptionUid.HasValue)
                        await Mediator.SendRequest(new SaveInseptionUser(model.InseptionUid.Value, user.Id));

                    return RedirectToAction(nameof(Index));
                }
                AddErrors(result);
            }
            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(CreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await UserManager.FindByIdAsync(createModel.Id);
                    user.UserName = createModel.Email;
                    user.Email = createModel.Email;
                    user.PhoneNumber = createModel.PhoneNumber;

                    await UserManager.RemovePasswordAsync(user.Id);
                    await UserManager.AddPasswordAsync(user.Id, createModel.Password);
                    await UserManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(createModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                default:
                    ModelState.AddModelError("", @"Invalid login attempt.");
                    return View(model);
            }
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}
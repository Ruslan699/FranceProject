
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DaddyAgencies.Application.Features.PhysicalPerson;
using DaddyAgencies.Application.Helpers;
using DaddyAgencies.Common.EntityFramework.Identity;
using MediatR;
using WebApp.Controllers.Base;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userId = User.GetUid();
            var person = await Mediator.SendRequest(new FindPhysicalPersonByUser(userId));
            var model = Mapper.Map<PhysicalPersonModel>(person.Payload);
            return View("Index", model);
        }

        [HttpGet]
        public async Task<ActionResult> SavePersonDraft(string fullName = "")
        {
            var fullNames = fullName.Split(' ').ToList();
            while (fullNames.Count < 3)
                fullNames.Add(" - ");

            var request = new SavePhysicalPersonDraft
            {
                Surname = fullNames[0],
                Name = fullNames[1],
                LastName = fullNames[2],
                PhoneNumber = User.GetValue(Claims.PhoneNumber),
                Email = User.GetValue(ClaimTypes.Name),
                UserUid = User.GetUid()
            };
            var responce = await Mediator.SendRequest(request);

            return RedirectToAction("Index", "Profile");
        }
    }
}
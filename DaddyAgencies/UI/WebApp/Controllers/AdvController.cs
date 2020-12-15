using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AdvController : Controller
    {
        // GET: Adv
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SaveAdv(AdvModel model)
        {
            return RedirectToAction(nameof(Index), nameof(AdvController));
        }
    }
}
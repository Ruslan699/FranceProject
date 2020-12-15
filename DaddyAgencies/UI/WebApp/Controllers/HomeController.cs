using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Error(ErrorModel model)
        {
            return View("Error");
        }
    }

    public class ErrorModel
    {
    }
}
using System.Web.Mvc;

namespace HealthTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Health Tracker";
            return View();
        }
    }
}
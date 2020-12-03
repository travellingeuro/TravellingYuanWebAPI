using System.Web.Mvc;

namespace TravellingYuanWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Privacy()
        {
            ViewBag.Title = "TravellingDollar - Privacy Policy";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "TravellingDollar - About";
            return View();
        }
    }
}

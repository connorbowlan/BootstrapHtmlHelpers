using System.Web.Mvc;
using SampleApplication.Web.Models;

namespace SampleApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SampleModel model)
        {
            return View(model);
        }
    }
}
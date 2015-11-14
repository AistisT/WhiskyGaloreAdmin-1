using System.Web.Mvc;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class ManagerController : Controller
    {

        public ActionResult Orders()
        {
            return View(new Manager("getOrders"));
        }
        public ActionResult Whisky()
        {
            return View(new Manager("getProductInfo"));
        }
    }
}

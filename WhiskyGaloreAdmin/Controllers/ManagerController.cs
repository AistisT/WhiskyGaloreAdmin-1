using System.Web.Mvc;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class ManagerController : Controller
    {

        public ActionResult Orders()
        {
            Manager m = new Manager();
            m.getData("getOrders");
            return View(m);
        }
        public ActionResult Whisky()
        {
            Manager m = new Manager();
            m.getData("getProductInfo");
            return View(m);
        }
        public ActionResult Staff()
        {
            Manager m = new Manager();
            m.getData("getStaffDetailsManager");
            return View(m);
        }

    }
}

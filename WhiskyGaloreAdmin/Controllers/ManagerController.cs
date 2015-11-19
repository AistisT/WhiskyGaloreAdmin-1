using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class ManagerController : Controller
    {
        [ManagerFilter]
        public ActionResult Orders()
        {
            Manager m = new Manager();
            m.getData("getOrders");
            return View(m);
        }
        [ManagerFilter]
        public ActionResult Whisky()
        {
            Manager m = new Manager();
            m.getData("getProductInfo");
            return View(m);
        }

        [ManagerFilter]
        public ActionResult Staff()
        {
            Manager m = new Manager();
            m.getData("getStaffDetailsManager");
            return View(m);
        }

    }
}

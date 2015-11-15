using System.Net;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class FinancesController : Controller
    {

        [HttpGet]
        public ActionResult DailyFinances()
        {
            Manager m = new Manager();
            m.getData("getDailyFinances");
            return View(m);
        }
        [HttpGet]
        public ActionResult Calculate(System.DateTime dailyDate)
        {
            ModelState.Clear();
            Manager m = new Manager();
            m.calculateDailyFinances(dailyDate);
            m.getData("getDailyFinances");
            return View("DailyFinances",m);
        }
    }
}
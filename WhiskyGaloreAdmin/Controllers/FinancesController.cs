using System.Web.Mvc;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class FinancesController : Controller
    {
        // GET: Finances
        public ActionResult DailyFinances()
        {
            return View(new Manager("getDailyFinances"));
        }
    }
}
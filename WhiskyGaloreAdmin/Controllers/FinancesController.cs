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

        [HttpGet]
        public ActionResult MonthlyFinances()
         {
            Manager m = new Manager();
            m.getData("getMonthlyFinances");
            return View(m);
        }
        [HttpGet]
        public ActionResult CalculateMonthly(int month,string year)
        {
            ModelState.Clear();
            Manager m = new Manager();
            m.calculateMonthlyFinances (month,year);
            m.getData("getMonthlyFinances");
            return View("MonthlyFinances", m);
        }

        [HttpGet]
        public ActionResult YearlyFinances()
        {
            Manager m = new Manager();
            m.getData("getYearlyFinances");
            return View(m);
        }

        public ActionResult CalculateYearly(string year)
        {
            ModelState.Clear();
            Manager m = new Manager();
            m.calculateYearlyFinances(year);
            m.getData("getYearlyFinances");
            return View("YearlyFinances", m);
        }

    }
}
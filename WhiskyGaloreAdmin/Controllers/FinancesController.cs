using System.Net;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class FinancesController : Controller
    {
        [LoggingFilter]
        [HttpGet]
        public ActionResult DailyFinances()
        {
            Manager m = new Manager();
            m.getData("getDailyFinances");
            return View(m);
        }
        [LoggingFilter]
        [HttpGet]
        public ActionResult Calculate(System.DateTime dailyDate)
        {
            ModelState.Clear();
            Manager m = new Manager();
            m.calculateDailyFinances(dailyDate);
            m.getData("getDailyFinances");
            return View("DailyFinances",m);
        }
        [LoggingFilter]
        [HttpGet]
        public ActionResult MonthlyFinances()
         {
            Manager m = new Manager();
            m.getData("getMonthlyFinances");
            return View(m);
        }
        [LoggingFilter]
        [HttpGet]
        public ActionResult CalculateMonthly(int month,string year)
        {
            ModelState.Clear();
            Manager m = new Manager();
            m.calculateMonthlyFinances (month,year);
            m.getData("getMonthlyFinances");
            return View("MonthlyFinances", m);
        }
        [LoggingFilter]
        [HttpGet]
        public ActionResult YearlyFinances()
        {
            Manager m = new Manager();
            m.getData("getYearlyFinances");
            return View(m);
        }
        [LoggingFilter]
        public ActionResult CalculateYearly(string year)
        {
            ModelState.Clear();
            Manager m = new Manager();
            m.calculateYearlyFinances(year);
            m.getData("getYearlyFinances");
            return View("YearlyFinances", m);
        }
        [LoggingFilter]
        [HttpGet]
        public ActionResult CountrySales()
        {
            Manager m = new Manager();
            m.getData("getCountrySales");
            return View(m);
        }
        [LoggingFilter]
        public ActionResult CalculateCountry(string country)
        {
            ModelState.Clear();
            Manager m = new Manager();
            m.calculateCountrySales(country);
            m.getData("getCountrySales");
            return View("CountrySales", m);
        }

    }
}
using System.Net;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class FinancesController : Controller
    {

        [ManagerFilter]
        [HttpGet]
        public ActionResult DailyFinances()
        {
            Manager m = new Manager();
            m.getData("getDailyFinances");
            return View(m);
        }

        [ManagerFilter]
        [HttpGet]
        public ActionResult Calculate(System.DateTime dailyDate)
        {
            if (dailyDate==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dailyDate == null)
            {
                return HttpNotFound();
            }

            ModelState.Clear();
            Manager m = new Manager();
            m.calculateDailyFinances(dailyDate);
            m.getData("getDailyFinances");
            return View("DailyFinances",m);
        }

        [ManagerFilter]
        [HttpGet]
        public ActionResult MonthlyFinances()
         {
            Manager m = new Manager();
            m.getData("getMonthlyFinances");
            return View(m);
        }

        [ManagerFilter]
        [HttpGet]
        public ActionResult CalculateMonthly(int month,string year)
        {

            if (month < 0 || string.IsNullOrEmpty(year))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (month < 0 || string.IsNullOrEmpty(year))
            {
                return HttpNotFound();
            }


            ModelState.Clear();
            Manager m = new Manager();
            m.calculateMonthlyFinances (month,year);
            m.getData("getMonthlyFinances");
            return View("MonthlyFinances", m);
        }

        [ManagerFilter]
        [HttpGet]
        public ActionResult YearlyFinances()
        {
            Manager m = new Manager();
            m.getData("getYearlyFinances");
            return View(m);
        }

        [ManagerFilter]
        public ActionResult CalculateYearly(string year)
        {
            if (string.IsNullOrEmpty(year))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrEmpty(year))
            {
                return HttpNotFound();
            }

            ModelState.Clear();
            Manager m = new Manager();
            m.calculateYearlyFinances(year);
            m.getData("getYearlyFinances");
            return View("YearlyFinances", m);
        }

        [ManagerFilter]
        [HttpGet]
        public ActionResult CountrySales()
        {
            Manager m = new Manager();
            m.getData("getCountrySales");
            return View(m);
        }

        [ManagerFilter]
        public ActionResult CalculateCountry(string country)
        {
            if (string.IsNullOrEmpty(country))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrEmpty(country))
            {
                return HttpNotFound();
            }

            ModelState.Clear();
            Manager m = new Manager();
            m.calculateCountrySales(country);
            m.getData("getCountrySales");
            return View("CountrySales", m);
        }
    }
}
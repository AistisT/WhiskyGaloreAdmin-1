using WhiskyGaloreAdmin.Models;
using System.Web.Mvc;
using System.Net;
using WhiskyGaloreAdmin.Filters;

namespace WhiskyGaloreAdmin.Controllers
{
    public class DailyHoursController : Controller
    {
        [LoggingFilter]
        [ManagerFilter]
        [HandleError()]
        public ActionResult SomeError()
        {

            Manager m = new Manager();
            m.getData("getStaffDataWithDailyHours");
            return View("Details",m);
        }

        [LoggingFilter]
        [ManagerFilter]
        public ActionResult Details()
        {
            Manager m = new Manager();
            m.getData("getStaffDataWithDailyHours");
            return View(m);
        }
        [LoggingFilter]
        [ManagerFilter]
        public ActionResult Add()
        {
            DailyHours d = new DailyHours();
            d.getNames();
            return View(d);
        }

        [LoggingFilter]
        [ManagerFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DailyHours h)
        {
            // if (ModelState.IsValid)
            // {
            h.InsertDailyhours(h);
            ModelState.Clear();
            DailyHours d = new DailyHours();
            d.getNames();
            return View(d);
            // }
            //   else
            //    return View(h);
        }
        [LoggingFilter]
        [ManagerFilter]
        [HttpGet]
        public ActionResult Edit(int staffId)
        {
            if (staffId < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (staffId < 0)
            {
                return HttpNotFound();
            }
                ModelState.Clear();
                DailyHours d = new DailyHours();
                d.getData(staffId);
                return View(d);
        }

        [LoggingFilter]
        [ManagerFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DailyHours d, string command)
        {

            if (command.Equals("Update"))
            {
                // if (ModelState.IsValid)
                //{
                d.UpdateDailyHours(d);
                // }
                //  else
                //{
                //   return View(d);
                //  }
            }
            else
            {
                d.DeleteDailyHours(d);
            }
            return RedirectToAction("Details");
        }
    }
}
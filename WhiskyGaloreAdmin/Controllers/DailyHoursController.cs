using WhiskyGaloreAdmin.Models;
using System.Web.Mvc;
using System.Net;
using WhiskyGaloreAdmin.Filters;

namespace WhiskyGaloreAdmin.Controllers
{
    public class DailyHoursController : Controller
    {


        [ManagerFilter]
        public ActionResult Details()
        {
            Manager m = new Manager();
            m.getData("getStaffDataWithDailyHours");
            return View(m);
        }

        [ManagerFilter]
        public ActionResult Add()
        {
            DailyHours d = new DailyHours();
            d.getNames();
            return View(d);
        }

        [ManagerFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DailyHours h)
        {
            if (h == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (h == null)
            {
                return HttpNotFound();
            }
            h.InsertDailyhours(h);
            ModelState.Clear();
            DailyHours d = new DailyHours();
            d.getNames();
            return View(d);

        }
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

        [ManagerFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DailyHours d, string command)
        {
            if (d == null || string.IsNullOrEmpty(command))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (d == null || string.IsNullOrEmpty(command))
            {
                return HttpNotFound();
            }
            if (command.Equals("Update"))
            {

                d.UpdateDailyHours(d);

            }
            else
            {
                d.DeleteDailyHours(d);
            }
            return RedirectToAction("Details");
        }
    }
}
using WhiskyGaloreAdmin.Models;
using System.Web.Mvc;
using System.Net;

namespace WhiskyGaloreAdmin.Controllers
{
    public class DailyHoursController : Controller
    {
        [HandleError()]
        public ActionResult SomeError()
        {
            Manager m = new Manager();
            m.getData("getStaffDataWithDailyHours");
            return View("details",m);
        }


        public ActionResult Details()
        {
            Manager m = new Manager();
            m.getData("getStaffDataWithDailyHours");
            return View(m);
        }

        public ActionResult Add()
        {
            DailyHours d = new DailyHours();
            d.getNames();
            return View(d);
        }


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
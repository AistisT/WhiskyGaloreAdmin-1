using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using WhiskyGaloreAdmin.Models;
using System.Data;
using System.Net;

namespace WhiskyGaloreAdmin.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: /Employee/Details
        public ActionResult Details()
        {
            return View(new Staff());
        }

        // GET: /Employee/Register
        public ActionResult Register()
        {
            return View(new Staff());
        }

        // POST: /Employee/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Staff r)
        {
            if (ModelState.IsValid)
            {
                r.insertEmployee(r);
            }
            else
            {

                return View(r);
            }
            return RedirectToAction("Details");
        }

        // GET: /Employee/Edit
        public ActionResult Edit(string username)
        {
            Debug.WriteLine("in controller " + username);
            if (username == null)
            {
                Debug.WriteLine("null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (username == null)
            {
                Debug.WriteLine("not found");
                return HttpNotFound();
            }
            Staff s = new Staff(username);
            return View(s);
        }

        // POST: /Employee/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff r, string command)
        {
            Debug.WriteLine("in edit");
            if (command.Equals("Update"))
            {
                Debug.WriteLine("c " + command);
                if (ModelState.IsValid)
                {
                    r.updateEmployee(r);
                }
                else
                {
                    Debug.WriteLine("username " + r.username);
                    return View(r);
                }
            }
            else
            {
                Debug.WriteLine("c " + command);
                r.deleteEmployee(r.username);
            }
            return RedirectToAction("Details");
        }
    }
}
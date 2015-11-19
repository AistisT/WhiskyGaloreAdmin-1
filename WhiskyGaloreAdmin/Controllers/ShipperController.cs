using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using WhiskyGaloreAdmin.Models;
using System.Data;
using System.Net;
using WhiskyGaloreAdmin.Filters;

namespace WhiskyGaloreAdmin.Controllers
{
    public class ShipperController : Controller
    {

        [AdminFilter]
        // GET: /Shipper/Details
        public ActionResult Details()
        {
                return View(new Shipper());
        }

        [AdminFilter]
        // GET: /Shipper/Register
        public ActionResult Register()
        {
            return View();
        }

        [AdminFilter]
        // POST: /Shipper/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Shipper s)
        {
            if (s == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (s == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                s.insertShipper(s);
            }
            else
            {

                return View(s);
            }
            return RedirectToAction("Details");
        }

        [AdminFilter]
        // GET: /Shipper/Edit
        public ActionResult Edit(string username)
        {

            if ( string.IsNullOrEmpty(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if ( string.IsNullOrEmpty(username))
            {
                return HttpNotFound();
            }
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
            Shipper s = new Shipper(username);
             
            return View(s);
        }

        [AdminFilter]
        // POST: /Shipper/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shipper r, string command)
        {
            if (r == null || string.IsNullOrEmpty(command))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (r == null || string.IsNullOrEmpty(command))
            {
                return HttpNotFound();
            }

            if (command.Equals("Update"))
            {
                if (ModelState.IsValid)
                {
                    r.updateShipper(r);
                }
                else
                {
                    return View(r);
                }
            }
            else
            {
                r.deleteShipper(r.username);
            }
            return RedirectToAction("Details");
        }
    }
}
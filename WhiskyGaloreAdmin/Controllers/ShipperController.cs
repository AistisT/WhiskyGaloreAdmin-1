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
        [LoggingFilter]
        // GET: /Shipper/Details
        public ActionResult Details()
        {
            if (System.Web.HttpContext.Current.Session["lg"] != null)
            {
                return View(new Shipper());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [LoggingFilter]
        // GET: /Shipper/Register
        public ActionResult Register()
        {
            return View();
        }
        [LoggingFilter]
        // POST: /Shipper/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Shipper s)
        {
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
        [LoggingFilter]
        // GET: /Shipper/Edit
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
            Shipper s = new Shipper(username);
             
            return View(s);
        }
        [LoggingFilter]
        // POST: /Shipper/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shipper r, string command)
        {
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
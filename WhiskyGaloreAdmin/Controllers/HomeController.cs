using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;
namespace WhiskyGaloreAdmin.Controllers
{
    public class HomeController : Controller
    {
        [LoggingFilter]
        public ActionResult Manager()
        {
            return View();
        }
        [LoggingFilter]
        //
        // GET: /Home/Admin
        public ActionResult Admin()
        {

            return View();
        }

        [LoggingFilter]
        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }
        [LoggingFilter]
        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }
        [LoggingFilter]
        public ActionResult Whisky()
        {
            var c= DependencyResolver.Current.GetService<ManagerController>();
            return c.Whisky();
        }
        [LoggingFilter]
        public ActionResult Forms()
        {
            return View("Forms");
        }
        [LoggingFilter]
        public ActionResult Panels()
        {
            return View("Panels");
        }
        [LoggingFilter]
        public ActionResult Buttons()
        {
            return View("Buttons");
        }
        [LoggingFilter]
        public ActionResult Notifications()
        {
            return View("Notifications");
        }
        [LoggingFilter]
        public ActionResult Typography()
        {
            return View("Typography");
        }
        [LoggingFilter]
        public ActionResult Icons()
        {
            return View("Icons");
        }
        [LoggingFilter]
        public ActionResult Grid()
        {
            return View("Grid");
        }
        [LoggingFilter]
        public ActionResult Blank()
        {
            return View("Blank");
        }
        [LoggingFilter]
        // GET: /Home/Login
        public ActionResult Login()
        {
            return View(new User());
        }



        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {

                if (user.loginUser(user))
                {
                    String loginName = user.username;
                    String account = user.acctype.ToString();
                    String loggedIn = user.loggedIn.ToString();

                    //Store in session
                    Session["loginName"] = loginName;
                    Session["account"] = account;
                    Session["loggedIn"] = loggedIn;
                    Session["lg"] = user;

                    Debug.WriteLine("account type " + user.username);
                    if (account.Equals("Administrator"))
                    {
                        return RedirectToAction("Admin", "Home");
                    }
                    if (account.Equals("Manager"))
                    {
                        return RedirectToAction("Product", "Manager");
                    }
                    if (account.Equals("Shipper"))
                    {
                        return RedirectToAction("Orders", "Shipping");
                    }


                    return View();
                }

                else
                {

                    ModelState.AddModelError("", "The username or password is incorrect.");
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }
        [LoggingFilter]
        // GET: /LogOut
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Home");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Models;
namespace WhiskyGaloreAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Manager()
        {
            return View();
        }

        //
        // GET: /Home/Admin
        public ActionResult Admin()
        {

            return View();
        }


        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }

        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }
        public ActionResult Whisky()
        {
            var c= DependencyResolver.Current.GetService<ManagerController>();
            return c.Whisky();
        }
        public ActionResult Forms()
        {
            return View("Forms");
        }

        public ActionResult Panels()
        {
            return View("Panels");
        }

        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        public ActionResult Notifications()
        {
            return View("Notifications");
        }

        public ActionResult Typography()
        {
            return View("Typography");
        }

        public ActionResult Icons()
        {
            return View("Icons");
        }

        public ActionResult Grid()
        {
            return View("Grid");
        }

        public ActionResult Blank()
        {
            return View("Blank");
        }

        //
        // GET: /Home/Login
        public ActionResult Login()
        {
            return View(new User());
        }


        //
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
                        return RedirectToAction("Manager", "Home");
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

        //
        // GET: /LogOut
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Home");
        }

    }
}